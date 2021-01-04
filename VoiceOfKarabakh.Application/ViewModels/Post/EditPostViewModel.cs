using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.Application.ViewModels.Post
{
    public class EditPostViewModel
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        public int ReadingTime { get; set; }

        public bool Drafted { get; set; }

        public IFormFile NewHeaderPhoto { get; set; }

        [Required]
        public string CurrentPhotoPath { get; set; }

        [Required]
        public int CurrentSaveFileId { get; set; }

        public List<int> SelectedCategoryIds { get; set; }

        public List<int> SelectedTagIds { get; set; }

        public List<EditLocalizationViewModel> TitleLocalizations { get; set; }
        public List<EditLocalizationViewModel> MetaTitleLocalizations { get; set; }
        public List<EditLocalizationViewModel> ContentLocalizations { get; set; }
    }
}
