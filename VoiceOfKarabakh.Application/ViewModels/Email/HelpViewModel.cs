using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VoiceOfKarabakh.Application.ViewModels.Email
{
    //for those whome want to help
    public class HelpViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public IFormFile Cv{ get; set; }

        public IEnumerable<IFormFile> Documents { get; set; }

        public string Message { get; set; }
    }
}
