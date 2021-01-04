using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VoiceOfKarabakh.Application.Interfaces.Donation;
using VoiceOfKarabakh.Application.Interfaces.Localization;
using VoiceOfKarabakh.Application.Interfaces.LocalizationSet;
using VoiceOfKarabakh.Application.Interfaces.SaveFile;
using VoiceOfKarabakh.Application.Options;
using VoiceOfKarabakh.Application.Utility;
using VoiceOfKarabakh.Application.ViewModels.Donation;
using VoiceOfKarabakh.Domain.Interfaces.Donation;
using VoiceOfKarabakh.UI.Mvc.Models.Dto;

namespace VoiceOfKarabakh.UI.Mvc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DonationsController : ControllerBase
    {
        private readonly IDonationService _donationService;
        private readonly ISaveFileService _saveFileService;
        private readonly IDonationRepository _donationRepository;
        private readonly ILocalizationSetService _localizationSetService;
        private readonly ILocalizationService _localizationService;

        string wwwrootFolder;
        string PhotosFolder;

        public DonationsController(IDonationService donationService, ISaveFileService saveFileService, IDonationRepository donationRepository, ILocalizationSetService localizationSetService, ILocalizationService localizationService, IWebHostEnvironment webHostEnvironment, IOptions<FilePathOption> filePathOptions)
        {
            _donationService = donationService;
            _saveFileService = saveFileService;
            _localizationSetService = localizationSetService;
            _localizationService = localizationService;

            _donationRepository = donationRepository;

            wwwrootFolder = webHostEnvironment.WebRootPath;
            PhotosFolder = filePathOptions.Value.PhotosFolder;
        }

        [HttpGet]
        [Route("[action]/{cultureCode}")]
        public IActionResult GetAllDonationViewModels(string cultureCode)
        {
            return Ok(_donationService.GetDonationViewModels(cultureCode, "TitleLocalizationSet.Localizations,DonationCategory.TitleLocalizationSet.Localizations"));
        }

        [HttpGet, Route("[action]/{donationCategoryId}/{cultureCode}")]
        [AllowAnonymous]
        public IActionResult GetDonationsViewModelByDonationCategory(int donationCategoryId, string cultureCode)
        {
            return Ok(_donationService.GetDonationViewModelsByDonationCategory(donationCategoryId, cultureCode, "TitleLocalizationSet.Localizations,MetaTitleLocalizationSet.Localizations,HeaderPhoto"));
        }

        [HttpGet("{id}")]
        public IActionResult GetEditDonationViewModel(int id)
        {
            if (!_donationService.Exists(id))
                return NotFound();

            var editDonationVM = _donationService.GetEditDonationViewModel(id);
            return Ok(editDonationVM);
        }

        [HttpPost]
        public IActionResult Add([FromForm] NewDonationDto newDonationDto)
        {
            NewDonationViewModel newDonationViewModel = new NewDonationViewModel()
            {
                ContentLocalizations = newDonationDto.ContentLocalizations,
                TitleLocalizations = newDonationDto.TitleLocalizations,
                MetaTitleLocalizations = newDonationDto.MetaTitleLocalizations,
                SelectedDonationCategoryId = newDonationDto.SelectedDonationCategoryId
            };

            string path = FileOperations.GenerateFilePath(wwwrootFolder, PhotosFolder, newDonationDto.Photo);
            if (FileOperations.Upload(newDonationDto.Photo, path))
            {
                newDonationViewModel.NewHeaderPhoto = new Application.ViewModels.SaveFile.NewSaveFileViewModel()
                {
                    Path = path
                };
            }
            else//not get null exception in service
            {
                newDonationViewModel.NewHeaderPhoto = new Application.ViewModels.SaveFile.NewSaveFileViewModel()
                {
                    Path = path
                };
            }

            _donationService.AddNew(newDonationViewModel);
            _donationService.Save();

            return CreatedAtAction("Get", newDonationDto);
        }

        [HttpPut]
        public IActionResult Edit([FromForm] EditDonationDto editDonationDto)
        {
            var editDonationVM = new EditDonationViewModel()
            {
                Id = editDonationDto.Id,
                DonationCategoryId = editDonationDto.DonationCategoryId
            };

            _donationService.Edit(editDonationVM);

            string includes = "TitleLocalizationSet.Localizations,MetaTitleLocalizationSet.Localizations,ContentLocalizationSet.Localizations";

            if (editDonationDto.NewPhoto != null)
                includes += ",HeaderPhoto";

            var donation = _donationRepository.Get(editDonationDto.Id, includes);

            //uploading new photo
            if (editDonationDto.NewPhoto != null)
            {
                string filePath = _saveFileService.GetSaveFile(donation.HeaderPhoto.Id).FilePath;
                FileOperations.Upload(editDonationDto.NewPhoto, filePath);
            }

            foreach (var titleTranslation in editDonationDto.TitleLocalizations)
            {
                string cultureCode = titleTranslation.CultureCode;
                int setId = titleTranslation.LocalizationSetId;
                string value = titleTranslation.Value;

                if (_localizationService.Exists(setId, cultureCode))
                {
                    _localizationService.Update(new Application.ViewModels.Localization.EditLocalizationViewModel()
                    {
                        LocalizationSetId = setId,
                        CultureCode = cultureCode,
                        Value = value
                    });
                }
                else
                {
                    _localizationService.Add(setId, new Application.ViewModels.Localization.NewLocalizationViewModel()
                    {
                        CultureCode = cultureCode,
                        Value = value
                    });
                }
            }

            foreach (var metaTitleTranslation in editDonationDto.MetaTitleLocalizations)
            {
                string cultureCode = metaTitleTranslation.CultureCode;
                int setId = metaTitleTranslation.LocalizationSetId;
                string value = metaTitleTranslation.Value;

                if (_localizationService.Exists(setId, cultureCode))
                {
                    _localizationService.Update(new Application.ViewModels.Localization.EditLocalizationViewModel()
                    {
                        LocalizationSetId = setId,
                        CultureCode = cultureCode,
                        Value = value
                    });
                }
                else
                {
                    _localizationService.Add(setId, new Application.ViewModels.Localization.NewLocalizationViewModel()
                    {
                        CultureCode = cultureCode,
                        Value = value
                    });
                }
            }

            foreach (var contentTranslation in editDonationDto.ContentLocalizations)
            {
                string cultureCode = contentTranslation.CultureCode;
                int setId = contentTranslation.LocalizationSetId;
                string value = contentTranslation.Value;

                if (_localizationService.Exists(setId, cultureCode))
                {
                    _localizationService.Update(new Application.ViewModels.Localization.EditLocalizationViewModel()
                    {
                        LocalizationSetId = setId,
                        CultureCode = cultureCode,
                        Value = value
                    });
                }
                else
                {
                    _localizationService.Add(setId, new Application.ViewModels.Localization.NewLocalizationViewModel()
                    {
                        CultureCode = cultureCode,
                        Value = value
                    });
                }
            }

            _localizationService.Save();
            _donationService.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_donationService.Exists(id))
                return NotFound();

            var donation = _donationRepository.Get(id, "HeaderPhoto,TitleLocalizationSet,MetaTitleLocalizationSet,ContentLocalizationSet");

            _donationService.Delete(id);

            if (FileOperations.DeleteFile(donation.HeaderPhoto.FilePath))
            {
                _saveFileService.Delete(donation.HeaderPhoto.Id);
            }

            _localizationSetService.Delete(donation.TitleLocalizationSet.Id);
            _localizationSetService.Delete(donation.MetaTitleLocalizationSet.Id);
            _localizationSetService.Delete(donation.ContentLocalizationSet.Id);

            _donationService.Save();
            _saveFileService.Save();
            _localizationSetService.Save();

            return Ok();
        }
    }
}
