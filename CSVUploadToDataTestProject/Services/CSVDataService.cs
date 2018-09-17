using CSVUploadToDataProject.EntityFramework.Repository;
using CSVUploadToDataTestProject.EntityFramework.DomainModel;
using CSVUploadToDataTestProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVUploadToDataProject.Services
{
    public interface ICSVDataService
    {

    }

    public class CSVDataService
    {
        private List<CSVDataDTO> DataInFile { get; set; }

        private List<CSVData> CSVDataEntities { get; set; }

        private IQueryable<Site> Sites { get; set;  }

        private IQueryable<Client> Clients { get; set; }

        private readonly IRepostory<CSVData, int> _repostory;

        public CSVDataService(IRepostory<CSVData, int> repostory)
        {
            _repostory = repostory;

            CSVDataEntities = new List<CSVData>();
        }

        public void SaveToDatabase(List<CSVDataDTO> dataInFile)
        {
            List<CSVDataDTO> DataInFile = dataInFile;

            GetSites();

            GetClients();


            foreach (var dataDTO in DataInFile)
            {
                var csvData = new CSVData
                {
                    Client = Clients.First(c => c.Id == dataDTO.ClientId),
                    Date = dataDTO.Date,
                    FooData = dataDTO.FooData,
                    Site = Sites.First(s => s.Name == dataDTO.Site),
                };

                CSVDataEntities.Add(csvData);
            }


        }

        private void GetSites()
        {
            var siteNames = DataInFile.Select(d => d.Site);

            Sites = _repostory.DbContext().Sites.AsQueryable().Where(s => siteNames.Contains(s.Name));
        }

        private void GetClients()
        {
            var clientIds = DataInFile.Select(d => d.ClientId);

            Clients = _repostory.DbContext().Clients.AsQueryable().Where(c => clientIds.Contains(c.Id));
        }
    }    

    public class CSVDataCreationResult
    {
        List<string> ClientsThatNeedCreating { get; set; }

        List<string> SitesThatNeedCreating { get; set; }

        List<CSVData> SuccessResult { get; set; }
    }
}
