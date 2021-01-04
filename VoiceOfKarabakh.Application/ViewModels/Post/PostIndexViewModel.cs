using System;

namespace VoiceOfKarabakh.Application.ViewModels.Post
{
    public class PostIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string PhotoFileName { get; set; }
        public DateTime Created { get; set; }
        public int ReadingCount { get; set; }
        public int ReadingTime { get; set; }
        public string PostType { get; set; }
    }
}
