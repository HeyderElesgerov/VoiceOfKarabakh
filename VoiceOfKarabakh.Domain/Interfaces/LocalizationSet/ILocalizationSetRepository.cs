using System.Collections.Generic;

namespace VoiceOfKarabakh.Domain.Interfaces.LocalizationSet
{
    public interface ILocalizationSetRepository
    {
        IEnumerable<Models.LocalizationSet> GetLocalizationSets(string includes = null);

        Models.LocalizationSet GetLocalizationSetById(int id, string includes = null);

        void AddLocalizationSet(Models.LocalizationSet localizationSet);

        void DeleteLocalizationSet(int setId);

        void Save();
    }
}
