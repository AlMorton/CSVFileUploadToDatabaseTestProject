using CSVUploadToData.Tests.MockSetUp;
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

        private readonly MockDBContext<Client> _MockDBContext;        

        public RepositoryUnitTests()
        {
            _MockDBContext = new MockDBContext<Client>(SetUpClients());

            // Setup the client list
            _MockDBContext._dbContext.Setup(e => e.Clients).Returns(_MockDBContext.MockDbSet.Object);

            _repository = new Repository<Client, int>(_MockDBContext._dbContext.Object);           
            
        }

        [TestMethod]
        public void TestGetById()
        {
            var client = _repository.Query().First(c => c.Id == 1);

            Assert.IsTrue(client.Id == 1);
        }

        [TestMethod]
        public void TestSaveMany()
        {
            var clients = SetUpClients();

            var result = _repository.SaveManyAsync(clients).Result;

            Assert.IsTrue(result == 4);
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
