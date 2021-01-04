using System;
using System.Collections.Generic;
using System.Text;
using VoiceOfKarabakh.Application.Interfaces.LocalizationSet;
using VoiceOfKarabakh.Domain.Interfaces.LocalizationSet;

namespace VoiceOfKarabakh.Application.Services.LocalizationSet
{
    public class LocalizationSetService : ILocalizationSetService
    {
        private readonly ILocalizationSetRepository _localizationSetRepository;

        public LocalizationSetService(ILocalizationSetRepository localizationSetRepository)
        {
            _localizationSetRepository = localizationSetRepository;
        }

        public void Delete(int setId)
        {
            _localizationSetRepository.DeleteLocalizationSet(setId);
        }

        public bool Exists(int setId)
        {
            return _localizationSetRepository.GetLocalizationSetById(setId) != null;
        }

        public void Save()
        {
            _localizationSetRepository.Save();
        }
    }
}
