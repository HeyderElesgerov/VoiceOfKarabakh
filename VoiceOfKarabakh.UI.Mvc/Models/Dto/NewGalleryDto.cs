using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.UI.Mvc.Models.Dto
{
    public class NewGalleryDto
    {
        public List<NewLocalizationViewModel> TitleLocalizations { get; set; }

        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}
