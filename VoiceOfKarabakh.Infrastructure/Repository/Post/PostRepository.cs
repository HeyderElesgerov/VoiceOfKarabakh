using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VoiceOfKarabakh.Domain.Interfaces.Post;
using VoiceOfKarabakh.Infrastructure.Data;

namespace VoiceOfKarabakh.Infrastructure.Repository.Post
{
    public class PostRepository<TPost> : IPostRepository<TPost> where TPost : Domain.Models.Posts.Post
    {
        readonly ApplicationDbContext _context;
        readonly DbSet<TPost> _posts;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
            _posts = _context.Set<TPost>();
        }

        public void Add(TPost newPost)
        {
            _posts.Add(newPost);
        }

        public void Delete(int id)
        {
            if (!Exists(id))
            {
                throw new ArgumentNullException();
            }

            _posts.Remove(GetPost(id));
        }

        public bool Exists(int id)
        {
            return _posts.Any(p => p.Id == id);
        }

        public TPost GetPost(int id, string includes = null)
        {
            if (includes != null)
            {
                var postsQuery = _posts.Where(p => p.Id == id);

                foreach (string include in includes.Split(','))
                {
                    postsQuery = postsQuery.Include(include);
                }

                return postsQuery.FirstOrDefault(p => p.Id == id);
            }

            return _posts.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<TPost> GetPosts(string includes = null, params Expression<Func<TPost, bool>>[] filters)
        {
            var posts = Queryable.AsQueryable(_posts);

            if (includes != null)
            {
                foreach (string include in includes.Split(','))
                {
                    posts = posts.Include(include);
                }
            }

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    posts = posts.Where(filter);
                }
            }

            return posts;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(int id, TPost editedPost)
        {
            if (!Exists(id))
                throw new ArgumentNullException();

            TPost existingPost = GetPost(id);
            existingPost.Updated = DateTime.Now;
            existingPost.ReadingTime = editedPost.ReadingTime;
            existingPost.Drafted = editedPost.Drafted;
        }

        public bool PostCategoryExists(int postId, int categoryId)
        {
            var post = GetPost(postId, "Categories");

            if (post.Categories != null)
            {
                return post.Categories.Any(c => c.Id == categoryId);
            }

            return false;
        }

        public bool PostTagExists(int postId, int tagId)
        {
            var post = GetPost(postId, "Tags");

            if (post.Tags != null)
            {
                return post.Tags.Any(t => t.Id == tagId);
            }

            return false;
        }

        public void AddPostCategory(int postId, Domain.Models.Category category)
        {
            var post = GetPost(postId, "Categories");
            post.Categories.Add(category);
        }

        public void AddPostTag(int postId, Domain.Models.Tag tag)
        {
            var post = GetPost(postId, "Tags");
            post.Tags.Add(tag);
        }

        public void RemovePostCategory(int postId, int categoryId)
        {
            var post = GetPost(postId, "Categories");
            post.Categories.Remove(post.Categories.First(c => c.Id == categoryId));
        }

        public void RemovePostTag(int postId, int tagId)
        {
            var post = GetPost(postId, "Tags");
            post.Tags.Remove(post.Tags.First(t => t.Id == tagId));
        }
    }
}
