using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VoiceOfKarabakh.Application.Interfaces;
using VoiceOfKarabakh.Application.Utility;
using VoiceOfKarabakh.Application.ViewModels.Common;
using VoiceOfKarabakh.Application.ViewModels.Localization;
using VoiceOfKarabakh.Application.ViewModels.Post;
using VoiceOfKarabakh.Domain.Factory.Localization;
using VoiceOfKarabakh.Domain.Factory.LocalizationSet;
using VoiceOfKarabakh.Domain.Interfaces.Post;

namespace VoiceOfKarabakh.Application.Services.Post
{
    public class PostService<TPost> : IPostService<TPost> where TPost : Domain.Models.Posts.Post, new()
    {
        private readonly IPostRepository<TPost> _postRepository;

        private LocalizationSetFactory _localizationSetFactory;
        private LocalizationFactory _localizationFactory;

        public PostService(IPostRepository<TPost> postRepository, LocalizationSetFactory localizationSetFactory, LocalizationFactory localizationFactory)
        {
            _postRepository = postRepository;
            _localizationSetFactory = localizationSetFactory;
            _localizationFactory = localizationFactory;
        }

        public void AddNewPost(NewPostViewModel newPostViewModel)
        {
            TPost newPost = new TPost()
            {
                AuthorId = newPostViewModel.AuthorId,
                ReadingTime = newPostViewModel.ReadingTime,
                Drafted = newPostViewModel.Drafted,
                HeaderPhoto = new Domain.Models.SavedFile() { FilePath = newPostViewModel.NewPhoto.Path },
                Created = DateTime.Now,
                TitleLocalizationSet = _localizationSetFactory.GetLocalizationSet(),
                MetaTitleLocalizationSet = _localizationSetFactory.GetLocalizationSet(),
                ContentLocalizationSet = _localizationSetFactory.GetLocalizationSet(),
                Categories = new List<Domain.Models.Category>(newPostViewModel.SelectedCategories),
                Tags = new List<Domain.Models.Tag>(newPostViewModel.SelectedTags),
                ReadingCount = 0
            };

            foreach (var titleTranslation in newPostViewModel.TitleLocalizations)
            {
                string value = titleTranslation.Value != null ? titleTranslation.Value : "";

                var loc = _localizationFactory.GetLocalization(
                    titleTranslation.CultureCode, value, newPost.TitleLocalizationSet);

                newPost.TitleLocalizationSet.Localizations.Add(loc);
            }

            foreach (var metaTitleTranslation in newPostViewModel.MetaTitleLocalizations)
            {
                string value = metaTitleTranslation.Value != null ? metaTitleTranslation.Value : "";
                var loc = _localizationFactory.GetLocalization(
                    metaTitleTranslation.CultureCode, value, newPost.MetaTitleLocalizationSet);

                newPost.MetaTitleLocalizationSet.Localizations.Add(loc);
            }

            foreach (var contentTranslation in newPostViewModel.ContentLocalizations)
            {
                string value = contentTranslation.Value != null ? contentTranslation.Value : "";
                var loc = _localizationFactory.GetLocalization(
                    contentTranslation.CultureCode, value, newPost.ContentLocalizationSet);

                newPost.ContentLocalizationSet.Localizations.Add(loc);
            }

            _postRepository.Add(newPost);
        }

        public PostAdminIndexViewModel GetPostViewModel(int id, string cultureCode, string includes = null)
        {
            var post = _postRepository.GetPost(id, includes);
            PostAdminIndexViewModel postVM = new PostAdminIndexViewModel()
            {
                Id = id,
                AuthorEmail = "createuserservice@be.fast",
                Created = post.Created,
                Updated = post.Updated,
                ReadingCount = post.ReadingCount,
                ReadingTime = post.ReadingTime,
                Drafted = post.Drafted,
                Title = ""
            };

            if (post.TitleLocalizationSet != null && post.TitleLocalizationSet.Localizations != null)
            {
                var titleLoc = post.TitleLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                if (titleLoc != null)
                {
                    postVM.Title = titleLoc.Value;
                }
            }

            return postVM;
        }

