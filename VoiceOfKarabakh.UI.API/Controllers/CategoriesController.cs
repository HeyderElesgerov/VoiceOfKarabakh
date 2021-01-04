﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;
using VoiceOfKarabakh.Application.Interfaces.Category;
using VoiceOfKarabakh.Application.ViewModels.Category;
using VoiceOfKarabakh.Application.Interfaces.Localization;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.UI.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILocalizationService _localizationService;

        public CategoriesController(ICategoryService categoryService, ILocalizationService localizationService)
        {
            _categoryService = categoryService;
            _localizationService = localizationService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            string cultureCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            string titleLocSetProperty = "TitleLocalizationSet";
            string titleLocProperty = "Localizations";

            string includes = titleLocSetProperty + "." + titleLocProperty;
            var categoryVMs = _categoryService.GetAllCategories(cultureCode, includes);

            return Ok(categoryVMs);// new ActionResult<IEnumerable<CategoryViewModel>>(categoryVMs);
        }

        [HttpPost, Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add(NewCategoryViewModel newCategoryViewModel)
        {
            _categoryService.AddNewCategory(newCategoryViewModel);
            _categoryService.Save();
            return CreatedAtAction(nameof(Get), null);
        }

        [HttpPut]
        public IActionResult Put(EditCategoryViewModel editCategoryViewModel)
        {
            foreach (var translation in editCategoryViewModel.CategoryTitleTranslations)
            {
                string cultureCode = translation.CultureCode;
                string value = translation.Value != null ? translation.Value : "";
                int setId = translation.LocalizationSetId;

                if(_localizationService.Exists(setId, cultureCode))
                {
                    _localizationService.Update(translation);
                }
                else
                {
                    _localizationService.Add(setId, new NewLocalizationViewModel()
                    {
                        CultureCode = cultureCode,
                        Value = value
                    });
                }
            }

            try
            {
                _localizationService.Save();
                return NoContent();
            }
            catch (DbUpdateException updateEx)
            {
                ModelState.AddModelError("", updateEx.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);

            try
            {
                _categoryService.Save();
                return Ok();
            }
            catch (DBConcurrencyException dbEx)
            {
                ModelState.AddModelError("", dbEx.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpGet("{id}")]
        public CategoryViewModel Get(int id)
        {
            string cultureCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            string titleLocSetProperty = "TitleLocalizationSet";
            string titleLocProperty = "Localizations";

            string includes = titleLocSetProperty + "." + titleLocProperty;
            var categoryVM = _categoryService.GetCategory(id, cultureCode, includes);

            return categoryVM;
        }
    }
}
