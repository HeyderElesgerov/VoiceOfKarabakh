using System;
using System.Collections.Generic;
using System.Text;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.Application.ViewModels.DonationCategory
{
    public class EditDonationCategoryViewModel
    {
        public int DonationCategoryId { get; set; }
        public List<EditLocalizationViewModel> TitleTranslations { get; set; }
    }
}
