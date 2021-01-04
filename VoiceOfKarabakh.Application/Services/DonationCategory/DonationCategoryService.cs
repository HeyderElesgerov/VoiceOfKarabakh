using System.Collections.Generic;
using System.Linq;
using VoiceOfKarabakh.Application.Interfaces.DonationCategory;
using VoiceOfKarabakh.Application.ViewModels.DonationCategory;
using VoiceOfKarabakh.Domain.Interfaces.DonationCategory;

namespace VoiceOfKarabakh.Application.Services.DonationCategory
{
    public class DonationCategoryService : IDonationCategoryService
    {
        private readonly IDonationCategoryRepository _donationCategoryRepository;

        public DonationCategoryService(IDonationCategoryRepository donationCategoryRepository)
        {
            _donationCategoryRepository = donationCategoryRepository;
        }

        public void Add(NewDonationCategoryViewModel newDonationCategoryViewModel)
        {
            Domain.Models.DonationCategory.DonationCategory newDonationCategory = new Domain.Models.DonationCategory.DonationCategory()
            {
                TitleLocalizationSet = new Domain.Models.LocalizationSet() { Localizations = new List<Domain.Models.Localization>() }
            };

            foreach (var translation in newDonationCategoryViewModel.TitleTranslations)
            {
                newDonationCategory.TitleLocalizationSet.Localizations.Add(new Domain.Models.Localization()
                {
                    CultureCode = translation.CultureCode,
                    Value = translation.Value != null ? translation.Value : ""
                });
            }

            _donationCategoryRepository.Add(newDonationCategory);
        }

        public void Delete(int id)
        {
            _donationCategoryRepository.Delete(id);
        }

        public bool Exists(int id)
        {
            return _donationCategoryRepository.Exists(id);
        }

        public DonationCategoryViewModel GetDonationCategoryViewModel(int id, string cultureCode, string includes = null)
        {
            var donationCategory = _donationCategoryRepository.Get(id, includes);

            var titleLoc = donationCategory.TitleLocalizationSet.Localizations
                                .FirstOrDefault(l => l.CultureCode == cultureCode);

            var vm = new DonationCategoryViewModel()
            {
                Id = id,
                Title = titleLoc != null ? titleLoc.Value : ""
            };

            return vm;
        }

        public IEnumerable<DonationCategoryViewModel> GetDonationCategoryViewModels(string cultureCode, string includes = null)
        {
            var donationCategories = _donationCategoryRepository.GetAll(includes);

            var donationCategoriesVMs = new List<DonationCategoryViewModel>();

            foreach (var donationCategory in donationCategories)
            {
                donationCategoriesVMs.Add(GetDonationCategoryViewModel(donationCategory.Id, cultureCode, includes));
            }

            return donationCategoriesVMs;
        }

        public EditDonationCategoryViewModel GetEditDonationCategoryViewModel(int id)
        {
            var donationCategory = _donationCategoryRepository.Get(id, "TitleLocalizationSet.Localizations");
            var vm = new EditDonationCategoryViewModel()
            {
                DonationCategoryId = id,
                TitleTranslations = new List<ViewModels.Localization.EditLocalizationViewModel>()
            };

            foreach(var loc in donationCategory.TitleLocalizationSet.Localizations)
            {
                vm.TitleTranslations.Add(new ViewModels.Localization.EditLocalizationViewModel()
                {
                    CultureCode = loc.CultureCode,
                    LocalizationSetId = loc.LocalizationSetId,
                    Value = loc.Value
                });
            }

            return vm;
        }

        public void Save()
        {
            _donationCategoryRepository.Save();
        }
    }
}
