using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using VoiceOfKarabakh.Application.Interfaces.Donation;
using VoiceOfKarabakh.Application.ViewModels.Donation;
using VoiceOfKarabakh.Domain.Interfaces.Donation;

namespace VoiceOfKarabakh.Application.Services.Donation
{
    public class DonationService : IDonationService
    {
        private readonly IDonationRepository _donationRepository;

        public DonationService(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        public void AddNew(NewDonationViewModel newDonationViewModel)
        {
            Domain.Models.Donation.Donation newDonation = new Domain.Models.Donation.Donation()
            {
                DonationCategoryId = newDonationViewModel.SelectedDonationCategoryId,
                HeaderPhoto = new Domain.Models.SavedFile() { FilePath = newDonationViewModel.NewHeaderPhoto.Path },
                TitleLocalizationSet = new Domain.Models.LocalizationSet()
                {
                    Localizations = new List<Domain.Models.Localization>()
                },
                MetaTitleLocalizationSet = new Domain.Models.LocalizationSet()
                {
                    Localizations = new List<Domain.Models.Localization>()
                },
                ContentLocalizationSet = new Domain.Models.LocalizationSet()
                {
                    Localizations = new List<Domain.Models.Localization>()
                }
            };

            foreach (var newLocVM in newDonationViewModel.TitleLocalizations)
            {
                newDonation.TitleLocalizationSet.Localizations.Add(new Domain.Models.Localization()
                {
                    CultureCode = newLocVM.CultureCode,
                    Value = newLocVM.Value != null ? newLocVM.Value : ""
                });
            }

            foreach (var newLocVM in newDonationViewModel.MetaTitleLocalizations)
            {
                newDonation.MetaTitleLocalizationSet.Localizations.Add(new Domain.Models.Localization()
                {
                    CultureCode = newLocVM.CultureCode,
                    Value = newLocVM.Value != null ? newLocVM.Value : ""
                });
            }

            foreach (var newLocVM in newDonationViewModel.ContentLocalizations)
            {
                newDonation.ContentLocalizationSet.Localizations.Add(new Domain.Models.Localization()
                {
                    CultureCode = newLocVM.CultureCode,
                    Value = newLocVM.Value != null ? newLocVM.Value : ""
                });
            }

            _donationRepository.Add(newDonation);
        }

        public EditDonationViewModel GetEditDonationViewModel(int id)
        {
            if (!Exists(id))
                throw new Exception("Not found");

            var donation = _donationRepository.Get(id, "HeaderPhoto,TitleLocalizationSet.Localizations,MetaTitleLocalizationSet.Localizations,ContentLocalizationSet.Localizations");

            EditDonationViewModel editDonationViewModel = new EditDonationViewModel()
            {
                Id = id,
                DonationCategoryId = donation.DonationCategoryId,
                PhotoPath = donation.HeaderPhoto.FileName,
                PhotoId = donation.HeaderPhoto.Id,
                ContentLocalizations = new List<ViewModels.Localization.EditLocalizationViewModel>(),
                TitleLocalizations = new List<ViewModels.Localization.EditLocalizationViewModel>(),
                MetaTitleLocalizations = new List<ViewModels.Localization.EditLocalizationViewModel>()
            };

            foreach(var titleTranslation in donation.TitleLocalizationSet.Localizations)
            {
                editDonationViewModel.TitleLocalizations.Add(new ViewModels.Localization.EditLocalizationViewModel()
                {
                    CultureCode = titleTranslation.CultureCode,
                    Value = titleTranslation.Value,
                    LocalizationSetId = titleTranslation.LocalizationSetId
                });
            }

            foreach (var metaTitleTranslation in donation.MetaTitleLocalizationSet.Localizations)
            {
                editDonationViewModel.MetaTitleLocalizations.Add(new ViewModels.Localization.EditLocalizationViewModel()
                {
                    CultureCode = metaTitleTranslation.CultureCode,
                    Value = metaTitleTranslation.Value,
                    LocalizationSetId = metaTitleTranslation.LocalizationSetId
                });
            }

            foreach (var contentTranslation in donation.ContentLocalizationSet.Localizations)
            {
                editDonationViewModel.ContentLocalizations.Add(new ViewModels.Localization.EditLocalizationViewModel()
                {
                    CultureCode = contentTranslation.CultureCode,
                    Value = contentTranslation.Value,
                    LocalizationSetId = contentTranslation.LocalizationSetId
                });
            }

            return editDonationViewModel;
        }

        public void Delete(int id)
        {
            _donationRepository.Delete(id);
        }

        public void Edit(EditDonationViewModel editDonationViewModel)
        {
            Domain.Models.Donation.Donation donation = new Domain.Models.Donation.Donation()
            {
                Id = editDonationViewModel.Id,
                DonationCategoryId = editDonationViewModel.DonationCategoryId
            };

            _donationRepository.Update(donation.Id, donation);
        }

        public bool Exists(int id)
        {
            return _donationRepository.Exists(id);
        }

        public IEnumerable<DonationViewModel> GetDonationViewModels(string cultureCode, string includes = null)
        {
            List<DonationViewModel> donationViewModels = new List<DonationViewModel>();

            var donations = _donationRepository.GetAll(includes);

            foreach (var donation in donations)
            {
                var donationVM = new DonationViewModel()
                {
                    Id = donation.Id
                };

                if (donation.TitleLocalizationSet != null)
                {
                    var loc = donation.TitleLocalizationSet.Localizations
                                      .FirstOrDefault(l => l.CultureCode == cultureCode);

                    donationVM.Title = loc != null ? loc.Value : "";
                }

                if (donation.DonationCategory != null && donation.DonationCategory.TitleLocalizationSet != null)
                {
                    var loc = donation.DonationCategory.TitleLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                    donationVM.DonationCategoryTitle = loc != null ? loc.Value : "";
                }

                donationViewModels.Add(donationVM);
            }

            return donationViewModels;
        }

        public void Save()
        {
            _donationRepository.Save();
        }

        public IEnumerable<DonationViewModel> GetDonationViewModelsByDonationCategory(int categoryId, string cultureCode, string includes = null)
        {
            List<DonationViewModel> donationViewModels = new List<DonationViewModel>();

            var donations = _donationRepository.GetByDonationCategory(categoryId, includes);

            foreach (var donation in donations)
            {
                var donationVM = new DonationViewModel()
                {
                    Id = donation.Id
                };

                if (donation.TitleLocalizationSet != null)
                {
                    var loc = donation.TitleLocalizationSet.Localizations
                                      .FirstOrDefault(l => l.CultureCode == cultureCode);

                    donationVM.Title = loc != null ? loc.Value : "";
                }

                if(donation.MetaTitleLocalizationSet != null)
                {
                    var loc = donation.MetaTitleLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                    donationVM.MetaTitle = loc != null ? loc.Value : "";
                }

                if (donation.HeaderPhoto != null)
                {
                    donationVM.PhotoName = donation.HeaderPhoto.FileName;
                }

                donationViewModels.Add(donationVM);
            }

            return donationViewModels;

        }

        public DonationViewModel GetDonationViewModel(int id, string cultureCode, string includes = null)
        {
            var donation = _donationRepository.Get(id, includes);

            DonationViewModel donationVM = new DonationViewModel()
            {
                Id = id,
                Title = "",
                DonationCategoryTitle = ""
            };

            if(donation.DonationCategory != null)
            {
                var donationCategoryTitleLocSet = donation.DonationCategory.TitleLocalizationSet;

                if(donationCategoryTitleLocSet != null)
                {
                    var loc = donationCategoryTitleLocSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                    if(loc != null)
                    {
                        donationVM.DonationCategoryTitle = loc.Value;
                    }
                }
            }

            if(donation.TitleLocalizationSet != null && donation.TitleLocalizationSet.Localizations != null)
            {
                var donationTitleLoc = donation.TitleLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                if(donationTitleLoc != null)
                {
                    donationVM.Title = donationTitleLoc.Value;
                }
            }

            return donationVM;
        }
    }
}
