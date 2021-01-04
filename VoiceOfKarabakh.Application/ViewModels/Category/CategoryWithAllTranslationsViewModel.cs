using System.Collections.Generic;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.Application.ViewModels.Category
{
    public class CategoryWithAllTranslationsViewModel
    {
        public int CategoryId { get; set; }
        public List<LocalizationViewModel> LocalizationViewModels { get; set; }
    }
}
