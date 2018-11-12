using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataTransfer.Model.Enums;

namespace Reflection.Model
{

    public class TypeReader
    {

        public static Dictionary<string, TypeReader> TypeDictionary = new Dictionary<string, TypeReader>();

        public string Name { get; set; }

        public string NamespaceName { get; set; }

        public TypeReader BaseType { get; set; }

        public List<TypeReader> GenericArguments { get; set; }

        public Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> Modifiers { get; set; }

        public TypeKind Type { get; set; }

        public List<TypeReader> ImplementedInterfaces { get; set; }

        public List<TypeReader> NestedTypes { get; set; }

        public List<PropertyReader> Properties { get; set; }

        public TypeReader DeclaringType { get; set; }

        public List<MethodReader> Methods { get; set; }

        public List<MethodReader> Constructors { get; set; }

        public List<ParameterReader> Fields { get; set; }


        public TypeReader(Type type)
        {
            Name = type.Name;
            if (!TypeDictionary.ContainsKey(Name))
            {
                TypeDictionary.Add(Name, this);
            }

            Type = GetTypeEnum(type);
            BaseType = EmitExtends(type.BaseType);
            Modifiers = EmitModifiers(type);

            DeclaringType = EmitDeclaringType(type.DeclaringType);
            Constructors = MethodReader.EmitConstructors(type);
            Methods = MethodReader.EmitMethods(type);
            NestedTypes = EmitNestedTypes(type);
            ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
            GenericArguments = !type.IsGenericTypeDefinition ? null : EmitGenericArguments(type);
            Properties = PropertyReader.EmitProperties(type);
            Fields = EmitFields(type);
        }

        private TypeReader(string typeName, string namespaceName)
        {
            Name = typeName;
            this.NamespaceName = namespaceName;
        }

        private TypeReader(string typeName, string namespaceName, IEnumerable<TypeReader> genericArguments) : this(typeName, namespaceName)
        {
            this.GenericArguments = genericArguments.ToList();
        }


        public static TypeReader EmitReference(Type type)
        {
            if (!type.IsGenericType)
                return new TypeReader(type.Name, type.GetNamespace());

            return new TypeReader(type.Name, type.GetNamespace(), EmitGenericArguments(type));
        }
        public static List<TypeReader> EmitGenericArguments(Type type)
        {
            List<Type> arguments = type.GetGenericArguments().ToList();
            foreach (Type typ in arguments)
            {
                StoreType(typ);
            }

            return arguments.Select(EmitReference).ToList();
        }

        public static void StoreType(Type type)
        {
            if (!TypeDictionary.ContainsKey(type.Name))
            {
                new TypeReader(type);
            }
        }

        private static List<ParameterReader> EmitFields(Type type)
        {
            List<FieldInfo> fieldInfo = type.GetFields(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                           BindingFlags.Static | BindingFlags.Instance).ToList();

            List<ParameterReader> parameters = new List<ParameterReader>();
            foreach (FieldInfo field in fieldInfo)
            {
                StoreType(field.FieldType);
                parameters.Add(new ParameterReader(field.Name, EmitReference(field.FieldType)));
            }
            return parameters;
        }

        private TypeReader EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            StoreType(declaringType);
            return EmitReference(declaringType);
        }
        private List<TypeReader> EmitNestedTypes(Type type)
        {
            List<Type> nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic).ToList();
            foreach (Type typ in nestedTypes)
            {
                StoreType(typ);
            }

            return nestedTypes.Select(t => new TypeReader(t)).ToList();
        }
        private IEnumerable<TypeReader> EmitImplements(IEnumerable<Type> interfaces)
        {
            foreach (Type @interface in interfaces)
            {
                StoreType(@interface);
            }

            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }
        private static TypeKind GetTypeEnum(Type type)
        {
            return type.IsEnum ? TypeKind.EnumType :
                   type.IsValueType ? TypeKind.StructType :
                   type.IsInterface ? TypeKind.InterfaceType :
                   TypeKind.ClassType;
        }

        static Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> EmitModifiers(Type type)
        {
            AccessLevel _access = type.IsPublic || type.IsNestedPublic ? AccessLevel.IsPublic :
                type.IsNestedFamily ? AccessLevel.IsProtected :
                type.IsNestedFamANDAssem ? AccessLevel.Internal :
                AccessLevel.IsPrivate;
            StaticEnum _static = type.IsSealed && type.IsAbstract ? StaticEnum.Static : StaticEnum.NotStatic;
            SealedEnum _sealed = SealedEnum.NotSealed;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (_static == StaticEnum.NotStatic)
            {
                _sealed = type.IsSealed ? SealedEnum.Sealed : SealedEnum.NotSealed;
                _abstract = type.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;
            }



            return new Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum>(_access, _sealed, _abstract, _static);
        }

        private static TypeReader EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            StoreType(baseType);
            return EmitReference(baseType);
        }
    }
}
