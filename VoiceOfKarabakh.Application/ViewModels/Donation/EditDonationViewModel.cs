using System.Collections.Generic;
using VoiceOfKarabakh.Application.ViewModels.Localization;
using VoiceOfKarabakh.Application.ViewModels.SaveFile;
using VoiceOfKarabakh.Domain.Models;

namespace VoiceOfKarabakh.Application.ViewModels.Donation
{
    public class EditDonationViewModel
    {
        public int Id { get; set; }

        public int DonationCategoryId { get; set; }

        public int PhotoId { get; set; }

        public string PhotoPath { get; set; }

        public List<EditLocalizationViewModel> TitleLocalizations { get; set; }
        public List<EditLocalizationViewModel> MetaTitleLocalizations { get; set; }
        public List<EditLocalizationViewModel> ContentLocalizations { get; set; }
    }
}
