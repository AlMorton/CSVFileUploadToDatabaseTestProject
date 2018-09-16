using CSVUploadToDataTestProject.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSVUploadToData.Tests
{
    [TestClass]
    public class FileUploadFacadeUnitTests
    {
        private readonly FileUploadFacade _fileUploadFacade;

        private string TestLine = "1,Odaiba,16/09/2018,10";

        private string[] TestArray = new string[]{"1", "Odaiba", "16/09/2018", "10"};

        public FileUploadFacadeUnitTests()
        {
            _fileUploadFacade = new FileUploadFacade();
        }

        [TestMethod]
        public void ParseLineToModelTestMethod()
        {          
            var result = _fileUploadFacade.ParseLine(TestLine);

            Assert.IsTrue(result[0] == "1");
            Assert.IsTrue(result[1] == "Odaiba");
            Assert.IsTrue(result[2] == "16/09/2018");
            Assert.IsTrue(result[3] == "10");
        }

        [TestMethod]
        public void ConvertToCSVDataDTOTestMethod()
        {
            var result = _fileUploadFacade.ConvertToCSVDataDTO(TestArray);

            var date = new DateTime(2018, 09, 16);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.ClientId == 1);
            Assert.IsTrue(result.Site == "Odaiba");
            Assert.IsTrue(result.Date.Date == date.Date);            
            Assert.IsTrue(result.FooData == 10);

        }
    }
}
