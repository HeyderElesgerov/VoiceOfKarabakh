using System.Collections.Generic;

namespace VoiceOfKarabakh.Domain.Interfaces.DonationCategory
{
    public interface IDonationCategoryRepository
    {
        void Add(Models.DonationCategory.DonationCategory newDonationCategory);

        bool Exists(int id);

        void Delete(int id);

        Models.DonationCategory.DonationCategory Get(int id, string includes = null);

        IEnumerable<Models.DonationCategory.DonationCategory> GetAll(string includes = null);

        void Save();
    }
}
