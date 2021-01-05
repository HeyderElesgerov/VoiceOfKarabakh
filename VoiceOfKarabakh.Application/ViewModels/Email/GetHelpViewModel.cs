using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VoiceOfKarabakh.Application.ViewModels.Email
{
    public class GetHelpViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Status { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Course { get; set; }

        [Required]
        public string Address { get; set; }

        public IEnumerable<IFormFile> Documents { get; set; }

        public string Note { get; set; }
    }
}