        public PaginatedElements<PostIndexViewModel> GetIndexPagePostElements(int page, int perPage, string cultureCode, string includes = null, Expression<Func<TPost, bool>> predict = null)
        {
            var posts = _postRepository.GetPosts(includes, predict);

            int maxElementCount = posts.Count();

            var postIndexViewModels = new List<PostIndexViewModel>();

            foreach (var post in posts.OrderByDescending(p => p.Created)
                                      .Skip((page - 1) * perPage).Take(perPage))
            {
                var postindexVM = new PostIndexViewModel()
                {
                    Id = post.Id,
                    Created = post.Created,
                    ReadingTime = post.ReadingTime,
                    ReadingCount = post.ReadingCount,
                    Title = "",
                    MetaTitle = "",
                    PostType = typeof(TPost).FullName
                };

                if (post.TitleLocalizationSet != null && post.TitleLocalizationSet.Localizations != null)
                {
                    var titleLoc = post.TitleLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                    if (titleLoc != null)
                    {
                        postindexVM.Title = titleLoc.Value;
                    }
                }

                if (post.MetaTitleLocalizationSet != null && post.MetaTitleLocalizationSet.Localizations != null)
                {
                    var metaTitleLoc = post.MetaTitleLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                    if (metaTitleLoc != null)
                    {
                        postindexVM.MetaTitle = metaTitleLoc.Value;
                    }
                }

                if (post.HeaderPhoto != null)
                {
                    postindexVM.PhotoFileName = post.HeaderPhoto.FilePath.Split('/').Last();
                }

                postIndexViewModels.Add(postindexVM);
            }

            var paginatedElementsVM = new PaginatedElements<PostIndexViewModel>(postIndexViewModels, perPage, page, maxElementCount);

            return paginatedElementsVM;
        }

        public EditPostViewModel GetEditPostViewModel(int id, string includes = null)
        {
            var post = _postRepository.GetPost(id, includes);
            EditPostViewModel editPostVM = new EditPostViewModel()
            {
                PostId = id,
                CurrentPhotoPath = post.HeaderPhoto?.FileName,
                CurrentSaveFileId = post.HeaderPhoto.Id,
                Drafted = post.Drafted,
                ReadingTime = post.ReadingTime,
                SelectedTagIds = post.Tags?.Select(c => c.Id).ToList(),
                SelectedCategoryIds = post.Categories?.Select(t => t.Id).ToList(),
                TitleLocalizations = new List<EditLocalizationViewModel>(),
                MetaTitleLocalizations = new List<EditLocalizationViewModel>(),
                ContentLocalizations = new List<EditLocalizationViewModel>()
            };

            if (post.TitleLocalizationSet != null && post.TitleLocalizationSet.Localizations != null)
            {
                foreach (var titleLoc in post.TitleLocalizationSet.Localizations)
                {
                    var editLocVM = new EditLocalizationViewModel()
                    {
                        LocalizationSetId = titleLoc.LocalizationSetId,
                        CultureCode = titleLoc.CultureCode,
                        Value = titleLoc.Value
                    };

                    editPostVM.TitleLocalizations.Add(editLocVM);
                }
            }

            if (post.MetaTitleLocalizationSet != null && post.MetaTitleLocalizationSet.Localizations != null)
            {
                foreach (var metaTitleLoc in post.MetaTitleLocalizationSet.Localizations)
                {
                    var editLocVM = new EditLocalizationViewModel()
                    {
                        CultureCode = metaTitleLoc.CultureCode,
                        LocalizationSetId = metaTitleLoc.LocalizationSetId,
                        Value = metaTitleLoc.Value
                    };

                    editPostVM.MetaTitleLocalizations.Add(editLocVM);
                }
            }

            if (post.ContentLocalizationSet != null && post.ContentLocalizationSet.Localizations != null)
            {
                foreach (var contentLoc in post.ContentLocalizationSet.Localizations)
                {
                    var editLocVM = new EditLocalizationViewModel()
                    {
                        CultureCode = contentLoc.CultureCode,
                        LocalizationSetId = contentLoc.LocalizationSetId,
                        Value = contentLoc.Value
                    };

                    editPostVM.ContentLocalizations.Add(editLocVM);
                }
            }

            return editPostVM;
        }

