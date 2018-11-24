using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializationXml;
using Reflection;
using Reflection.Model;
using FluentAssertions;
using ObjectsComparer;

namespace XmlSerializationTests
{
    [TestClass]
    public class XmlSerializationTest
    {
        private const string FilePath = @"..\..\..\LibraryForTests\bin\Debug\LibraryForTests.dll";
        private Reflector reflector;

        [TestInitialize]
        public void Initialize()
        {
            reflector = new Reflector(FilePath);
        }

        [TestMethod]
        public void SerializeAndDeserialize()
        {
            
            XmlSerializator xmlSerializator = new XmlSerializator();
        
            xmlSerializator.Serialize(reflector.AssemblyReader, "SerializedAssembly.xml");

            AssemblyReader assemlbyReader =  xmlSerializator.Deserialize("SerializedAssembly.xml");
        }
    }
}
