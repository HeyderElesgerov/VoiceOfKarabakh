using System;
using System.Collections.Generic;

namespace VoiceOfKarabakh.Domain.Models
{
    public class Gallery
    {
        public int Id { get; set; }

        public LocalizationSet TitleLocalizationSet { get; set; }

        public DateTime AddedTime { get; set; }

        public ICollection<SavedFile> Photos { get; set; }
    }
}
