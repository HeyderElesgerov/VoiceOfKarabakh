﻿using System.Collections.Generic;

namespace VoiceOfKarabakh.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }

        public LocalizationSet TitleLocalizationSet { get; set; }

        public ICollection<Posts.Post> Posts { get; set; }
    }
}
