using ApplicationLogic.Mapper;
using ApplicationLogic.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TEST
{
    [TestClass]
    public class AssemblyTests
    {
        [TestInitialize]
        public void Initialize()
        {
            TestConstants.Context = new NodeItemMapper();
            TestMethodsInitializes.AssemblyInitialize();
        }

        [TestMethod]
        public void Map_AssemblyInitialized_ShouldBeOk()
        {
            NodeItem expected = new NodeItem(TestConstants.AssemblyName, false);
            NodeItem actual =TestConstants.Context.Map(TestConstants.Storage);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
