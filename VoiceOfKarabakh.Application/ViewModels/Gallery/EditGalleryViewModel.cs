using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.Application.ViewModels.Gallery
{
    public class EditGalleryViewModel
    {
        public int GalleryId { get; set; }
        
        public List<SaveFile.NewSaveFileViewModel> Photos { get; set; }

        public List<EditLocalizationViewModel> TitleTranslations { get; set; }
    }
}
