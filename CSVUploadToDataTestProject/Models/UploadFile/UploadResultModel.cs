﻿using CSVUploadToDataTestProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVUploadToDataProject.Models.UploadFile
{
    public class UploadResultModel
    {
        public List<CSVDataDTO> CSVDataDTOs { get; set; }
    }
}
