using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VoiceOfKarabakh.Application.ViewModels.Common;
using VoiceOfKarabakh.Application.ViewModels.Post;

namespace VoiceOfKarabakh.Application.Interfaces
{
    public interface IPostService<TPost> where TPost : Domain.Models.Posts.Post
    {
        PostAdminIndexViewModel GetPostViewModel(int id, string cultureCode, string includes = null);

        IEnumerable<PostAdminIndexViewModel> GetPostViewModels(
            string cultureCode, string includes = null, Expression<Func<TPost, bool>>[] filters = null);

        PaginatedElements<PostIndexViewModel> GetIndexPagePostElements(int page, int perPage, string cultureCode, string includes = null, Expression<Func<TPost, bool>> predict = null);

        IEnumerable<PostIndexViewModel> GetLatestPosts(string cultureCode, int count, string includes = null);

        PaginatedElements<PostIndexViewModel> GetPopularPosts(string cultureCode, int count, int page, string includes = null);

        EditPostViewModel GetEditPostViewModel(int id, string includes = null);

        ReadPostViewModel GetReadPostViewModel(int id, string cultureCode, string includes = null);

        void AddNewPost(NewPostViewModel newPostViewModel);

        void Update(EditPostViewModel editPostViewModel);

        void DeletePost(int postId);

        void ReInitSelectedCategories(int postId, IEnumerable<Domain.Models.Category> selectedCategories);
        void ReInitSelectedTags(int postId, IEnumerable<Domain.Models.Tag> selectedTags);
        void Save();
    }
}
