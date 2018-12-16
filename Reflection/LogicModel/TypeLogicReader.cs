﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Base.Enums;
using Base.Model;
using Reflection.Model;

namespace Reflection.LogicModel
{
    public class TypeLogicReader
    {
        public static Dictionary<string, TypeLogicReader> TypeDictionary = new Dictionary<string, TypeLogicReader>();

        public string Name { get; set; }

        public string NamespaceName { get; set; }

        public TypeLogicReader BaseType { get; set; }

        public List<TypeLogicReader> GenericArguments { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public AbstractEnum AbstractEnum { get; set; }

        public StaticEnum StaticEnum { get; set; }

        public SealedEnum SealedEnum { get; set; }

        public TypeKind Type { get; set; }

        public List<TypeLogicReader> ImplementedInterfaces { get; set; }

        public List<TypeLogicReader> NestedTypes { get; set; }

        public List<PropertyLogicReader> Properties { get; set; }

        public TypeLogicReader DeclaringType { get; set; }

        public List<MethodLogicReader> Methods { get; set; }

        public List<MethodLogicReader> Constructors { get; set; }

        public List<ParameterLogicReader> Fields { get; set; }

        private TypeLogicReader()
        {

        }

        public TypeLogicReader(Type type)
        {
            Name = type.Name;
            if (!TypeDictionary.ContainsKey(Name))
            {
                TypeDictionary.Add(Name, this);
            }

            Type = GetTypeEnum(type);
            BaseType = EmitExtends(type.BaseType);
            EmitModifiers(type);
            if (Name == "ActivationException")
            {
                Console.WriteLine("kp");
            }
            DeclaringType = EmitDeclaringType(type.DeclaringType);
            Constructors = MethodLogicReader.EmitConstructors(type);
            Methods = MethodLogicReader.EmitMethods(type);
            NestedTypes = EmitNestedTypes(type);
            ImplementedInterfaces = EmitImplements(type.GetInterfaces()).ToList();
            GenericArguments = !type.IsGenericTypeDefinition ? new List<TypeLogicReader>() : EmitGenericArguments(type);
            Properties = PropertyLogicReader.EmitProperties(type);
            Fields = EmitFields(type);
            if (Name == "Exception")
            {
                Console.WriteLine("exc");
            }
        }

        private TypeLogicReader(TypeBase baseType)
        {
            TypeDictionary.Add(Name, this);

            this.Name = baseType.Name;
            this.NamespaceName = baseType.NamespaceName;
            this.Type = baseType.Type;

            this.BaseType = GetOrAdd(baseType.BaseType);
            this.DeclaringType = GetOrAdd(baseType.DeclaringType);

            this.AbstractEnum = baseType.AbstractEnum;
            this.AccessLevel = baseType.AccessLevel;
            this.SealedEnum = baseType.SealedEnum;
            this.StaticEnum = baseType.StaticEnum;

            //TODO: LINQ

            foreach (var baseConstructor in baseType.Constructors)
            {
                this.Constructors.Add(new MethodLogicReader(baseConstructor));
            }

            foreach (var baseField in baseType.Fields)
            {
                this.Fields.Add(new ParameterLogicReader(baseField));
            }

            foreach (var baseGenericArgument in baseType.GenericArguments)
            {
                this.GenericArguments.Add(GetOrAdd(baseGenericArgument));
            }

            foreach (var baseImplementedInterface in baseType.ImplementedInterfaces)
            {
                this.ImplementedInterfaces.Add(GetOrAdd(baseImplementedInterface));
            }

            foreach (var baseMethod in baseType.Methods)
            {
                this.Methods.Add(new MethodLogicReader(baseMethod));
            }

            foreach (var baseNestedType in baseType.NestedTypes)
            {
                this.NestedTypes.Add(GetOrAdd(baseNestedType));
            }

            foreach (var baseProperty in baseType.Properties)
            {
                this.Properties.Add(new PropertyLogicReader(baseProperty));
            }
        }

        public static TypeLogicReader GetOrAdd(TypeBase baseType)
        {
            if (TypeDictionary.ContainsKey(baseType.Name))
            {
                return TypeDictionary[baseType.Name];
            }
            else
            {
                return new TypeLogicReader(baseType);
            }
        }

        public static TypeLogicReader GetOrAdd(Type type)
        {
            if (TypeDictionary.ContainsKey(type.Name))
            {
                return TypeDictionary[type.Name];
            }
            else
            {
                return new TypeLogicReader(type);
            }
        }


        //private TypeLogicReader(string typeName, string namespaceName)
        //{
        //    Name = typeName;
        //    this.NamespaceName = namespaceName;
        //}

        //private TypeLogicReader(string typeName, string namespaceName, IEnumerable<TypeLogicReader> genericArguments) : this(typeName, namespaceName)
        //{
        //    this.GenericArguments = genericArguments.ToList();
        //}


        //public static TypeLogicReader EmitReference(Type type)
        //{
        //    if (!type.IsGenericType)
        //        return new TypeLogicReader(type.Name, type.GetNamespace());

        //    return new TypeLogicReader(type.Name, type.GetNamespace(), EmitGenericArguments(type));
        //}
        public static List<TypeLogicReader> EmitGenericArguments(Type type)
        {
            if (!type.ContainsGenericParameters)
            {
                return  new List<TypeLogicReader>();
            }
            List<Type> arguments = type.GetGenericArguments().ToList();
            foreach (Type typ in arguments)
            {
                StoreType(typ);
            }

            return arguments.Select(GetOrAdd).ToList();
        }

        public static void StoreType(Type type)
        {
            if (!TypeDictionary.ContainsKey(type.Name))
            {
                new TypeLogicReader(type);
            }
        }

        private static List<ParameterLogicReader> EmitFields(Type type)
        {
            List<FieldInfo> fieldInfo = type.GetFields(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                           BindingFlags.Static | BindingFlags.Instance).ToList();

            List<ParameterLogicReader> parameters = new List<ParameterLogicReader>();
            foreach (FieldInfo field in fieldInfo)
            {
                StoreType(field.FieldType);
                parameters.Add(new ParameterLogicReader(field.Name, GetOrAdd(field.FieldType)));
            }
            return parameters;
        }

        private TypeLogicReader EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            StoreType(declaringType);
            return GetOrAdd(declaringType);
        }
        private List<TypeLogicReader> EmitNestedTypes(Type type)
        {
            List<Type> nestedTypes = type.GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic).ToList();
            foreach (Type typ in nestedTypes)
            {
                StoreType(typ);
            }

