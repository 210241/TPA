using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Base;
using Base.Model;
using Base.Enums;
using Reflection.LogicModel;
using MethodBase = Base.Model.MethodBase;

namespace Reflection.Model
{
    public class TypeReader : TypeBase
    {
        public static Dictionary<string, TypeReader> TypeDictionary = new Dictionary<string, TypeReader>();

        private TypeReader(TypeLogicReader baseType)
        {

            this.Name = baseType.Name;
            TypeDictionary.Add(Name, this);
            this.NamespaceName = baseType.NamespaceName;
            this.Type = baseType.Type.toBaseEnum();

            this.BaseType = GetOrAdd(baseType.BaseType);
            this.DeclaringType = GetOrAdd(baseType.DeclaringType);

            this.AbstractEnum = baseType.AbstractEnum.toBaseEnum();
            this.AccessLevel = baseType.AccessLevel.toBaseEnum();
            this.SealedEnum = baseType.SealedEnum.toBaseEnum();
            this.StaticEnum = baseType.StaticEnum.toBaseEnum();


            Constructors = baseType.Constructors.Select(t => new MethodReader(t) as MethodBase ).ToList();

            Fields = baseType.Fields.Select(t => new ParameterReader(t) as ParameterBase).ToList();

            GenericArguments = baseType.GenericArguments.Select(t => GetOrAdd(t) as TypeBase).ToList();

            ImplementedInterfaces = baseType.ImplementedInterfaces.Select(t => GetOrAdd(t) as TypeBase).ToList();

            Methods = baseType.Methods.Select(t => new MethodReader(t) as MethodBase).ToList();

            NestedTypes = baseType.NestedTypes.Select(t => GetOrAdd(t) as TypeBase).ToList();

            Properties = baseType.Properties.Select(t => new PropertyReader(t) as PropertyBase).ToList();

        }

        public static TypeReader GetOrAdd(TypeLogicReader baseType)
        {
            if (baseType == null)
            {
                return null;
            }
            if (TypeDictionary.ContainsKey(baseType.Name))
            {
                return TypeDictionary[baseType.Name];
            }
            else
            {
                return new TypeReader(baseType);
            }
        }

        private TypeReader(TypeBase baseType)
        {
           

            this.Name = baseType.Name;
            TypeDictionary.Add(Name, this);
            this.NamespaceName = baseType.NamespaceName;
            this.Type = baseType.Type;

            this.BaseType = GetOrAdd(baseType.BaseType);
            this.DeclaringType = GetOrAdd(baseType.DeclaringType);

            this.AbstractEnum = baseType.AbstractEnum;
            this.AccessLevel = baseType.AccessLevel;
            this.SealedEnum = baseType.SealedEnum;
            this.StaticEnum = baseType.StaticEnum;

            Constructors = baseType.Constructors.Select(t => new MethodReader(t) as MethodBase).ToList();

            Fields = baseType.Fields.Select(t => new ParameterReader(t) as ParameterBase).ToList();

            GenericArguments = baseType.GenericArguments.Select(t => GetOrAdd(t) as TypeBase).ToList();

            ImplementedInterfaces = baseType.ImplementedInterfaces.Select(t => GetOrAdd(t) as TypeBase).ToList();

            Methods = baseType.Methods.Select(t => new MethodReader(t) as MethodBase).ToList();

            NestedTypes = baseType.NestedTypes.Select(t => GetOrAdd(t) as TypeBase).ToList();

            Properties = baseType.Properties.Select(t => new PropertyReader(t) as PropertyBase).ToList();

        }

        public static TypeReader GetOrAdd(TypeBase baseType)
        {
            if (TypeDictionary.ContainsKey(baseType.Name))
            {
                return TypeDictionary[baseType.Name];
            }
            else
            {
                return new TypeReader(baseType);
            }
        }
    }
}
