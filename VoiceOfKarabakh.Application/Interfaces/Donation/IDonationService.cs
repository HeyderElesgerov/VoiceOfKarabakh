using System;
using System.Collections.Generic;
using System.Text;
using VoiceOfKarabakh.Application.ViewModels.Donation;

namespace VoiceOfKarabakh.Application.Interfaces.Donation
{
    public interface IDonationService
    {
        IEnumerable<DonationViewModel> GetDonationViewModels(string cultureCode, string includes = null);
        IEnumerable<DonationViewModel> GetDonationViewModelsByDonationCategory(int categoryId, string cultureCode, string includes = null);
        DonationViewModel GetDonationViewModel(int id, string cultureCode, string includes = null);
        EditDonationViewModel GetEditDonationViewModel(int id);
        void AddNew(NewDonationViewModel newDonationViewModel);
        void Edit(EditDonationViewModel editDonationViewModel);
        void Delete(int id);
        bool Exists(int id);
        void Save();
    }
}
