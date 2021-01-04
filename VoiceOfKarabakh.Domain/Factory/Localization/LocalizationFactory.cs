using System;
using System.Collections.Generic;
using System.Text;
using VoiceOfKarabakh.Domain.Models;

namespace VoiceOfKarabakh.Domain.Factory.Localization
{
    public class LocalizationFactory
    {
        public Models.Localization GetLocalizationInstance(string cultureCode, string value)
        {
            return new Models.Localization()
            {
                CultureCode = cultureCode,
                Value = value
            };
        }

        public Models.Localization GetLocalization(string cultureCode, string value, Models.LocalizationSet localizationSet)
        {
            var localization = GetLocalizationInstance(cultureCode, value);
            localization.LocalizationSet = localizationSet;

            return localization;
        }

        public Models.Localization GetLocalization(string cultureCode, string value, int localizationSetId)
        {
            var localization = GetLocalizationInstance(cultureCode, value);
            localization.LocalizationSetId = localizationSetId;

            return localization;
        }
    }
}
