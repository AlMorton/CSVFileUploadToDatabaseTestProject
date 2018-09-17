using CSVUploadToData.Tests.MockSetUp;
using CSVUploadToDataProject.Services;
using CSVUploadToDataTestProject.EntityFramework.DomainModel;
using CSVUploadToDataTestProject.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSVUploadToData.Tests
{
    [TestClass]
    public class ICSVDataServiceTests
    {
        private readonly CSVDataService _cSVDataService;

        private readonly MockDBContext<CSVData> _MockDBContext;

        public ICSVDataServiceTests()
        {
            _cSVDataService = new CSVDataService(_MockDBContext._repository);
        }

        [TestMethod]
        public void RemoveDataTest()
        {
            _cSVDataService.SaveToDatabase(new List<CSVDataDTO>());
        }


    }
}
