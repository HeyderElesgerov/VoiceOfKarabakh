using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace VoiceOfKarabakh.Domain.Interfaces.Post
{
    public interface IPostRepository<TPost> where TPost : Models.Posts.Post
    {
        IEnumerable<TPost> GetPosts(
            string includes = null, params Expression<Func<TPost, bool>>[] filters);

        TPost GetPost(int id, string includes = null);

        void Add(TPost newPost);

        void Update(int id, TPost editedPost);

        void IncreaseReadingTime(int id);

        void Delete(int id);

        bool Exists(int id);

        bool PostCategoryExists(int postId, int categoryId);
        bool PostTagExists(int postId, int tagId);

        void AddPostCategory(int postId, Models.Category category);
        void AddPostTag(int postId, Models.Tag tag);

        void RemovePostCategory(int postId, int categoryId);
        void RemovePostTag(int postId, int tagId);

        void Save();
    }
}
