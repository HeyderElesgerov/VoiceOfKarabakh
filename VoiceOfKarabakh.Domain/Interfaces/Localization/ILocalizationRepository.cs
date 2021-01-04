using System.Collections.Generic;

namespace VoiceOfKarabakh.Domain.Interfaces.Localization
{
    public interface ILocalizationRepository
    {
        Models.Localization GetLocalization(int setId, string cultureCode);

        IEnumerable<Models.Localization> GetLocalizationsBySet(int setId);

        void AddLocalization(Models.Localization newLocalization);

        void Update(Models.Localization editedLocalization);

        void Delete(int setId, string cultureCode);

        void Save();

        bool Exists(int setId, string cultureCode);
    }
}
