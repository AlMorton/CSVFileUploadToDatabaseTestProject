using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSVUploadToDataTestProject.Models.Account
{
    public class LogInViewModel
    {
        [Required]
        public string Username { get; set; } = "Test";

        [Required]
        public string Password { get; set; } = "Password1";   
        
        public string ReturnUrl { get; set; }
    }
}
