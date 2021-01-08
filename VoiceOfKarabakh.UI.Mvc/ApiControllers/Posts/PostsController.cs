using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using VoiceOfKarabakh.Application.Interfaces;
using VoiceOfKarabakh.Application.Interfaces.Localization;
using VoiceOfKarabakh.Application.Interfaces.LocalizationSet;
using VoiceOfKarabakh.Application.Interfaces.SaveFile;
using VoiceOfKarabakh.Application.Options;
using VoiceOfKarabakh.Application.Utility;
using VoiceOfKarabakh.Application.ViewModels.Localization;
using VoiceOfKarabakh.Application.ViewModels.Post;
using VoiceOfKarabakh.Application.ViewModels.SaveFile;
using VoiceOfKarabakh.Domain.Interfaces.Category;
using VoiceOfKarabakh.Domain.Interfaces.Post;
using VoiceOfKarabakh.Domain.Interfaces.Tag;
using VoiceOfKarabakh.Domain.Models;
using VoiceOfKarabakh.UI.Mvc.Models.Dto;

namespace VoiceOfKarabakh.UI.Mvc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PostsController<TPost> : ControllerBase where TPost : Domain.Models.Posts.Post, new()
    {
        private readonly IPostService<TPost> _postService;
        private readonly ILocalizationService _localizationService;
        private readonly ISaveFileService _saveFileService;
        private readonly ILocalizationSetService _localizationSetService;

        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPostRepository<TPost> _postRepository;

        string wwwRootFolder;
        string photosFolder;
        CultureInfo defaultCulture;

        public PostsController(
            IPostService<TPost> postService,
            ILocalizationService localizationService,
            ISaveFileService saveFileService,
            ILocalizationSetService localizationSetService,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository,
            IOptions<RequestLocalizationOptions> options,
            IPostRepository<TPost> postRepository,
            IWebHostEnvironment webHostEnvironment, IOptions<FilePathOption> filePathOpt)
        {
            _postService = postService;
            _saveFileService = saveFileService;
            _localizationService = localizationService;
            _localizationSetService = localizationSetService;

            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _postRepository = postRepository;

            defaultCulture = options.Value.DefaultRequestCulture.Culture;

            wwwRootFolder = webHostEnvironment.WebRootPath;
            photosFolder = filePathOpt.Value.PhotosFolder;
        }

        [HttpGet]
        public IActionResult GetPostViewModels()//for admin index
        {
            string cultureCode = defaultCulture.TwoLetterISOLanguageName;
            string includes = "TitleLocalizationSet.Localizations,MetaTitleLocalizationSet.Localizations,ContentLocalizationSet.Localizations";

            return Ok(_postService.GetPostViewModels(cultureCode, includes));
        }

        [HttpGet("{id}")]
        public IActionResult GetEditPostViewModelById(int id)
        {
            string includes = "TitleLocalizationSet.Localizations,MetaTitleLocalizationSet.Localizations,ContentLocalizationSet.Localizations,HeaderPhoto,Categories,Tags";

            var editPostVM = _postService.GetEditPostViewModel(id, includes);

            return Ok(editPostVM);
        }

        [HttpPost]
        public IActionResult Add([FromForm] NewPostDto newPostDto)
        {
            NewPostViewModel newPostViewModel = new NewPostViewModel()
            {
                AuthorId = "implementthis@be.fast",
                Drafted = newPostDto.Drafted,
                ReadingTime = newPostDto.ReadingTime,
                ContentLocalizations = new List<NewLocalizationViewModel>(),
                MetaTitleLocalizations = new List<NewLocalizationViewModel>(),
                TitleLocalizations = new List<NewLocalizationViewModel>(),
                SelectedCategories = new List<Category>(),
                SelectedTags = new List<Tag>(),
                NewPhoto = new NewSaveFileViewModel()
            };

            if (newPostDto.TitleLocalizations != null)
            {
                foreach (var translation in newPostDto.TitleLocalizations)
                {
                    newPostViewModel.TitleLocalizations.Add(new NewLocalizationViewModel()
                    {
                        CultureCode = translation.CultureCode,
                        Value = translation.Value
                    });
                }
            }

            if (newPostDto.MetaTitleLocalizations != null)
            {
                foreach (var translation in newPostDto.MetaTitleLocalizations)
                {
                    newPostViewModel.MetaTitleLocalizations.Add(new NewLocalizationViewModel()
                    {
                        CultureCode = translation.CultureCode,
                        Value = translation.Value
                    });
                }
            }

            if (newPostDto.ContentLocalizations != null)
            {
                foreach (var translation in newPostDto.ContentLocalizations)
                {
                    newPostViewModel.ContentLocalizations.Add(new NewLocalizationViewModel()
                    {
                        CultureCode = translation.CultureCode,
                        Value = translation.Value
                    });
                }
            }

            if (newPostDto.SelectedCategoryIds != null)
            {
                foreach (int catId in newPostDto.SelectedCategoryIds)
                {
                    //we just assing catId to category, it will be referenced to an
                    //exisitng category while saving
                    newPostViewModel.SelectedCategories.Add(_categoryRepository.GetCategory(catId));
                }
            }

            if (newPostDto.SelectedTagIds != null)
            {
                foreach (int tagId in newPostDto.SelectedTagIds)
                {
                    newPostViewModel.SelectedTags.Add(_tagRepository.GetTag(tagId));
                }
            }

            //saved file
            string filePath = FileOperations.GenerateFilePath(wwwRootFolder, photosFolder, newPostDto.Photo);
            newPostViewModel.NewPhoto.Path = filePath;
            FileOperations.Upload(newPostDto.Photo, filePath);

            _postService.AddNewPost(newPostViewModel);
            _postService.Save();

            return CreatedAtAction(nameof(GetEditPostViewModelById), newPostDto);
        }

        [HttpPut]
        public IActionResult Edit([FromForm] EditPostViewModel editPostViewModel)
        {
            //it just changes some of properties. To change translation, photo, we use appropirate services
            _postService.Update(editPostViewModel);

            //updating existing translations and adding new ones for new culture
            foreach (var titleTranslation in editPostViewModel.TitleLocalizations)
            {
                int setId = titleTranslation.LocalizationSetId;
                string cultureCode = titleTranslation.CultureCode;

                if (!_localizationService.Exists(setId, cultureCode))
                {
                    _localizationService.Add(setId, new NewLocalizationViewModel()
                    {
                        CultureCode = cultureCode,
                        Value = titleTranslation.Value
                    });
                }
                else
                {
                    _localizationService.Update(new EditLocalizationViewModel()
                    {
                        CultureCode = cultureCode,
                        LocalizationSetId = setId,
                        Value = titleTranslation.Value
                    });
                }
            }

            foreach (var metaTitleTranslation in editPostViewModel.MetaTitleLocalizations)
            {
                int setId = metaTitleTranslation.LocalizationSetId;
                string cultureCode = metaTitleTranslation.CultureCode;

                if (!_localizationService.Exists(setId, cultureCode))
                {
                    _localizationService.Add(setId, new NewLocalizationViewModel()
                    {
                        CultureCode = cultureCode,
                        Value = metaTitleTranslation.Value
                    });
                }
                else
                {
                    _localizationService.Update(new EditLocalizationViewModel()
                    {
                        CultureCode = cultureCode,
                        LocalizationSetId = setId,
                        Value = metaTitleTranslation.Value
                    });
                }
            }

            foreach (var contentTranslation in editPostViewModel.ContentLocalizations)
            {
                int setId = contentTranslation.LocalizationSetId;
                string cultureCode = contentTranslation.CultureCode;

                if (!_localizationService.Exists(setId, cultureCode))
                {
                    _localizationService.Add(setId, new NewLocalizationViewModel()
                    {
                        CultureCode = cultureCode,
                        Value = contentTranslation.Value
                    });
                }
                else
                {
                    _localizationService.Update(new EditLocalizationViewModel()
                    {
                        CultureCode = cultureCode,
                        LocalizationSetId = setId,
                        Value = contentTranslation.Value
                    });
                }
            }

            _localizationService.Save();

            //updating header photo
            if (editPostViewModel.NewHeaderPhoto != null)
            {
                string filePath = _saveFileService.GetSaveFile(editPostViewModel.CurrentSaveFileId).FilePath;
                //we dont need to change fk in post to new saved file, we just editing file, not path
                FileOperations.Upload(editPostViewModel.NewHeaderPhoto, filePath);
            }


            //updating selected categories
            if (editPostViewModel.SelectedCategoryIds != null)
            {
                List<Category> categories = new List<Category>();
                foreach (int selectedCategoryId in editPostViewModel.SelectedCategoryIds)
                {
                    categories.Add(_categoryRepository.GetCategory(selectedCategoryId));
                }
                _postService.ReInitSelectedCategories(editPostViewModel.PostId, categories);
            }
            else
            {
                _postService.ReInitSelectedCategories(editPostViewModel.PostId, new List<Category>());
            }

            //updating selected tags
            if (editPostViewModel.SelectedTagIds != null)
            {
                List<Tag> tags = new List<Tag>();
                foreach (int selectedTagId in editPostViewModel.SelectedTagIds)
                {
                    tags.Add(_tagRepository.GetTag(selectedTagId));
                }
                _postService.ReInitSelectedTags(editPostViewModel.PostId, tags);
            }
            else
            {
                _postService.ReInitSelectedTags(editPostViewModel.PostId, new List<Tag>());
            }

            _postService.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var post = _postRepository.GetPost(id, "TitleLocalizationSet,MetaTitleLocalizationSet,ContentLocalizationSet,HeaderPhoto");

            int headerPhotoId = post.HeaderPhoto.Id;
            string headerPhotoPath = post.HeaderPhoto.FilePath;

            _postService.DeletePost(id);

            //delete file
            if (FileOperations.DeleteFile(headerPhotoPath))
            {
                _saveFileService.Delete(headerPhotoId);
            }

            //delete localizations
            _localizationSetService.Delete(post.TitleLocalizationSet.Id);
            _localizationSetService.Delete(post.MetaTitleLocalizationSet.Id);
            _localizationSetService.Delete(post.ContentLocalizationSet.Id);

            _saveFileService.Save();
            _localizationSetService.Save();
            _postService.Save();

            return Ok();
        }

        [HttpGet, Route("[action]/{count}/{culturecode}")]
        [AllowAnonymous]
        public IActionResult GetLatestPosts(int count, string cultureCode)
        {
            return Ok(_postService.GetLatestPosts(cultureCode, count, "TitleLocalizationSet.Localizations,HeaderPhoto"));
        }

        [HttpGet, Route("[action]/{count}/{culturecode}/{page}")]
        [AllowAnonymous]
        public IActionResult GetPopularPosts(int count, string cultureCode, int page)
        {
            var popularPosts = _postService.GetPopularPosts(cultureCode, count, page, "TitleLocalizationSet.Localizations,HeaderPhoto");
            return Ok(popularPosts);
        }

        [HttpGet, Route("[action]/{count}/{culturecode}/{page}")]
        [AllowAnonymous]
        public IActionResult GetIndexPageElements(int count, string cultureCode, int page)
        {
            string includes = "TitleLocalizationSet.Localizations,MetaTitleLocalizationSet.Localizations,HeaderPhoto";

            return Ok(_postService.GetIndexPagePostElements(page, count, cultureCode, includes, p => !p.Drafted));
        }

        [HttpGet, Route("[action]/{id}/{culturecode}")]
        [AllowAnonymous]
        public IActionResult GetReadPost(int id, string cultureCode)
        {
            var post = _postRepository.GetPost(id);

            if(post == null)
                return NotFound();

            var readPostVM = _postService.GetReadPostViewModel(id, cultureCode, "TitleLocalizationSet.Localizations,ContentLocalizationSet.Localizations,HeaderPhoto,Categories.TitleLocalizationSet.Localizations,Tags.TitleLocalizationSet.Localizations");

            if(HttpContext.Request.Cookies["post"+id] == null)
            {
                HttpContext.Response.Cookies.Append("post" + id, "read");
                _postRepository.IncreaseReadingTime(id);
                _postRepository.Save();
            }

            return Ok(readPostVM);
        }

        [HttpGet, Route("[action]/{count}/{page}/{categoryId}/{culturecode}")]
        [AllowAnonymous]
        public IActionResult GetByCategory(int categoryId, string cultureCode, int count, int page)
        {
            string includes = "TitleLocalizationSet.Localizations,MetaTitleLocalizationSet.Localizations,HeaderPhoto,Categories";

            return Ok(_postService.GetIndexPagePostElements(page, count, cultureCode, includes,
                p => !p.Drafted && p.Categories.Any(c => c.Id == categoryId)));
        }

        [HttpGet, Route("[action]/{count}/{page}/{tagId}/{culturecode}")]
        [AllowAnonymous]
        public IActionResult GetByTag(int tagId, string cultureCode, int count, int page)
        {
            string includes = "TitleLocalizationSet.Localizations,MetaTitleLocalizationSet.Localizations,HeaderPhoto,Categories";

            return Ok(_postService.GetIndexPagePostElements(page, count, cultureCode, includes,
                p => !p.Drafted && p.Tags.Any(c => c.Id == tagId)));
        }

        [HttpGet, Route("[action]/{postId}/{cultureCode}")]
        [AllowAnonymous]
        public IActionResult GetRelatedPosts(int postId, string cultureCode)
        {
            List<PostIndexViewModel> posts = new List<PostIndexViewModel>();//related posts

            var post = _postRepository.GetPost(postId, "Categories,Tags");

            if (post != null)
            {
                foreach (var category in post.Categories)
                {
                    var relatedPosts = _postService.GetPostIndexViewModels(cultureCode, "TitleLocalizationSet.Localizations,Categories", p =>
                                !p.Drafted && p.Id != postId && p.Categories.Contains(category));

                    foreach (var relatedPost in relatedPosts)
                    {
                        bool alredySelected = posts.FirstOrDefault(p => p.Id == relatedPost.Id) != null;

                        if (!alredySelected)
                        {
                            posts.Add(relatedPost);
                        }
                    }
                }

                foreach (var tag in post.Tags)
                {
                    var relatedPosts = _postService.GetPostIndexViewModels(cultureCode,
                        "TitleLocalizationSet.Localizations,Tags", p =>
                                !p.Drafted && p.Id != postId && p.Tags.Contains(tag));

                    foreach (var relatedPost in relatedPosts)
                    {
                        bool alredySelected = posts.FirstOrDefault(p => p.Id == relatedPost.Id) != null;

                        if (!alredySelected)
                        {
                            posts.Add(relatedPost);
                        }
                    }
                }
            }

            return Ok(posts);
        }
    }
}