        public IEnumerable<PostAdminIndexViewModel> GetPostViewModels(string cultureCode, string includes = null, Expression<Func<TPost, bool>>[] filters = null)
        {
            List<PostAdminIndexViewModel> postViewModels = new List<PostAdminIndexViewModel>();
            var posts = _postRepository.GetPosts(includes, filters);

            foreach (var post in posts)
            {
                PostAdminIndexViewModel postVM = new PostAdminIndexViewModel()
                {
                    Id = post.Id,
                    AuthorEmail = "createuserservice@be.fast",
                    Created = post.Created,
                    Updated = post.Updated,
                    ReadingCount = post.ReadingCount,
                    ReadingTime = post.ReadingTime,
                    Drafted = post.Drafted,
                    Title = ""
                };

                if (post.TitleLocalizationSet != null && post.TitleLocalizationSet.Localizations != null)
                {
                    var titleLoc = post.TitleLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                    if (titleLoc != null)
                    {
                        postVM.Title = titleLoc.Value;
                    }
                }

                postViewModels.Add(postVM);
            }

            return postViewModels;
        }

        public IEnumerable<PostIndexViewModel> GetPostIndexViewModels(string cultureCode, string includes = null, Expression<Func<TPost, bool>> filter = null)
        {
            List<PostIndexViewModel> postIndexViewModels = new List<PostIndexViewModel>();
            var posts = _postRepository.GetPosts(includes, filter);

            foreach(var post in posts)
            {
                var postindexVM = new PostIndexViewModel()
                {
                    Id = post.Id,
                    Created = post.Created,
                    ReadingTime = post.ReadingTime,
                    ReadingCount = post.ReadingCount,
                    Title = "",
                    MetaTitle = "",
                    PostType = typeof(TPost).FullName
                };

                if (post.TitleLocalizationSet != null && post.TitleLocalizationSet.Localizations != null)
                {
                    var titleLoc = post.TitleLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                    if (titleLoc != null)
                    {
                        postindexVM.Title = titleLoc.Value;
                    }
                }

                if (post.MetaTitleLocalizationSet != null && post.MetaTitleLocalizationSet.Localizations != null)
                {
                    var metaTitleLoc = post.MetaTitleLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                    if (metaTitleLoc != null)
                    {
                        postindexVM.MetaTitle = metaTitleLoc.Value;
                    }
                }

                if (post.HeaderPhoto != null)
                {
                    postindexVM.PhotoFileName = post.HeaderPhoto.FilePath.Split('/').Last();
                }

                postIndexViewModels.Add(postindexVM);
            }

            return postIndexViewModels;
        }

        public ReadPostViewModel GetReadPostViewModel(int id, string cultureCode, string includes = null)
        {
            var post = _postRepository.GetPost(id, includes);

            ReadPostViewModel readPostViewModel = new ReadPostViewModel()
            {
                Id = id,
                Author = "implementthis@be.fast",
                Created = post.Created,
                ReadingTime = post.ReadingTime,
                Title = "",
                Content = "",
                Categories = new List<ViewModels.Category.CategoryViewModel>(),
                Tags = new List<ViewModels.Tag.TagViewModel>()
            };

            if(post.HeaderPhoto != null)
            {
                readPostViewModel.PhotoFilePath = post.HeaderPhoto.FileName;
            }

            if(post.TitleLocalizationSet != null && post.TitleLocalizationSet.Localizations != null)
            {
                var titleLoc = post.TitleLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                if(titleLoc != null)
                {
                    readPostViewModel.Title = titleLoc.Value;
                }
            }

            if (post.ContentLocalizationSet != null && post.ContentLocalizationSet.Localizations != null)
            {
                var contentLoc = post.ContentLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                if (contentLoc != null)
                {
                    readPostViewModel.Content = contentLoc.Value;
                }
            }

            if(post.Categories != null)
            {
                foreach(var category in post.Categories)
                {
                    if (category.TitleLocalizationSet != null&& category.TitleLocalizationSet.Localizations != null)
                    {
                        var titleLoc = category.TitleLocalizationSet.Localizations
                                               .FirstOrDefault(l => l.CultureCode == cultureCode);

                        if(titleLoc != null)
                        {
                            readPostViewModel.Categories.Add(new ViewModels.Category.CategoryViewModel()
                            {
                                CategoryId = category.Id,
                                CategoryTitle = titleLoc.Value
                            });
                        }
                    }
                }
            }

            if (post.Tags != null)
            {
                foreach (var tag in post.Tags)
                {
                    if (tag.TitleLocalizationSet != null && tag.TitleLocalizationSet.Localizations != null)
                    {
                        var titleLoc = tag.TitleLocalizationSet.Localizations
                                               .FirstOrDefault(l => l.CultureCode == cultureCode);

                        if (titleLoc != null)
                        {
                            readPostViewModel.Tags.Add(new ViewModels.Tag.TagViewModel()
                            {
                                TagId = tag.Id,
                                TagTitle = titleLoc.Value
                            });
                        }
                    }
                }
            }

            return readPostViewModel;
        }

