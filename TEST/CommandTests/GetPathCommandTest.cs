using System;
using ApplicationLogic.Interfaces;
using ApplicationLogic.Model;
using ApplicationLogic.ViewModel;
using DataTransfer.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TEST.CommandTests
{
    [TestClass]
    public class GetPathCommandTest
    {
        protected MainViewModel context;

        protected const string filePath = "This is test path";


        [TestInitialize]
        public void Initialize()
        {

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
