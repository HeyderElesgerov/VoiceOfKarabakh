using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.UI.Mvc.Models.Dto
{
    public class EditDonationDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int DonationCategoryId { get; set; }

        [Required, Range(0, double.MaxValue)]
        public int Price { get; set; }

        public string CurrentPhotoPath { get; set; }

        public int CurrentPhotoId { get; set; }

        public IFormFile NewPhoto { get; set; }

        public List<EditLocalizationViewModel> TitleLocalizations { get; set; }
        public List<EditLocalizationViewModel> MetaTitleLocalizations { get; set; }
        public List<EditLocalizationViewModel> ContentLocalizations { get; set; }
    }
}
