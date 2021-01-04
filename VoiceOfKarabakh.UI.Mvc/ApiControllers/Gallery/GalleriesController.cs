using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using VoiceOfKarabakh.Application.Interfaces.Gallery;
using VoiceOfKarabakh.Application.Interfaces.Localization;
using VoiceOfKarabakh.Application.Interfaces.LocalizationSet;
using VoiceOfKarabakh.Application.Interfaces.SaveFile;
using VoiceOfKarabakh.Application.Options;
using VoiceOfKarabakh.Application.Utility;
using VoiceOfKarabakh.Application.ViewModels.Gallery;
using VoiceOfKarabakh.Domain.Interfaces.Gallery;
using VoiceOfKarabakh.UI.Mvc.Models.Dto;

namespace VoiceOfKarabakh.UI.Mvc.ApiControllers.Gallery
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class GalleriesController : ControllerBase
    {
        private readonly IGallerySevice _gallerySevice;
        private readonly IGalleryRepository _galleryRepository;
        private readonly ILocalizationSetService _localizationSetService;
        private readonly ILocalizationService _localizationService;
        private readonly ISaveFileService _saveFileService;
        string wwwrootFolder;
        string photosFolder;

        public GalleriesController(IGallerySevice gallerySevice, IGalleryRepository galleryRepository, ILocalizationSetService localizationSetService, ILocalizationService localizationService, IWebHostEnvironment webhostenvirnoment, ISaveFileService saveFileService, IOptions<FilePathOption> filePathOpt)
        {
            _gallerySevice = gallerySevice;
            _localizationSetService = localizationSetService;
            _localizationService = localizationService;
            _saveFileService = saveFileService;

            _galleryRepository = galleryRepository;

            wwwrootFolder = webhostenvirnoment.WebRootPath;
            photosFolder = filePathOpt.Value.PhotosFolder;
        }

        [HttpGet, Route("[action]/{cultureCode}")]
        public IActionResult GetAdminIndexViewModels(string cultureCode)
        {
            var vms = _gallerySevice.GetAllGalleryIndexViewModel(cultureCode, "TitleLocalizationSet.Localizations");
            return Ok(vms);
        }

        [HttpGet, Route("[action]/{culturecode}/{page}/{count}")]
        [AllowAnonymous]
        public IActionResult GetPaginatedIndexViewModels(string cultureCode, int page, int count)
        {
            var vms = _gallerySevice.GetGalleryIndexVms(cultureCode, count, page, "TitleLocalizationSet.Localizations,Photos");
            return Ok(vms);
        }

        [HttpGet, Route("[action]/{id}")]
        public IActionResult GetEditGalleryViewModel(int id)
        {
            var editGalleryVM = _gallerySevice.GetEditGalleryViewModel(id, "Photos,TitleLocalizationSet.Localizations");

            return Ok(editGalleryVM);
        }

        [HttpGet, Route("[action]/{id}/{cultureCode}")]
        [AllowAnonymous]
        public IActionResult GetReadGalleryViewModel(int id, string cultureCode)
        {
            var vm = _gallerySevice.GetReadGalleryViewModel(id, cultureCode, "Photos,TitleLocalizationSet.Localizations");

            if (vm == null)
                return NotFound();

            return Ok(vm);
        }

        [HttpPost]
        public IActionResult AddNew([FromForm] NewGalleryDto newGalleryDto)
        {
            NewGalleryViewModel newGalleryViewModel = new NewGalleryViewModel()
            {
                TitleTranslations = newGalleryDto.TitleLocalizations,
                NewPhotoViewModels = new List<Application.ViewModels.SaveFile.NewSaveFileViewModel>()
            };

            foreach (var photo in newGalleryDto.Photos)
            {
                string path = FileOperations.GenerateFilePath(wwwrootFolder, photosFolder, photo);

                if (FileOperations.Upload(photo, path))
                {
                    newGalleryViewModel.NewPhotoViewModels.Add(new Application.ViewModels.SaveFile.NewSaveFileViewModel()
                    {
                        Path = path
                    });
                }
            }

            _gallerySevice.AddNew(newGalleryViewModel);

            _gallerySevice.Save();
            return CreatedAtAction(nameof(GetAdminIndexViewModels), null);
        }

        [HttpPut]
        public IActionResult Edit([FromForm] EditGalleryDto editGalleryDto)
        {
            var gallery = _galleryRepository.GetGallery(editGalleryDto.GalleryId, "Photos");

            if (gallery == null)
                return NotFound();

            foreach (var titleTranslation in editGalleryDto.TitleLocalizations)
            {
                int setId = titleTranslation.LocalizationSetId;
                string culture = titleTranslation.CultureCode;
                string value = titleTranslation.Value != null ? titleTranslation.Value : "";

                if (_localizationService.Exists(setId, culture))
                {
                    _localizationService.Update(new Application.ViewModels.Localization.EditLocalizationViewModel()
                    {
                        CultureCode = culture,
                        LocalizationSetId = setId,
                        Value = value
                    });
                }
                else
                {
                    _localizationService.Add(setId, new Application.ViewModels.Localization.NewLocalizationViewModel()
                    {
                        CultureCode = culture,
                        Value = value
                    });
                }
            }

            List<Domain.Models.SavedFile> photos = new List<Domain.Models.SavedFile>();

            //we delete all saved files related to this gallery and recreate selected saved files.
            //we need to delete physically, then from database by reiniting in gallery service
            foreach (var existingPhoto in gallery.Photos)
            {
                FileOperations.DeleteFile(existingPhoto.FilePath);
            }

            //now we are recreating photos
            foreach (var photo in editGalleryDto.Photos)
            {
                string path = FileOperations.GenerateFilePath(wwwrootFolder, photosFolder, photo);

                if (FileOperations.Upload(photo, path))
                {
                    photos.Add(new Domain.Models.SavedFile()
                    {
                        FilePath = path
                    });
                }
            }

            _gallerySevice.ReInitPhotos(editGalleryDto.GalleryId, photos);

            _localizationService.Save();
            _gallerySevice.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var gallery = _galleryRepository.GetGallery(id, "Photos,TitleLocalizationSet");

            if (gallery == null)
                return NotFound();

            foreach (var photo in gallery.Photos)
            {
                if (FileOperations.DeleteFile(photo.FilePath))
                {
                    _saveFileService.Delete(photo.Id);
                }
            }

            _localizationSetService.Delete(gallery.TitleLocalizationSet.Id);
            _gallerySevice.Delete(id);

            _galleryRepository.Save();
            _saveFileService.Save();
            _localizationSetService.Save();

            return Ok();
        }
    }
}
