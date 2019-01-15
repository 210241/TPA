using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Model;
using SerializationXml.Model;

namespace SerializationXml
{
    public static class DataTransferGraphMapper
    {
        public static AssemblyBase AssemblyBase(AssemblyDbSaver assemblyDbSaver)
        {
            _typeDictionary = new Dictionary<string, TypeBase>();
            return new AssemblyBase()
            {
                Name = assemblyDbSaver.Name,
                Namespaces = assemblyDbSaver.Namespaces?.Select(NamespaceBase).ToList()
            };
        }

        public static NamespaceBase NamespaceBase(NamespaceDbSaver namespaceDbSaver)
        {
            return new NamespaceBase()
            {
                Name = namespaceDbSaver.Name,
                Types = namespaceDbSaver.Types?.Select(GetOrAdd).ToList()
            };
        }

        public static TypeBase TypeBase(TypeDbSaver typeDbSaver)
        {
            TypeBase typeBase = new TypeBase()
            {
                Name = typeDbSaver.Name
            };

            _typeDictionary.Add(typeBase.Name, typeBase);

            typeBase.NamespaceName = typeDbSaver.NamespaceName;
            typeBase.Type = typeDbSaver.Type;
            typeBase.BaseType = GetOrAdd(typeDbSaver.BaseType);
            typeBase.DeclaringType = GetOrAdd(typeDbSaver.DeclaringType);
            typeBase.AbstractEnum = typeDbSaver.AbstractEnum;
            typeBase.AccessLevel = typeDbSaver.AccessLevel;
            typeBase.SealedEnum = typeDbSaver.SealedEnum;
            typeBase.StaticEnum = typeDbSaver.StaticEnum;

            typeBase.Constructors = typeDbSaver.Constructors?.Select(MethodBase).ToList();
            typeBase.Fields = typeDbSaver.Fields?.Select(FieldBase).ToList();
            typeBase.GenericArguments = typeDbSaver.GenericArguments?.Select(GetOrAdd).ToList();
            typeBase.ImplementedInterfaces = typeDbSaver.ImplementedInterfaces?.Select(GetOrAdd).ToList();
            typeBase.Methods = typeDbSaver.Methods?.Select(MethodBase).ToList();
            typeBase.NestedTypes = typeDbSaver.NestedTypes?.Select(GetOrAdd).ToList();
            typeBase.Properties = typeDbSaver.Properties?.Select(PropertyBase).ToList();

            return typeBase;
        }

        public static MethodBase MethodBase(MethodDbSaver methodDbSaver)
        {
            return new MethodBase()
            {
                Name = methodDbSaver.Name,
                AbstractEnum = methodDbSaver.AbstractEnum,
                AccessLevel = methodDbSaver.AccessLevel,
                Extension = methodDbSaver.Extension,
                ReturnType = GetOrAdd(methodDbSaver.ReturnType),
                StaticEnum = methodDbSaver.StaticEnum,
                VirtualEnum = methodDbSaver.VirtualEnum,
                GenericArguments = methodDbSaver.GenericArguments?.Select(GetOrAdd).ToList(),
                Parameters = methodDbSaver.Parameters?.Select(ParameterBase).ToList()
            };
        }

        public static ParameterBase ParameterBase(ParameterDbSaver parameterDbSaver)
        {
            return new ParameterBase()
            {
                Name = parameterDbSaver.Name,
                Type = GetOrAdd(parameterDbSaver.Type)
            };
        }
        
        public static FieldBase FieldBase(FieldDbSaver fieldDbSaver)
        {
            return new FieldBase()
            {
                Name = fieldDbSaver.Name,
                Type = GetOrAdd(fieldDbSaver.Type)
            };
        }

        public static PropertyBase PropertyBase(PropertyDbSaver propertyDbSaver)
        {
            return new PropertyBase()
            {
                Name = propertyDbSaver.Name,
                Type = GetOrAdd(propertyDbSaver.Type)
            };
        }

        public static TypeBase GetOrAdd(TypeDbSaver baseType)
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
