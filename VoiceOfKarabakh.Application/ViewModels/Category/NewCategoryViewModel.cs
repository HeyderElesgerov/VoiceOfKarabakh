using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.Application.ViewModels.Category
{
    public class NewCategoryViewModel
    {
        [Required]
        public List<NewLocalizationViewModel> CategoryTitleTranslations { get; set; }
    }
}
