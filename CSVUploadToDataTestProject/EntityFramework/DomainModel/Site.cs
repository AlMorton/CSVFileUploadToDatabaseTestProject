using System.Collections.Generic;

namespace CSVUploadToDataTestProject.EntityFramework.DomainModel
{
    public class Site
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public ICollection<CSVData> CSVData { get; set; }
    }
}
