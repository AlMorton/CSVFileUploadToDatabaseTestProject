using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CSVUploadToDataTestProject.Models.UploadFile;
using CSVUploadToDataTestProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSVUploadToDataTestProject.Controllers
{
    public class UploadFileController : Controller
    {
        private readonly IFileUploadFacade _fileUploadFacade;

        public UploadFileController(IFileUploadFacade fileUploadFacade)
        {
            _fileUploadFacade = fileUploadFacade;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upload(UploadFileFormModel form)
        {   
            if(!form.File.FileName.Contains(".csv") || form.File.Length > 1024)
            {
                return Redirect("Index");
            }
                      
            var result = await _fileUploadFacade.ParseFileAsync(form.File);

            return RedirectToAction("UploadResult", result );
        }

        public IActionResult UploadResult(List<CSVDataDTO> cSVDataDTOs)
        {
            return View(cSVDataDTOs);
        }
    }
}