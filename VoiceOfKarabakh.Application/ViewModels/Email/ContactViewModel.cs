using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VoiceOfKarabakh.Application.ViewModels.Email
{
    public class ContactViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string From { get; set; }

        [Required]
        public string Topic { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
