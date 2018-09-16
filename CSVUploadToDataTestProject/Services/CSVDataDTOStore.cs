using CSVUploadToDataTestProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVUploadToDataProject.Services
{
    public interface ICSVDataDTOStore
    {
        List<CSVDataDTO> CSVDataDTOs { get; set; } 
    }    

    public class CSVDataDTOStore : ICSVDataDTOStore
    {
        public List<CSVDataDTO> CSVDataDTOs { get; set;} = new List<CSVDataDTO>();
    }
}
