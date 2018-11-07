using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLogic.Mapper;
using ApplicationLogic.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TEST
{
    [TestClass]
    public class TypeTests
    {
        [TestInitialize]
        public void Initialize()
        {
            TestConstants.Context = new NodeItemMapper();
            TestStorageInitializers.AssemblyInitialize();
            TestStorageInitializers.NamespacesInitialize();
            TestStorageInitializers.TypesInitialize();
        }

        [TestMethod]
        public void Map_TypeInitialized_ShouldBeOk()
        {
            NodeItem actual = TestConstants.Context.Map(TestConstants.Storage);
            NodeItem expected = new NodeItem(TestConstants.AssemblyName, true)
            {
                Children =
                {
                    new NodeItem($"Namespace: {TestConstants.NamespaceName}", true)
                    {
                        Children = { new NodeItem($"Enum: {TestConstants.TypeName}", false)}
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}