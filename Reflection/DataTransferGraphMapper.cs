using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;
using Base.Model;
using Reflection.LogicModel;

namespace Reflection
{
    public static class DataTransferGraphMapper
    {
        public static AssemblyBase AssemblyBase(AssemblyLogicReader assemblyLogicReader)
        {
            _typeDictionary = new Dictionary<string, TypeBase>();
            return new AssemblyBase()
            {
                Name = assemblyLogicReader.Name,
                Namespaces = assemblyLogicReader.NamespaceLogicReader?.Select(NamespaceBase).ToList()
            };
        }

        public static NamespaceBase NamespaceBase(NamespaceLogicReader namespaceLogicReader)
        {
            return new NamespaceBase()
            {
                Name = namespaceLogicReader.Name,
                Types = namespaceLogicReader.Types?.Select(GetOrAdd).ToList()
            };
        }

        public static TypeBase TypeBase(TypeLogicReader typeLogicReader)
        {
            TypeBase typeBase = new TypeBase()
            {
                Name = typeLogicReader.Name
            };

            _typeDictionary.Add(typeBase.Name, typeBase);

            typeBase.NamespaceName = typeLogicReader.NamespaceName;
            typeBase.Type = typeLogicReader.Type.toBaseEnum();
            typeBase.BaseType = GetOrAdd(typeLogicReader.BaseType);
            typeBase.DeclaringType = GetOrAdd(typeLogicReader.DeclaringType);
            typeBase.AbstractEnum = typeLogicReader.AbstractEnum.toBaseEnum();
            typeBase.AccessLevel = typeLogicReader.AccessLevel.toBaseEnum();
            typeBase.SealedEnum = typeLogicReader.SealedEnum.toBaseEnum();
            typeBase.StaticEnum = typeLogicReader.StaticEnum.toBaseEnum();

            typeBase.Constructors = typeLogicReader.Constructors?.Select(MethodBase).ToList();
            typeBase.Fields = typeLogicReader.Fields?.Select(FieldBase).ToList();
            typeBase.GenericArguments = typeLogicReader.GenericArguments?.Select(GetOrAdd).ToList();
            typeBase.ImplementedInterfaces = typeLogicReader.ImplementedInterfaces?.Select(GetOrAdd).ToList();
            typeBase.Methods = typeLogicReader.Methods?.Select(MethodBase).ToList();
            typeBase.NestedTypes = typeLogicReader.NestedTypes?.Select(GetOrAdd).ToList();
            typeBase.Properties = typeLogicReader.Properties?.Select(PropertyBase).ToList();

            return typeBase;
        }

        public static MethodBase MethodBase(MethodLogicReader methodLogicReader)
        {
            return new MethodBase()
            {
                Name = methodLogicReader.Name,
                AbstractEnum = methodLogicReader.AbstractEnum.toBaseEnum(),
                AccessLevel = methodLogicReader.AccessLevel.toBaseEnum(),
                Extension = methodLogicReader.Extension,
                ReturnType = GetOrAdd(methodLogicReader.ReturnType),
                StaticEnum = methodLogicReader.StaticEnum.toBaseEnum(),
                VirtualEnum = methodLogicReader.VirtualEnum.toBaseEnum(),
                GenericArguments = methodLogicReader.GenericArguments?.Select(GetOrAdd).ToList(),
                Parameters = methodLogicReader.Parameters?.Select(ParameterBase).ToList()
            };
        }

        public static ParameterBase ParameterBase(ParameterLogicReader parameterLogicReader)
        {
            return new ParameterBase()
            {
                Name = parameterLogicReader.Name,
                Type = GetOrAdd(parameterLogicReader.Type)
            };
        }
        
        public static FieldBase FieldBase(FieldLogicReader fieldLogicReader)
        {
            return new FieldBase()
            {
                Name = fieldLogicReader.Name,
                Type = GetOrAdd(fieldLogicReader.Type)
            };
        }

        public static PropertyBase PropertyBase(PropertyLogicReader propertyLogicReader)
        {
            return new PropertyBase()
            {
                Name = propertyLogicReader.Name,
                Type = GetOrAdd(propertyLogicReader.Type)
            };
        }

        public static TypeBase GetOrAdd(TypeLogicReader baseType)
        {
            if (baseType != null)
            {
                if (_typeDictionary.ContainsKey(baseType.Name))
                {
                    return _typeDictionary[baseType.Name];
                }
                else
                {
                    return TypeBase(baseType);
                }
            }
            else
                return null;
        }

        private static Dictionary<string, TypeBase> _typeDictionary;
    }
}
