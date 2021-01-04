using System;
using System.Collections.Generic;

namespace VoiceOfKarabakh.Domain.Models.Posts
{
    public class Post
    {
        public int Id { get; set; }

        public string AuthorId { get; set; }

        public LocalizationSet TitleLocalizationSet { get; set; }

        public LocalizationSet MetaTitleLocalizationSet { get; set; }

        public LocalizationSet ContentLocalizationSet { get; set; }
        
        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public int ReadingCount { get; set; }

        public SavedFile HeaderPhoto { get; set; }

        public int ReadingTime { get; set; }

        public bool Drafted { get; set; }

        public ICollection<Category> Categories { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
