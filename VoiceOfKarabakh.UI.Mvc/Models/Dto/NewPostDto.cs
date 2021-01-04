using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VoiceOfKarabakh.Application.ViewModels.Localization;
using VoiceOfKarabakh.Application.ViewModels.SaveFile;

namespace VoiceOfKarabakh.UI.Mvc.Models.Dto
{
    public class NewPostDto
    {
        [Required]
        public int ReadingTime { get; set; }

        public bool Drafted { get; set; }

        [Required]
        public IFormFile Photo { get; set; }

        public List<int> SelectedCategoryIds { get; set; }

        public List<int> SelectedTagIds { get; set; }

        public List<NewLocalizationViewModel> TitleLocalizations { get; set; }

        public List<NewLocalizationViewModel> MetaTitleLocalizations { get; set; }

        public List<NewLocalizationViewModel> ContentLocalizations { get; set; }
    }
}
