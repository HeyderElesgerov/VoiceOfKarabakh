using System;
using System.Collections;
using System.Collections.Generic;
using VoiceOfKarabakh.Domain.Models.Posts;

namespace VoiceOfKarabakh.Application.Utility
{
    public class PostPopularityComparer : IComparer<Post>
    {
        public int Compare(Post x, Post y)
        {
            int xrating = x.ReadingTime / (DateTime.Now - x.Created).Minutes;
            int yrating = y.ReadingTime / (DateTime.Now - y.Created).Minutes;

            return xrating.CompareTo(yrating);
        }
    }
}
