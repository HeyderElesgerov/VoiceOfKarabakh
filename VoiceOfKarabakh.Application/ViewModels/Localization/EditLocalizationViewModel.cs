using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceOfKarabakh.Application.ViewModels.Localization
{
    public class EditLocalizationViewModel
    {
        public int LocalizationSetId { get; set; }
        public string CultureCode { get; set; }
        public string Value { get; set; }
    }
}
