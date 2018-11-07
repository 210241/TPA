using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLogic.Base;
using ApplicationLogic.Interfaces;
using ApplicationLogic.Mapper;
using ApplicationLogic.Model;
using ApplicationLogic.ViewModel;
using Console_UI;
using DataTransfer.Interfaces;
using DataTransfer.Model;
using FluentAssertions;
using Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TEST.CommandTests
{
    [TestClass]
    public class LoadDataCommandTest
    {
        protected MainViewModel context;
        protected Mock<IFilePathProvider> filePathProviderMock;
        protected Mock<ILogger> loggerMock;
        protected Mock<IDataStorageProvider> dataStoreProviderMock;
        protected IMapper<AssemblyDataStorage, NodeItem> mapper;

        private AssemblyDataStorage testStorage;
        private List<NodeItem> testNodeItems;
        private string filePath = "test" ;

       [TestInitialize]
        public void Initialize()
        {
            filePathProviderMock = new Mock<IFilePathProvider>(MockBehavior.Strict);
            loggerMock = new Mock<ILogger>(MockBehavior.Strict);
            dataStoreProviderMock = new Mock<IDataStorageProvider>(MockBehavior.Strict);
            mapper = new NodeItemMapper(); //new Mock<IMapper<AssemblyDataStorage, NodeItem>>(MockBehavior.Strict);

            TestConstants.Context = new NodeItemMapper();
            TestStorageInitializers.AssemblyInitialize();
            TestStorageInitializers.NamespacesInitialize();
            testStorage = TestConstants.Storage;

            dataStoreProviderMock.Setup(x => x.GetDataStorage(filePath)).Returns(testStorage);
            
            testNodeItems = new List<NodeItem>() { mapper.Map(testStorage) };

            context = new MainViewModel(
                filePathProviderMock.Object,
                loggerMock.Object,
                dataStoreProviderMock.Object,
                mapper);

            context.FilePath = filePath;
        }

        [TestMethod]
        public void Example()
        {
            Assert.AreEqual(context.NodeItems.Count, 0);

            try
            {
                (context.LoadDataCommand as IAsynchronousCommand).ExecuteAsync().Wait();
            }
            catch (AggregateException)
            {
            }

            context.NodeItems.Should().BeEquivalentTo(testNodeItems);
        }
    }
}
