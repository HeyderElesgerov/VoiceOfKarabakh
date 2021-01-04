using System;
using System.Collections.Generic;
using System.Text;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.Application.ViewModels.Category
{
    public class EditCategoryViewModel
    {
        public int CategoryId { get; set; }
        public IEnumerable<EditLocalizationViewModel> CategoryTitleTranslations { get; set; }
    }
}
