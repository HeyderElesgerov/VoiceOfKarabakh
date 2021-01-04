using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceOfKarabakh.Application.Interfaces.LocalizationSet
{
    public interface ILocalizationSetService
    {
        void Delete(int setId);
        bool Exists(int setId);
        void Save();
    }
}
