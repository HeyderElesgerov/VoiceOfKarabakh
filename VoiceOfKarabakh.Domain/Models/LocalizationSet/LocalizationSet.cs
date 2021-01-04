using System.Collections.Generic;

namespace VoiceOfKarabakh.Domain.Models
{
    public class LocalizationSet
    {
        public int Id { get; set; }

        public virtual ICollection<Localization> Localizations { get; set; }
    }
}
