using CSVUploadToDataProject.EntityFramework.Repository;
using System;

namespace CSVUploadToDataTestProject.EntityFramework.DomainModel
{
    public class CSVData : IHasId<int>
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }        

        public int FooData { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public int SiteId { get; set; }

        public Site Site { get; set; }
    }
}
