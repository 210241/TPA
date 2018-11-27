using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Reflection.Enums;

namespace Reflection.Model
{
    [DataContract(Name = "TypeReader")]
    public class TypeReader
    {
        [DataMember]
        public static Dictionary<string, TypeReader> TypeDictionary = new Dictionary<string, TypeReader>();

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string NamespaceName { get; set; }

        [DataMember]
        public TypeReader BaseType { get; set; }

        [DataMember]
        public List<TypeReader> GenericArguments { get; set; }

        [DataMember]
        public AccessLevel AccessLevel { get; set; }

        [DataMember]
        public AbstractEnum AbstractEnum { get; set; }

        [DataMember]
        public StaticEnum StaticEnum { get; set; }

        [DataMember]
        public SealedEnum SealedEnum { get; set; }

        [DataMember]
        public TypeKind Type { get; set; }

        [DataMember]
        public List<TypeReader> ImplementedInterfaces { get; set; }

        [DataMember]
        public List<TypeReader> NestedTypes { get; set; }

        [DataMember]
        public List<PropertyReader> Properties { get; set; }

        [DataMember]
        public TypeReader DeclaringType { get; set; }

        [DataMember]
        public List<MethodReader> Methods { get; set; }

        [DataMember]
        public List<MethodReader> Constructors { get; set; }

        [DataMember]
        public List<ParameterReader> Fields { get; set; }

        private TypeReader()
        {

        }

        public TypeReader(Type type)
        {
            Name = type.Name;
            if (!TypeDictionary.ContainsKey(Name))
            {
                TypeDictionary.Add(Name, this);
            }

            Type = GetTypeEnum(type);
            BaseType = EmitExtends(type.BaseType);
            EmitModifiers(type);

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

        private void EmitModifiers(Type type)
        {
            AccessLevel  = type.IsPublic || type.IsNestedPublic ? AccessLevel.IsPublic :
                type.IsNestedFamily ? AccessLevel.IsProtected :
                type.IsNestedFamANDAssem ? AccessLevel.Internal :
                AccessLevel.IsPrivate;
            StaticEnum  = type.IsSealed && type.IsAbstract ? StaticEnum.Static : StaticEnum.NotStatic;
            SealedEnum  = SealedEnum.NotSealed;
            AbstractEnum  = AbstractEnum.NotAbstract;
            if (StaticEnum == StaticEnum.NotStatic)
            {
                SealedEnum = type.IsSealed ? SealedEnum.Sealed : SealedEnum.NotSealed;
                AbstractEnum = type.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;
            }
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
