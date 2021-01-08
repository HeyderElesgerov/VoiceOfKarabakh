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
            double xrating = (double)x.ReadingCount / (DateTime.Now - x.Created).TotalSeconds;
            double yrating = (double)y.ReadingCount / (double)(DateTime.Now - y.Created).TotalSeconds;

            return xrating.CompareTo(yrating);
        }
    }
}
