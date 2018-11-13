using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reflection;
using System.Linq;

namespace Reflection_Tests
{
    [TestClass]
    public class ReflectionTests
    {
        private const string FilePath = @"..\..\..\LibraryForTests\bin\Debug\LibraryForTests.dll";
        private const string BaseNamespace = "LibraryForTests";
        private const string FirstNamespace = "LibraryForTests.FirstNamespace";
        private const string SecondNamespace = "LibraryForTests.SecondNamespace";
        private Reflector _reflection;

        [TestInitialize]
        public void Initialize()
        {
            _reflection = new Reflector(FilePath);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedNamespaces_ShouldBeOk()
        {
            int expected = 3;
            int actual = _reflection.AssemblyReader.NamespaceReader.Count;
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedTypes_ShouldBeOk()
        {
            int firstExpected = 3;
            int firstActual = _reflection.AssemblyReader.NamespaceReader.Find(n => n.Name == FirstNamespace).Types.Count;
            int secondExpected = 4;
            int secondActual = _reflection.AssemblyReader.NamespaceReader.Find(n => n.Name == SecondNamespace).Types.Count;

            firstActual.Should().Be(firstExpected);
            secondActual.Should().Be(secondExpected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedParametersInClass_ShouldBeOk()
        {
            int expected = 5;
            int actual = _reflection.AssemblyReader.NamespaceReader
                .Find(n => n.Name == FirstNamespace).Types
                .Where(n => n.Name == "AClass").ToList().First().Fields.Count;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedPropertiesInClass_ShouldBeOk()
        {
            int expected = 1;
            int actual = _reflection.AssemblyReader.NamespaceReader
                .Find(n => n.Name == FirstNamespace).Types
                .Where(n => n.Name == "CClass").ToList().First().Properties.Count;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedMethodsInClass_ShouldBeOk()
        {
            int expected = 1;
            int actual = _reflection.AssemblyReader.NamespaceReader
                .Find(n => n.Name == SecondNamespace).Types
                .Where(n => n.Name == "Car").ToList().First().Methods.Count;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedExtensionMethodsInClass_ShouldBeOk()
        {
            int expected = 1;
            int actual = _reflection.AssemblyReader.NamespaceReader
                .Find(n => n.Name == BaseNamespace).Types
                .Where(n => n.Name == "BaseClass").ToList().First().Methods
                .FindAll(n => n.Extension == true).Count;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedGenericArguments_ShouldBeOk()
        {
            int expected = 1;
            int actual = _reflection.AssemblyReader.NamespaceReader
                .Find(n => n.Name == SecondNamespace).Types
                .Where(n => n.GenericArguments != null).ToList().Count;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedConstructorsInClass_ShouldBeOk()
        {
            int expected = 1;
            int actual = _reflection.AssemblyReader.NamespaceReader
                .Find(n => n.Name == FirstNamespace).Types
                .Where(n => n.Name == "BClass").ToList().First().Constructors.Count;

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Reflector_NumberOfLoadedImplementedInterfacesInClass_ShouldBeOk()
        {
            int expected = 1;
            int actual = _reflection.AssemblyReader.NamespaceReader
                .Find(n => n.Name == SecondNamespace).Types
                .Where(n => n.Name == "Car").ToList().First().ImplementedInterfaces.Count;

            actual.Should().Be(expected);
        }
    }
}