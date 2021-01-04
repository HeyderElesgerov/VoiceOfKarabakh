using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VoiceOfKarabakh.Application.ViewModels.Localization;
using VoiceOfKarabakh.Application.ViewModels.SaveFile;

namespace VoiceOfKarabakh.Application.ViewModels.Donation
{
    public class NewDonationViewModel
    {
        public int SelectedDonationCategoryId { get; set; }

        public NewSaveFileViewModel NewHeaderPhoto { get; set; }

        public List<NewLocalizationViewModel> TitleLocalizations { get; set; }
        public List<NewLocalizationViewModel> MetaTitleLocalizations { get; set; }
        public List<NewLocalizationViewModel> ContentLocalizations { get; set; }
    }
}
