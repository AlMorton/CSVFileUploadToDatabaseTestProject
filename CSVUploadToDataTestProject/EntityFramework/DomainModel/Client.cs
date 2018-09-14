using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVUploadToDataTestProject.EntityFramework.DomainModel
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CSVData> CSVData { get; set; }

        public ICollection<Site> Sites { get; set; }
    }
}
