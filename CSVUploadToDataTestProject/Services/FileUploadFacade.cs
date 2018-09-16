using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSVUploadToDataTestProject.Services
{
    public interface IFileUploadFacade
    {
        Task<List<string>> ParseFileAsync(IFormFile file);
    }

    public class FileUploadFacade : IFileUploadFacade
    {
        private List<string> DataInFile { get; set; } = new List<string>();

        public async Task<List<string>> ParseFileAsync(IFormFile file)
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
                        DataInFile.Add(line);
                    }
                    ms.Flush();
                }
            }

            return DataInFile;
        }

        public void ContructCSVViewModel()
        {
            foreach (var line in DataInFile)
            {

            }
        } 

        private void ParseLineToModel(string line)
        {
            var lineContents = line.Split(',');
        }
    }
}
