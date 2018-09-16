using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CSVUploadToDataProject.Models.UploadFile;
using CSVUploadToDataProject.Services;
using CSVUploadToDataTestProject.Models.UploadFile;
using CSVUploadToDataTestProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CSVUploadToDataTestProject.Controllers
{
    [Authorize]
    public class UploadFileController : Controller
    {
        private readonly IFileUploadFacade _fileUploadFacade;
        private readonly ICSVDataDTOStore _iCSVDataDTOStore;

        public UploadFileController(IFileUploadFacade fileUploadFacade,
            ICSVDataDTOStore iCSVDataDTOStore)
        {
            _fileUploadFacade = fileUploadFacade;
            _iCSVDataDTOStore = iCSVDataDTOStore;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(UploadFileFormModel form)
        {   
            if(!form.File.FileName.Contains(".csv") || form.File.Length > 1024)
            {
                return Redirect("Index");
            }
                      
            var result = await _fileUploadFacade.ParseFileAsync(form.File);

            _iCSVDataDTOStore.CSVDataDTOs = result;

            return RedirectToAction("UploadResult");
        }

        public IActionResult UploadResult()
        {
            var model = new UploadResultModel();
            model.CSVDataDTOs = _iCSVDataDTOStore.CSVDataDTOs;
            return View(model);
        }
    }
}