using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.UI.Mvc.Models.Dto
{
    public class NewDonationDto
    {
        [Required]
        public int SelectedDonationCategoryId { get; set; }

        [Required, Range(0, Double.MaxValue)]
        public int Price { get; set; }

        [Required]
        public IFormFile Photo { get; set; }

        public List<NewLocalizationViewModel> TitleLocalizations { get; set; }
        public List<NewLocalizationViewModel> MetaTitleLocalizations { get; set; }
        public List<NewLocalizationViewModel> ContentLocalizations { get; set; }
    }
}
