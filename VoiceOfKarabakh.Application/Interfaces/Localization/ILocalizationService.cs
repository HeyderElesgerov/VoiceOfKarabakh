using System;
using System.Collections.Generic;
using System.Text;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.Application.Interfaces.Localization
{
    public interface ILocalizationService
    {
        LocalizationViewModel GetLocalization(int setId, string cultureCode);
        IEnumerable<LocalizationViewModel> GetLocalizationViewModelsBySet(int setId);

        void Update(EditLocalizationViewModel editLocalizationViewModel);
        void Add(int setId, NewLocalizationViewModel newLocalizationViewModel);
        void Delete(int setId, string cultureCode);
        bool Exists(int setId, string cultureCode);

        void Save();
    }
}
