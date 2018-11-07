using System;
using ApplicationLogic.Interfaces;
using ApplicationLogic.Model;
using ApplicationLogic.ViewModel;
using DataTransfer.Interfaces;
using DataTransfer.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TEST.CommandTests
{
    [TestClass]
    public class GetPathCommandTest
    {
        protected MainViewModel context;
        protected Mock<IFilePathProvider> filePathProviderMock;
        protected Mock<ILogger> loggerMock;
        protected Mock<IDataStorageProvider> dataStoreProviderMock;
        protected Mock<IMapper<AssemblyDataStorage, NodeItem>> mapperMock;

        protected const string filePath = "This is test path";


        [TestInitialize]
        public void Initialize()
        {
            filePathProviderMock = new Mock<IFilePathProvider>(MockBehavior.Strict);
            loggerMock = new Mock<ILogger>(MockBehavior.Strict);
            dataStoreProviderMock = new Mock<IDataStorageProvider>(MockBehavior.Strict);
            mapperMock = new Mock<IMapper<AssemblyDataStorage, NodeItem>>(MockBehavior.Strict);

            filePathProviderMock.Setup(x => x.GetFilePath()).Returns(filePath);

            context = new MainViewModel(
                filePathProviderMock.Object,
                loggerMock.Object,
                dataStoreProviderMock.Object,
                mapperMock.Object);
        }
        


        [TestMethod]
        public void GetFilePath_ShouldReturnProperValue()
        {
            Assert.IsNull(context.FilePath);

            context.GetFilePathCommand.Execute(null);

            Assert.AreEqual(context.FilePath, filePath);
        }
    }
}
