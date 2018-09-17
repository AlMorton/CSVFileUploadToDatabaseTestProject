using CSVUploadToDataProject.EntityFramework.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVUploadToDataTestProject.EntityFramework.DomainModel
{
    public class Client : IHasId<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CSVData> CSVData { get; set; }

        public virtual ICollection<Site> Sites { get; set; }
    }
}
