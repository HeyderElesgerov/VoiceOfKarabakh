using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VoiceOfKarabakh.Application.ViewModels.Account
{
    public class AccountConfirmationViewModel
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
