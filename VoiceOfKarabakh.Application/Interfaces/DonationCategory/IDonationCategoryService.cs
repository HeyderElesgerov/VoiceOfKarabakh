using System;
using System.Collections.Generic;
using System.Text;
using VoiceOfKarabakh.Application.ViewModels.DonationCategory;

namespace VoiceOfKarabakh.Application.Interfaces.DonationCategory
{
    public interface IDonationCategoryService
    {
        void Add(NewDonationCategoryViewModel newDonationCategoryViewModel);

        void Delete(int id);

        DonationCategoryViewModel GetDonationCategoryViewModel(int id, string cultureCode, string includes = null);

        IEnumerable<DonationCategoryViewModel> GetDonationCategoryViewModels(string cultureCode, string includes = null);

        EditDonationCategoryViewModel GetEditDonationCategoryViewModel(int id);

        bool Exists(int id);

        void Save();
    }
}
