using System;
using System.Collections.Generic;
using System.Text;
using VoiceOfKarabakh.Application.Interfaces.Localization;
using VoiceOfKarabakh.Application.ViewModels.Localization;
using VoiceOfKarabakh.Domain.Interfaces.Localization;
using VoiceOfKarabakh.Domain.Factory.Localization;

namespace VoiceOfKarabakh.Application.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private readonly ILocalizationRepository _localizationRepository;
        private LocalizationFactory _localizationFactory;

        public LocalizationService(ILocalizationRepository localizationRepository, LocalizationFactory localizationFactory)
        {
            _localizationRepository = localizationRepository;
            _localizationFactory = localizationFactory;
        }

        public void Delete(int setId, string cultureCode)
        {
            _localizationRepository.Delete(setId, cultureCode);
        }

        public bool Exists(int setId, string cultureCode)
        {
            return _localizationRepository.Exists(setId, cultureCode);
        }

        public LocalizationViewModel GetLocalization(int setId, string cultureCode)
        {
            var localization = _localizationRepository.GetLocalization(setId, cultureCode);

            return new LocalizationViewModel()
            {
                CultureCode = localization.CultureCode,
                Value = localization.Value,
                LocalizationSetId = localization.LocalizationSetId
            };
        }

        public IEnumerable<LocalizationViewModel> GetLocalizationViewModelsBySet(int setId)
        {
            var localizationsBySet = _localizationRepository.GetLocalizationsBySet(setId);

            List<LocalizationViewModel> localizationVMs = new List<LocalizationViewModel>();

            foreach(var localization in localizationsBySet)
            {
                localizationVMs.Add(new LocalizationViewModel()
                {
                    CultureCode = localization.CultureCode,
                    Value = localization.Value,
                    LocalizationSetId = localization.LocalizationSetId
                });
            }

            return localizationVMs;
        }

        public void Save()
        {
            _localizationRepository.Save();
        }

        public void Update(EditLocalizationViewModel editLocalizationViewModel)
        {
            string cultureCode = editLocalizationViewModel.CultureCode;
            int setId = editLocalizationViewModel.LocalizationSetId;
            string newValue = editLocalizationViewModel.Value != null ? editLocalizationViewModel.Value : "";

            var localization = _localizationFactory.GetLocalization(cultureCode, newValue, setId);

            _localizationRepository.Update(localization);
        }

        public void Add(int setId, NewLocalizationViewModel newLocalizationViewModel)
        {
            string cultureCode = newLocalizationViewModel.CultureCode;
            string value = newLocalizationViewModel.Value != null ? newLocalizationViewModel.Value : "";
            var newLoc = _localizationFactory.GetLocalization(cultureCode, value, setId);

            _localizationRepository.AddLocalization(newLoc);
        }
    }
}
