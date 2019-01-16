using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reflection;
using System.Linq;
using NUnit.Framework;
using Reflection.Enums;

namespace Reflection_Tests
{
    [TestClass]
    public class ReflectionTests
    {
        private const string FilePath = @"..\..\..\LibraryForTests\TPA.ApplicationArchitecture.dll";
        private const string FirstNamespace = "TPA.ApplicationArchitecture.Data";
        private const string SecondNamespace = "TPA.ApplicationArchitecture.Data.CircularReference";
        private Reflector _reflection;

        [TestInitialize]
        public void Initialize()
        {
            _reflection = new Reflector(FilePath);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedNamespaces_ShouldBeOk()
        {
            int expected = 4;
            int actual = _reflection.AssemblyLogicReader.NamespaceLogicReader.Count;
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedTypes_ShouldBeOk()
        {
            int firstExpected = 12;
            int firstActual = _reflection.AssemblyLogicReader.NamespaceLogicReader.Find(n => n.Name == FirstNamespace).Types.Count;
            int secondExpected = 2;
            int secondActual = _reflection.AssemblyLogicReader.NamespaceLogicReader.Find(n => n.Name == SecondNamespace).Types.Count;

            firstActual.Should().Be(firstExpected);
            secondActual.Should().Be(secondExpected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedFieldsInClass_ShouldBeOk()
        {
            int expected = 3;
            int actual = _reflection.AssemblyLogicReader.NamespaceLogicReader
                .Find(n => n.Name == FirstNamespace).Types
                .Where(n => n.Name == "GenericClass <T>").ToList().First().Fields.Count;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedPropertiesInClass_ShouldBeOk()
        {
            int expected = 1;
            int actual = _reflection.AssemblyLogicReader.NamespaceLogicReader
                .Find(n => n.Name == FirstNamespace).Types
                .Where(n => n.Name == "InnerClass").ToList().First().Properties.Count;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedMethodsInClass_ShouldBeOk()
        {
            int expected = 3;
            int actual = _reflection.AssemblyLogicReader.NamespaceLogicReader
                .Find(n => n.Name == FirstNamespace).Types
                .Where(n => n.Name == "AbstractClass").ToList().First().Methods.Count;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedGenericArguments_ShouldBeOk()
        {
            int expected = 1;
            int actual = _reflection.AssemblyLogicReader.NamespaceLogicReader
                .Find(n => n.Name == FirstNamespace).Types
                .Where(n => n.GenericArguments.Count != 0).ToList().Count;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedConstructorsInClass_ShouldBeOk()
        {
            int expected = 1;
            int actual = _reflection.AssemblyLogicReader.NamespaceLogicReader
                .Find(n => n.Name == FirstNamespace).Types
                .Where(n => n.Name == "Linq2SQL").ToList().First().Constructors.Count;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedParametersInClass_ShouldBeOk()
        {
            int expected = 1;
            int actual = _reflection.AssemblyLogicReader.NamespaceLogicReader
                .Find(n => n.Name == FirstNamespace).Types
                .First(n => n.Name == "AbstractClass").Methods
                .Find(m => m.Name == "set_Property1").Parameters.ToList().Count;

            actual.Should().Be(expected);

        }


        [TestMethod]
        public void Reflector_NumberOfLoadedImplementedInterfacesInClass_ShouldBeOk()
        {
            int expected = 3;
            int actual = _reflection.AssemblyLogicReader.NamespaceLogicReader
                .Find(n => n.Name == FirstNamespace).Types
                .Where(n => n.Name == "Enum").ToList().First().ImplementedInterfaces.Count;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedAttributesInClass_ShouldBeOk()
        {
            int expected = 1;
            int actual = _reflection.AssemblyLogicReader.NamespaceLogicReader
                .Find(n => n.Name == FirstNamespace).Types
                .First(t => t.Name == "ClassWithAttribute").Attributes.Count;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedEnums_ShouldBeOk()
        {
            int expected = 1;
            int actual = _reflection.AssemblyLogicReader.NamespaceLogicReader
                .Find(n => n.Name == FirstNamespace).Types
                .Where(t => t.Type == TypeKind.EnumType).ToList().Count;

            actual.Should().Be(expected);

        }

        [TestMethod]
        public void Reflector_CorrectNameOfLoadedDerivedClassesInClass_ShouldBeOk()
        {
            string expected = "AbstractClass";
            string actual = _reflection.AssemblyLogicReader.NamespaceLogicReader
                .Find(n => n.Name == FirstNamespace).Types
                .First(t => t.Name == "DerivedClass").BaseType.Name;

            actual.Should().Be(expected);
        }
    }
}