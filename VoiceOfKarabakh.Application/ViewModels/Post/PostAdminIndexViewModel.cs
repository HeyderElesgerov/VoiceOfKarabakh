using System;

namespace VoiceOfKarabakh.Application.ViewModels.Post
{
    //for admin (in post index)
    public class PostAdminIndexViewModel
    {
        public int Id { get; set; }
        public string AuthorEmail { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int ReadingCount { get; set; }
        public int ReadingTime { get; set; }
        public bool Drafted { get; set; }
    }
}
