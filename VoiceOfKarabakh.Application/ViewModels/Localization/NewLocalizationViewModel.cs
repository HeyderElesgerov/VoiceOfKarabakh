using System.ComponentModel.DataAnnotations;

namespace VoiceOfKarabakh.Application.ViewModels.Localization
{
    public class NewLocalizationViewModel
    {
        [Required]
        public string CultureCode { get; set; }

        public string Value { get; set; }
    }
}
