using System.Collections.Generic;

namespace VoiceOfKarabakh.Domain.Interfaces.Donation
{
    public interface IDonationRepository
    {
        Models.Donation.Donation Get(int id, string includes = null);
        IEnumerable<Models.Donation.Donation> GetAll(string includes = null);
        IEnumerable<Models.Donation.Donation> GetByDonationCategory(int donationCategoryId, string includes = null);
        void Add(Models.Donation.Donation donation);
        void Update(int id, Models.Donation.Donation donation);
        void Delete(int id);
        bool Exists(int id);
        void Save();
    }
}