            return nestedTypes.Select(t => new TypeLogicReader(t)).ToList();
        }
        private IEnumerable<TypeLogicReader> EmitImplements(IEnumerable<Type> interfaces)
        {
            foreach (Type @interface in interfaces)
            {
                StoreType(@interface);
            }

            return from currentInterface in interfaces
                   select GetOrAdd(currentInterface);
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
            AccessLevel = type.IsPublic || type.IsNestedPublic ? AccessLevel.IsPublic :
                type.IsNestedFamily ? AccessLevel.IsProtected :
                type.IsNestedFamANDAssem ? AccessLevel.Internal :
                AccessLevel.IsPrivate;
            StaticEnum = type.IsSealed && type.IsAbstract ? StaticEnum.Static : StaticEnum.NotStatic;
            SealedEnum = SealedEnum.NotSealed;
            AbstractEnum = AbstractEnum.NotAbstract;
            if (StaticEnum == StaticEnum.NotStatic)
            {
                SealedEnum = type.IsSealed ? SealedEnum.Sealed : SealedEnum.NotSealed;
                AbstractEnum = type.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;
            }
        }

        private static TypeLogicReader EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            StoreType(baseType);
            if (baseType.Name == "Exception")
            {
                Console.WriteLine("EEEE");
            }
            return GetOrAdd(baseType);
        }
    }
}