using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer.Model;
using DataTransfer.Model.Enums;

namespace TEST
{
    public class TestStorageInitializers
    {
        public static void AssemblyInitialize()
        {
            TestConstants.AssemblyData = new AssemblyData()
            {
                Id = TestConstants.AssemblyName,
                Name = TestConstants.AssemblyName,
                Namespaces = new List<NamespaceData>()
            };

            TestConstants.Storage = new AssemblyDataStorage(TestConstants.AssemblyData);
        }

        public static void NamespacesInitialize()
        {
            NamespaceData namespaceData = new NamespaceData()
            {
                Id = TestConstants.NamespaceName,
                Name = TestConstants.NamespaceName,
                Types = new List<TypeData>()
            };

            (TestConstants.AssemblyData.Namespaces as List<NamespaceData>).Add(namespaceData);
            TestConstants.Storage.NamespacesDictionary.Add(namespaceData.Id, namespaceData);
        }

        public static void TypesInitialize()
        {
            NamespaceData namespaceData = TestConstants.Storage.NamespacesDictionary[TestConstants.NamespaceName];
            TypeData typeData = CreateSimpleTypeData(TestConstants.TypeName);
            (namespaceData.Types as List<TypeData>).Add(typeData);
            TestConstants.Storage.TypesDictionary.Add(typeData.Id, typeData);
        }

        public static void PropertiesInitialize()
        {
            TypeData typeData = TestConstants.Storage.TypesDictionary[TestConstants.TypeName];
            typeData.TypeKind = TypeKind.ClassType;

            TypeData propertyTypeData = CreateSimpleTypeData(TestConstants.SecondTypeName);
            PropertyData propertyData = new PropertyData()
            {
                Id = TestConstants.PropertyName,
                Name = TestConstants.PropertyName,
                TypeMetadata = propertyTypeData
            };
            (typeData.Properties as List<PropertyData>).Add(propertyData);
            TestConstants.Storage.PropertiesDictionary.Add(propertyData.Id, propertyData);
            TestConstants.Storage.TypesDictionary.Add(propertyTypeData.Id, propertyTypeData);
        }

        public static void MethodsInitialize(IEnumerable<ParameterData> parameters)
        {
            TypeData typeData = TestConstants.Storage.TypesDictionary[TestConstants.TypeName];
            typeData.TypeKind = TypeKind.ClassType;

            MethodData methodData = new MethodData()
            {
                Id = TestConstants.MethodName,
                Name = TestConstants.MethodName,
                GenericArguments = new List<TypeData>(),
                Modifiers = new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(
                    TestConstants.MethodAccessLevel,
                    default(AbstractEnum),
                    default(StaticEnum),
                    default(VirtualEnum)),
                ReturnType = null,
                Parameters = parameters
            };

            (typeData.Methods as List<MethodData>).Add(methodData);
            TestConstants.Storage.MethodsDictionary.Add(methodData.Id, methodData);
        }

        private static TypeData CreateSimpleTypeData(string typeName)
        {
            return new TypeData // add only basic information
            {
                Id = typeName,
                Name = typeName,
                NamespaceName = TestConstants.NamespaceName,
                Modifiers = new Tuple<AccessLevel, SealedEnum, AbstractEnum>(
                    TestConstants.TypeAccessLevel,
                    default(SealedEnum),
                    default(AbstractEnum)),
                TypeKind = default(TypeKind),
                Attributes = new List<Attribute>(),
                Properties = new List<PropertyData>(),
                Constructors = new List<MethodData>(),
                GenericArguments = new List<TypeData>(),
                ImplementedInterfaces = new List<TypeData>(),
                Methods = new List<MethodData>(),
                NestedTypes = new List<TypeData>()
            };
        }
    }
}
