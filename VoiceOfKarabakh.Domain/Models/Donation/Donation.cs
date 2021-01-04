namespace VoiceOfKarabakh.Domain.Models.Donation
{
    public class Donation
    {
        public int Id { get; set; }
        public int DonationCategoryId { get; set; }

        public DonationCategory.DonationCategory DonationCategory { get; set; }

        public LocalizationSet TitleLocalizationSet { get; set; }
        public LocalizationSet MetaTitleLocalizationSet { get; set; }
        public LocalizationSet ContentLocalizationSet { get; set; }

        public SavedFile HeaderPhoto { get; set; }
    }
}
