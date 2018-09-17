using CSVUploadToDataTestProject.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSVUploadToData.Tests.MockSetUp
{
    public class MockDBContext<T> where T : class
    {
        public Mock<MyDbContext> _dbContext { get; private set; }

        public Mock<DbSet<T>> MockDbSet { get; private set; }        

        public MockDBContext(List<T> TList)
        {
            SetUpMockDbSet(TList);

            _dbContext = new Mock<MyDbContext>();            
            _dbContext.Setup(e => e.Set<T>()).Returns(MockDbSet.Object);
            _dbContext.Setup(e => e.Entry(typeof(T))).
        }

        public Mock<DbSet<T>> SetUpMockDbSet(List<T> TList)
        {
            var list = TList.AsQueryable();

            MockDbSet = new Mock<DbSet<T>>();

            MockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(list.Provider);
            MockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(list.Expression);
            MockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(list.ElementType);
            MockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());          

            return MockDbSet;
        }
    }
}
