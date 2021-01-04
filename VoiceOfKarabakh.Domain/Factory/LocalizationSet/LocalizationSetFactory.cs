using System.Collections.Generic;
using System.Linq;

namespace VoiceOfKarabakh.Domain.Factory.LocalizationSet
{
    public class LocalizationSetFactory
    {
        public Models.LocalizationSet GetLocalizationSet()
        {
            return new Models.LocalizationSet()
            {
                Localizations = new List<Models.Localization>()
            };
        }

        public Models.LocalizationSet GetLocalizationSet(IEnumerable<Models.Localization> localizations)
        {
            var localizationSet = GetLocalizationSet();
            localizationSet.Localizations = localizations.ToList();
            return localizationSet;
        }

        public Models.LocalizationSet GetLocalizationSet(int setId)
        {
            return new Models.LocalizationSet()
            {
                Id = setId,
                Localizations = new List<Models.Localization>()
            };
        }

        public Models.LocalizationSet GetLocalizationSet(int setId, IEnumerable<Models.Localization> localizations)
        {
            var localizationSet = GetLocalizationSet(setId);
            localizationSet.Localizations = localizations.ToList();
            return localizationSet;
        }
    }
}
