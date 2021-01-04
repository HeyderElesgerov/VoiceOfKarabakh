using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceOfKarabakh.Application.Interfaces.Gallery;
using VoiceOfKarabakh.Application.ViewModels.Common;
using VoiceOfKarabakh.Application.ViewModels.Gallery;
using VoiceOfKarabakh.Application.ViewModels.SaveFile;
using VoiceOfKarabakh.Domain.Interfaces.Gallery;

namespace VoiceOfKarabakh.Application.Services.Gallery
{
    public class GalleryService : IGallerySevice
    {
        private readonly IGalleryRepository _galleryRepository;

        public GalleryService(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        public void AddNew(NewGalleryViewModel newGalleryViewModel)
        {
            Domain.Models.Gallery newGallery = new Domain.Models.Gallery()
            {
                AddedTime = DateTime.Now,
                TitleLocalizationSet = new Domain.Models.LocalizationSet()
                {
                    Localizations = new List<Domain.Models.Localization>()
                },
                Photos = new List<Domain.Models.SavedFile>()
            };

            foreach(var titleTranslation in newGalleryViewModel.TitleTranslations)
            {
                string title = titleTranslation.Value != null ? titleTranslation.Value : "";

                newGallery.TitleLocalizationSet.Localizations.Add(new Domain.Models.Localization()
                {
                    CultureCode = titleTranslation.CultureCode,
                    Value = title
                });
            }

            foreach(var newPhoto in newGalleryViewModel.NewPhotoViewModels)
            {
                newGallery.Photos.Add(new Domain.Models.SavedFile()
                {
                    FilePath = newPhoto.Path
                });
            }

            _galleryRepository.Add(newGallery);
        }

        public void Delete(int id)
        {
            _galleryRepository.Delete(id);
        }

        public IEnumerable<GalleryIndexViewModel> GetAllGalleryIndexViewModel(string cultureCode, string includes = null)
        {
            var galleries = _galleryRepository.GetGalleries(includes);

            List<GalleryIndexViewModel> galleryIndexViewModels = new List<GalleryIndexViewModel>();

            foreach(var gallery in galleries)
            {
                GalleryIndexViewModel galleryIndexViewModel = new GalleryIndexViewModel();
                galleryIndexViewModel.Id = gallery.Id;

                if (gallery.TitleLocalizationSet != null && gallery.TitleLocalizationSet.Localizations != null)
                {
                    var titleLoc = gallery.TitleLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);
                    galleryIndexViewModel.Title = titleLoc != null ? titleLoc.Value : "";
                }

                if(gallery.Photos != null)
                {
                    galleryIndexViewModel.HeaderPhotoPath = gallery.Photos.First().FileName;//first photo is header photo
                }

                galleryIndexViewModels.Add(galleryIndexViewModel);
            }

            return galleryIndexViewModels;
        }

        public EditGalleryViewModel GetEditGalleryViewModel(int id, string includes = null)
        {
            var gallery = _galleryRepository.GetGallery(id, includes);

            EditGalleryViewModel editGalleryViewModel = new EditGalleryViewModel()
            {
                GalleryId = id,
                TitleTranslations = new List<ViewModels.Localization.EditLocalizationViewModel>(),
                Photos = new List<ViewModels.SaveFile.NewSaveFileViewModel>()
            };

            if(gallery.TitleLocalizationSet != null && gallery.TitleLocalizationSet.Localizations != null)
            {
                foreach(var titleLoc in gallery.TitleLocalizationSet.Localizations)
                {
                    editGalleryViewModel.TitleTranslations.Add(new ViewModels.Localization.EditLocalizationViewModel()
                    {
                        CultureCode = titleLoc.CultureCode,
                        LocalizationSetId = titleLoc.LocalizationSetId,
                        Value = titleLoc.Value != null ? titleLoc.Value : ""
                    });
                }
            }

            if(gallery.Photos != null)
            {
                foreach(var photo in gallery.Photos)
                {
                    editGalleryViewModel.Photos.Add(new ViewModels.SaveFile.NewSaveFileViewModel()
                    {
                        Path = photo.FileName
                    });
                }
            }

            return editGalleryViewModel;
        }

        public PaginatedElements<GalleryIndexViewModel> GetGalleryIndexVms(string cultureCode, int count, int page, string includes = null)
        {
            var galleries = _galleryRepository.GetGalleries(includes);

            int maxElementCount = galleries.Count();

            List<GalleryIndexViewModel> galleryIndexViewModels = new List<GalleryIndexViewModel>();

            foreach(var gallery in galleries.OrderByDescending(p => p.AddedTime)
                                            .Skip((page-1)*count).Take(count))
            {
                GalleryIndexViewModel galleryIndexViewModel = new GalleryIndexViewModel();
                galleryIndexViewModel.Id = gallery.Id;

                if (gallery.TitleLocalizationSet != null && gallery.TitleLocalizationSet.Localizations != null)
                {
                    var titleLoc = gallery.TitleLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);
                    galleryIndexViewModel.Title = titleLoc != null ? titleLoc.Value : "";
                }

                if (gallery.Photos != null)
                {
                    galleryIndexViewModel.HeaderPhotoPath = gallery.Photos.First().FileName;//first photo is header photo
                }

                galleryIndexViewModels.Add(galleryIndexViewModel);
            }

            var paginatedVM = new PaginatedElements<GalleryIndexViewModel>(galleryIndexViewModels, count, page, maxElementCount);

            return paginatedVM;
        }

        public ReadGalleryViewModel GetReadGalleryViewModel(int id, string cultureCode, string includes = null)
        {
            var gallery = _galleryRepository.GetGallery(id, includes);

            ReadGalleryViewModel readGalleryViewModel = new ReadGalleryViewModel()
            {
                Id = gallery.Id,
                Photos = new List<ViewModels.SaveFile.SaveFileViewModel>()
            };

            if (gallery.TitleLocalizationSet != null && gallery.TitleLocalizationSet.Localizations != null)
            {
                var loc = gallery.TitleLocalizationSet.Localizations.FirstOrDefault(l => l.CultureCode == cultureCode);
                readGalleryViewModel.Title = loc.Value != null ? loc.Value : "";
            }

            if (gallery.Photos != null)
            {
                foreach (var photo in gallery.Photos)
                {
                    readGalleryViewModel.Photos.Add(new ViewModels.SaveFile.SaveFileViewModel()
                    {
                        Id = photo.Id,
                        FilePath = photo.FileName
                    });
                }
            }

            return readGalleryViewModel;
        }

        public void Save()
        {
            _galleryRepository.Save();
        }

        public void ReInitPhotos(int galleryId, IEnumerable<Domain.Models.SavedFile> savedFiles)
        {
            var gallery = _galleryRepository.GetGallery(galleryId, "Photos");

            gallery.Photos.ToList().RemoveAll(p => true);
            gallery.Photos = savedFiles.ToList();

            _galleryRepository.Update(galleryId, gallery);
        }
    }
}
