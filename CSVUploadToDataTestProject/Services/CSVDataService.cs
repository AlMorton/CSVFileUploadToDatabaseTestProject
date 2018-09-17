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
        CSVDataCreationResult SaveToDatabase(List<CSVDataDTO> dataInFile);
    }

    public sealed class CSVDataService
    {
        private List<CSVDataDTO> DataInFile { get; set; }

        private List<CSVData> CSVDataEntities { get; set; }

        private IQueryable<Site> Sites { get; set;  }

        private IQueryable<Client> Clients { get; set; }

        private IEnumerable<CSVDataDTO> ClientsThatDoNotExist { get; set; }

        private IEnumerable<CSVDataDTO> SitesThatDoNotExist { get; set; }

        private readonly IRepostory<CSVData, int> _repostory;

        public CSVDataService(IRepostory<CSVData, int> repostory)
        {
            _repostory = repostory;

            CSVDataEntities = new List<CSVData>();
        }

        public CSVDataCreationResult SaveToDatabase(List<CSVDataDTO> dataInFile)
        {
            List<CSVDataDTO> DataInFile = dataInFile;

            GetSites();

            GetClients();

            SetClientsThatDoNotExist();

            SetSitesThatDoNotExist();

            RemoveData();

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

            _repostory.SaveManyAsync(CSVDataEntities);

            var success = new CSVDataCreationResult {

                SuccessResult = DataInFile,
                SitesThatNeedCreating = SitesThatDoNotExist.Select(s => s.Site).ToList()

            };

            return success;
        }

        public void RemoveData()
        {
            DataInFile.RemoveAll(d => ClientsThatDoNotExist.Any(c => c.ClientId == d.ClientId) == true || 
                                 SitesThatDoNotExist.Any(s => s.Site == d.Site) == true);
        }

        public void GetSites()
        {
            var siteNames = DataInFile.Select(d => d.Site);

            Sites = _repostory.DbContext().Sites.AsQueryable().Where(s => siteNames.Contains(s.Name));
        }

        public void GetClients()
        {
            var clientIds = DataInFile.Select(d => d.ClientId);

            Clients = _repostory.DbContext().Clients.AsQueryable().Where(c => clientIds.Contains(c.Id));
        }

        public void SetClientsThatDoNotExist()
        {
           ClientsThatDoNotExist = DataInFile.Where(d => !Clients.Any(c => c.Id == d.ClientId)).AsEnumerable();
           
        }

        public void SetSitesThatDoNotExist()
        {
            SitesThatDoNotExist = DataInFile.Where(d => !Sites.Any(s => s.Name == d.Site)).AsEnumerable();
        }
    }    
}
