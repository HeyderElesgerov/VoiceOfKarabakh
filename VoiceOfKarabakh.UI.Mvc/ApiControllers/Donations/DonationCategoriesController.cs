using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using VoiceOfKarabakh.Application.Interfaces.DonationCategory;
using VoiceOfKarabakh.Application.Interfaces.Localization;
using VoiceOfKarabakh.Application.Interfaces.LocalizationSet;
using VoiceOfKarabakh.Application.Interfaces.SaveFile;
using VoiceOfKarabakh.Application.Utility;
using VoiceOfKarabakh.Application.ViewModels.DonationCategory;
using VoiceOfKarabakh.Domain.Interfaces.DonationCategory;

namespace VoiceOfKarabakh.UI.Mvc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DonationCategoriesController : ControllerBase
    {
        private readonly IDonationCategoryService _donationCategoryService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizationSetService _localizationSetService;
        private readonly IDonationCategoryRepository _donationCategoryRepository;
        private readonly ISaveFileService _saveFileService;

        public DonationCategoriesController(IDonationCategoryService donationCategoryService, ILocalizationService localizationService, ILocalizationSetService localizationSetService, ISaveFileService saveFileService, IDonationCategoryRepository donationCategoryRepository)
        {
            _donationCategoryService = donationCategoryService;
            _localizationService = localizationService;
            _localizationSetService = localizationSetService;
            _saveFileService = saveFileService;
            _donationCategoryRepository = donationCategoryRepository;
        }

        [HttpGet("{cultureCode}")]
        [AllowAnonymous]
        public IActionResult GetAll(string cultureCode)
        {
            return Ok(_donationCategoryService.GetDonationCategoryViewModels(cultureCode, "TitleLocalizationSet.Localizations"));
        }

        [Route("[action]/{id}"), HttpGet()]
        public IActionResult GetEditDonationCategoryViewModel(int id)
        {
            if (!_donationCategoryService.Exists(id))
            {
                return NotFound();
            }

            return Ok(_donationCategoryService.GetEditDonationCategoryViewModel(id));
        }

        [HttpPost]
        public IActionResult Add(NewDonationCategoryViewModel newDonationCategoryViewModel)
        {
            _donationCategoryService.Add(newDonationCategoryViewModel);
            _donationCategoryService.Save();

            return CreatedAtAction("GetById", newDonationCategoryViewModel);
        }

        [HttpPut]
        public IActionResult Update(EditDonationCategoryViewModel editDonationCategoryViewModel)
        {
            foreach (var translation in editDonationCategoryViewModel.TitleTranslations)
            {
                if (_localizationService.Exists(translation.LocalizationSetId, translation.CultureCode))
                {
                    _localizationService.Update(translation);
                }
                else
                {
                    _localizationService.Add(translation.LocalizationSetId, new Application.ViewModels.Localization.NewLocalizationViewModel()
                    {
                        CultureCode = translation.CultureCode,
                        Value = translation.Value
                    });
                }
            }

            _localizationService.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_donationCategoryService.Exists(id))
            {
                return NotFound();
            }
            var donationCategory = _donationCategoryRepository.Get(id, "TitleLocalizationSet,Donations.HeaderPhoto,Donations.TitleLocalizationSet,Donations.MetaTitleLocalizationSet,Donations.ContentLocalizationSet");

            _donationCategoryService.Delete(id);
            _localizationSetService.Delete(donationCategory.TitleLocalizationSet.Id);

            foreach(var donation in donationCategory.Donations)
            {
                //deletes donation's photo
                if (FileOperations.DeleteFile(donation.HeaderPhoto.FilePath))
                {
                    _saveFileService.Delete(donation.HeaderPhoto.Id);
                }

                //deleting related donation locsets
                _localizationSetService.Delete(donation.TitleLocalizationSet.Id);
                _localizationSetService.Delete(donation.MetaTitleLocalizationSet.Id);
                _localizationSetService.Delete(donation.ContentLocalizationSet.Id);
            }


            _donationCategoryService.Save();
            _localizationSetService.Save();
            _saveFileService.Save();

            return Ok();
        }
    }
}
