namespace VoiceOfKarabakh.Domain.Models
{
    public class Localization
    {
        public string CultureCode { get; set; }

        public string Value { get; set; }

        public int LocalizationSetId { get; set; }
        public virtual LocalizationSet LocalizationSet { get; set; }
    }
}
