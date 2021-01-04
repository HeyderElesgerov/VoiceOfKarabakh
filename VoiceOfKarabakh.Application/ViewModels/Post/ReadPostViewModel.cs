using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceOfKarabakh.Application.ViewModels.Post
{
    public class ReadPostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime Created { get; set; }

        public string PhotoFilePath { get; set; }

        public int ReadingTime { get; set; }

        public List<Category.CategoryViewModel> Categories { get; set; }

        public List<Tag.TagViewModel> Tags { get; set; }
    }
}
