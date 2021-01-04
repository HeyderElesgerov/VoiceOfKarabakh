using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.Application.ViewModels.Tag
{
    public class NewTagViewModel
    {
        [Required]
        public List<NewLocalizationViewModel> TagTitleTranslations { get; set; }
    }
}
