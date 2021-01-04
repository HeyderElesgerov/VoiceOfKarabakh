using System;
using System.Collections.Generic;
using System.Text;
using VoiceOfKarabakh.Application.ViewModels.Common;
using VoiceOfKarabakh.Application.ViewModels.Gallery;
using VoiceOfKarabakh.Application.ViewModels.SaveFile;

namespace VoiceOfKarabakh.Application.Interfaces.Gallery
{
    public interface IGallerySevice
    {
        public void AddNew(NewGalleryViewModel newGalleryViewModel);

        public void ReInitPhotos(int galleryId, IEnumerable<Domain.Models.SavedFile> savedFiles);
    
        public void Delete(int id);

        public void Save();

        //for user index page
        public PaginatedElements<GalleryIndexViewModel> GetGalleryIndexVms(
            string cultureCode, int count, int page, string includes = null);

        //for admin index page
        public IEnumerable<GalleryIndexViewModel> GetAllGalleryIndexViewModel(string cultureCode, string includes = null);

        //to read for user
        public ReadGalleryViewModel GetReadGalleryViewModel(int id, string cultureCode, string includes = null);

        //for editing iterface
        public EditGalleryViewModel GetEditGalleryViewModel(int id, string includes = null);
    }
}
