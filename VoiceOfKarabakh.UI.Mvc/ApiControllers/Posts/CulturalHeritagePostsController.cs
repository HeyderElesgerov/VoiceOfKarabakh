﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VoiceOfKarabakh.Application.Interfaces;
using VoiceOfKarabakh.Application.Interfaces.Localization;
using VoiceOfKarabakh.Application.Interfaces.LocalizationSet;
using VoiceOfKarabakh.Application.Interfaces.SaveFile;
using VoiceOfKarabakh.Application.Options;
using VoiceOfKarabakh.Domain.Interfaces.Category;
using VoiceOfKarabakh.Domain.Interfaces.Post;
using VoiceOfKarabakh.Domain.Interfaces.Tag;

namespace VoiceOfKarabakh.UI.Mvc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CulturalHeritagePostsController : PostsController<Domain.Models.Posts.CulturalHeritagePost>
    {
        public CulturalHeritagePostsController(
            IPostService<Domain.Models.Posts.CulturalHeritagePost> postService,
            ILocalizationService localizationService,
            ISaveFileService saveFileService,
            ILocalizationSetService localizationSetService,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository,
            IPostRepository<Domain.Models.Posts.CulturalHeritagePost> postRepository,
            IOptions<RequestLocalizationOptions> options,
            IWebHostEnvironment webHostEnvironment,
            IOptions<FilePathOption> filePathOpt)
            : base(
                  postService,
                  localizationService,
                  saveFileService,
                  localizationSetService,
                  categoryRepository,
                  tagRepository,
                  options,
                  postRepository,
                  webHostEnvironment,
                  filePathOpt)
        {
        }
    }
}
