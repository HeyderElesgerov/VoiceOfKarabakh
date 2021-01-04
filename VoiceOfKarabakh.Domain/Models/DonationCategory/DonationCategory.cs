using System.Collections.Generic;

namespace VoiceOfKarabakh.Domain.Models.DonationCategory
{
    public class DonationCategory
    {
        public int Id { get; set; }
        public LocalizationSet TitleLocalizationSet { get; set; }

        public ICollection<Donation.Donation> Donations { get; set; }
    }
}
