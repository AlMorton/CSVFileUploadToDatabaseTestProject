using CSVUploadToDataTestProject.EntityFramework.DomainModel;
using CSVUploadToDataTestProject.Services;
using System.Collections.Generic;

namespace CSVUploadToDataProject.Services
{
    public class CSVDataCreationResult
    {
        public List<string> ClientsThatNeedCreating { get; set; }
        
        public List<string> SitesThatNeedCreating { get; set; }
        
        public List<CSVDataDTO> SuccessResult { get; set; }
    }
}
