using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;
using VoiceOfKarabakh.Application.Interfaces.Localization;
using VoiceOfKarabakh.Application.Interfaces.Tag;
using VoiceOfKarabakh.Application.ViewModels.Localization;
using VoiceOfKarabakh.Application.ViewModels.Tag;

namespace VoiceOfKarabakh.UI.Mvc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly ILocalizationService _localizationService;

        public TagsController(ITagService tagService, ILocalizationService localizationService)
        {
            _tagService = tagService;
            _localizationService = localizationService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            string titleLocSetProperty = "TitleLocalizationSet";
            string titleLocProperty = "Localizations";

            string includes = titleLocSetProperty + "." + titleLocProperty;
            var tagVMs = _tagService.GetAllTagsWithTranslations(includes);

            return Ok(tagVMs);
        }

        [HttpGet("{id}")]
        public TagViewModel Get(int id)
        {
            string cultureCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            string titleLocSetProperty = "TitleLocalizationSet";
            string titleLocProperty = "Localizations";

            string includes = titleLocSetProperty + "." + titleLocProperty;
            var categoryVM = _tagService.GetTag(id, cultureCode, includes);

            return categoryVM;
        }

        [HttpPost, Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Add(NewTagViewModel newTagViewModel)
        {
            _tagService.AddNewTag(newTagViewModel);
            _tagService.Save();
            return CreatedAtAction(nameof(Get), null);
        }

        [HttpPut]
        public IActionResult Put(EditTagViewModel editTagViewModel)
        {
            foreach (var translation in editTagViewModel.TagTitleTranslations)
            {
                int setId = translation.LocalizationSetId;
                string cultureCode = translation.CultureCode;

                if (_localizationService.Exists(setId, cultureCode))
                {
                    _localizationService.Update(translation);

                }
                else
                {
                    _localizationService.Add(setId, new NewLocalizationViewModel()
                    {
                        CultureCode = cultureCode,
                        Value = translation.Value
                    });
                }
            }

            try
            {
                _localizationService.Save();//we dont change anything in tag
                return NoContent();
            }
            catch (DbUpdateException updateEx)
            {
                ModelState.AddModelError("", updateEx.Message);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _tagService.DeleteTag(id);

            try
            {
                _tagService.Save();
                return Ok();
            }
            catch (DBConcurrencyException dbEx)
            {
                ModelState.AddModelError("", dbEx.Message);
            }

            return BadRequest(ModelState);
        }
    }
}