        public IEnumerable<PostIndexViewModel> GetLatestPosts(string cultureCode, int count, string includes = null)
        {
            var publishedLatestPosts = _postRepository.GetPosts(includes, (tpost) => !tpost.Drafted)
                                                      .Take(count)
                                                      .OrderByDescending(p => p.Created);

            List<PostIndexViewModel> postIndexViewModels = new List<PostIndexViewModel>();

            foreach (var post in publishedLatestPosts)
            {
                var postindexVM = new PostIndexViewModel()
                {
                    Id = post.Id,
                    Created = post.Created,
                    ReadingTime = post.ReadingTime,
                    ReadingCount = post.ReadingCount,
                    Title = "",
                    PostType = typeof(TPost).Name
                };

                if (post.TitleLocalizationSet != null && post.TitleLocalizationSet.Localizations != null)
                {
                    var titleLoc = post.TitleLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                    if (titleLoc != null)
                    {
                        postindexVM.Title = titleLoc.Value;
                    }
                }

                if (post.HeaderPhoto != null)
                {
                    postindexVM.PhotoFileName = post.HeaderPhoto.FilePath.Split('/').Last();
                }

                postIndexViewModels.Add(postindexVM);
            }

            return postIndexViewModels;
        }

        public PaginatedElements<PostIndexViewModel> GetPopularPosts(string cultureCode, int count, int page, string includes = null)
        {
            var publishedLatestPosts = _postRepository.GetPosts(includes, (tpost) => !tpost.Drafted)
                                                      .OrderByDescending(p => p, new PostPopularityComparer());

            int allElementsCount = publishedLatestPosts.Count();

            var postIndexViewModels = new List<PostIndexViewModel>();

            foreach (var post in publishedLatestPosts.Skip((page - 1) * count).Take(count))
            {
                var postindexVM = new PostIndexViewModel()
                {
                    Id = post.Id,
                    Created = post.Created,
                    ReadingTime = post.ReadingTime,
                    ReadingCount = post.ReadingCount,
                    Title = "",
                    PostType = typeof(TPost).Name
                };

                if (post.TitleLocalizationSet != null && post.TitleLocalizationSet.Localizations != null)
                {
                    var titleLoc = post.TitleLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);

                    if (titleLoc != null)
                    {
                        postindexVM.Title = titleLoc.Value;
                    }
                }

                if (post.HeaderPhoto != null)
                {
                    postindexVM.PhotoFileName = post.HeaderPhoto.FilePath.Split('/').Last();
                }

                postIndexViewModels.Add(postindexVM);
            }

            PaginatedElements<PostIndexViewModel> paginatedPosts = new PaginatedElements<PostIndexViewModel>(postIndexViewModels, count, page, allElementsCount);

            return paginatedPosts;
        }

        public void DeletePost(int postId)
        {
            if (!_postRepository.Exists(postId))
                throw new ArgumentNullException();

            _postRepository.Delete(postId);
        }

        public void Save()
        {
            _postRepository.Save();
        }

        public void Update(EditPostViewModel editPostViewModel)
        {
            var post = _postRepository.GetPost(editPostViewModel.PostId);
            post.Drafted = editPostViewModel.Drafted;
            post.ReadingTime = editPostViewModel.ReadingTime;

            _postRepository.Update(post.Id, post);
        }

        public void ReInitSelectedCategories(int postId, IEnumerable<Domain.Models.Category> selectedCategories)
        {
            var post = _postRepository.GetPost(postId, "Categories");
            post.Categories.ToList().RemoveAll((c) => true);
            post.Categories = selectedCategories.ToList();
        }

        public void ReInitSelectedTags(int postId, IEnumerable<Domain.Models.Tag> selectedTags)
        {
            var post = _postRepository.GetPost(postId, "Tags");
            post.Tags.ToList().RemoveAll(t => true);
            post.Tags = selectedTags.ToList();
        }
    }
}
