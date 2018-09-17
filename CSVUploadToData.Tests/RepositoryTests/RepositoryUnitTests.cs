using CSVUploadToDataProject.EntityFramework.Repository;
using CSVUploadToDataTestProject.EntityFramework;
using CSVUploadToDataTestProject.EntityFramework.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSVUploadToData.Tests.RepositoryTests
{
    [TestClass]
    public class RepositoryUnitTests
    {
        private readonly Repository<Client, int> _repository;

        private Mock<MyDbContext> _dbContext { get; set; }

        public RepositoryUnitTests()
        {
            var clients = SetUpClients().AsQueryable();

            var mockSet = new Mock<DbSet<Client>>();

            mockSet.As<IQueryable<Client>>().Setup(m => m.Provider).Returns(clients.Provider);
            mockSet.As<IQueryable<Client>>().Setup(m => m.Expression).Returns(clients.Expression);
            mockSet.As<IQueryable<Client>>().Setup(m => m.ElementType).Returns(clients.ElementType);
            mockSet.As<IQueryable<Client>>().Setup(m => m.GetEnumerator()).Returns(clients.GetEnumerator());

            _dbContext = new Mock<MyDbContext>();

            _dbContext.Setup(e => e.Clients).Returns(mockSet.Object);
            _dbContext.Setup(e => e.Set<Client>()).Returns(mockSet.Object);

            _repository = new Repository<Client, int>(_dbContext.Object);            
            
        }

        [TestMethod]
        public void TestGetById()
        {
            var client = _repository.Query().First(c => c.Id == 1);

            Assert.IsTrue(client.Id == 1);
        }

        private List<Client> SetUpClients()
        {
            var clients = new List<Client>();

            for (int i = 1; i < 5; i++)
            {
                var client = new Client
                {
                    Id = i,
                    Name = $"Client {1}",               
                };

                clients.Add(client);
            }

            return clients;
        }
    }
}
