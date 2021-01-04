using System.Collections.Generic;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.Application.ViewModels.Tag
{
    public class TagWithAllTranslationsViewModel
    {
        public int TagId { get; set; }
        public List<LocalizationViewModel> LocalizationViewModels { get; set; }
    }
}
