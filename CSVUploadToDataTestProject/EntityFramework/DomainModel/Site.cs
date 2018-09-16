using CSVUploadToDataProject.EntityFramework.Repository;
using System.Collections.Generic;

namespace CSVUploadToDataTestProject.EntityFramework.DomainModel
{
    public class Site : IHasId<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public ICollection<CSVData> CSVData { get; set; }
    }
}
