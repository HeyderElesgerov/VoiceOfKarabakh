using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.UI.Mvc.Models.Dto
{
    public class EditGalleryDto
    {
        [Required]
        public int GalleryId { get; set; }

        public List<EditLocalizationViewModel> TitleLocalizations { get; set; }

        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}
