using System.Collections.Generic;

namespace VoiceOfKarabakh.Domain.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public LocalizationSet TitleLocalizationSet { get; set; }

        public ICollection<Posts.Post> Posts { get; set; }
    }
}
