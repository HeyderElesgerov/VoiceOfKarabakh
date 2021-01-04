using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VoiceOfKarabakh.Application.Interfaces.Category;
using VoiceOfKarabakh.Application.Interfaces.Tag;

namespace VoiceOfKarabakh.UI.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CulturalHeritageController : Controller
    {
        List<CultureInfo> cultureInfos;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public CulturalHeritageController(IOptions<RequestLocalizationOptions> options, ICategoryService categoryService, ITagService tagService)
        {
            cultureInfos = options.Value.SupportedCultures.ToList();
            _categoryService = categoryService;
            _tagService = tagService;
        }

        [AllowAnonymous]
        public IActionResult Index(int page = 1, int perPage = 10)
        {
            ViewData["Page"] = page;
            ViewData["PerPage"] = perPage;
            return View();
        }

        [AllowAnonymous]
        public IActionResult Read(int id)
        {
            ViewData["id"] = id;

            return View();
        }

        [AllowAnonymous]
        public IActionResult ByCategory(int categoryId, int page = 1, int perPage = 10)
        {
            string cultureCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var categoryVm = _categoryService.GetCategory(categoryId, cultureCode, "TitleLocalizationSet.Localizations");

            ViewData["CategoryId"] = categoryId;
            ViewData["Category"] = categoryVm.CategoryTitle;
            ViewData["Page"] = page;
            ViewData["PerPage"] = perPage;
            return View(categoryId);
        }

        [AllowAnonymous]
        public IActionResult ByTag(int tagId, int page = 1, int perPage = 10)
        {
            string cultureCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var tagVM = _tagService.GetTag(tagId, cultureCode, "TitleLocalizationSet.Localizations");

            ViewData["TagId"] = tagId;
            ViewData["Tag"] = tagVM.TagTitle;
            ViewData["Page"] = page;
            ViewData["PerPage"] = perPage;
            return View(tagId);
        }

        public IActionResult AdminIndex()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View(cultureInfos);
        }

        public IActionResult Edit(int id)
        {
            ViewData["id"] = id;
            return View(cultureInfos);
        }
    }
}
