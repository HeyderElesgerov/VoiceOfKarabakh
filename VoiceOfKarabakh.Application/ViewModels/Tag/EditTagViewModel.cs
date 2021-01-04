using System.Collections.Generic;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.Application.ViewModels.Tag
{
    public class EditTagViewModel
    {
        public int TagId { get; set; }
        public IEnumerable<EditLocalizationViewModel> TagTitleTranslations { get; set; }
    }
}
