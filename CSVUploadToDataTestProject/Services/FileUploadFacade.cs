using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSVUploadToDataTestProject.Services
{
    public interface IFileUploadFacade
    {
        Task<List<CSVDataDTO>> ParseFileAsync(IFormFile file);
    }

    public class FileUploadFacade : IFileUploadFacade
    {
        private List<CSVDataDTO> DataInFile { get; set; } = new List<CSVDataDTO>();        

        public async Task<List<CSVDataDTO>> ParseFileAsync(IFormFile file)
        {   

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);

                    ms.Seek(0, SeekOrigin.Begin);

                    StreamReader sr = new StreamReader(ms);

                    string line;

                    // Read first line headers
                    await sr.ReadLineAsync();

                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        var dto = ConvertToCSVDataDTO(ParseLine(line));
                        DataInFile.Add(dto);
                    }
                    ms.Flush();
                }
            }

            return DataInFile;
        }       

        public string[] ParseLine(string line)
        {
            var lineContents = line.Split(',');

            return lineContents;
        }

        public CSVDataDTO ConvertToCSVDataDTO(string[] strings)
        {
            CSVDataDTO cSVDataDTO = new CSVDataDTO();

            cSVDataDTO.ClientId = int.Parse(strings[0]);
            cSVDataDTO.Site = strings[1];


            var dateTimeStyle = DateTimeStyles.AssumeUniversal;        

            DateTime date;

            DateTime.TryParse(strings[2], new CultureInfo("en-GB"), dateTimeStyle, out date);

            cSVDataDTO.Date = date;

            cSVDataDTO.FooData = int.Parse(strings[3]);

            return cSVDataDTO;
        }       
    }
}
