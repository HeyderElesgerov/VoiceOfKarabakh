using System.Collections.Generic;
using VoiceOfKarabakh.Application.ViewModels.Localization;

namespace VoiceOfKarabakh.Application.ViewModels.Gallery
{
    public class NewGalleryViewModel
    {
        public List<NewLocalizationViewModel> TitleTranslations { get; set; }

        public List<SaveFile.NewSaveFileViewModel> NewPhotoViewModels { get; set; }
    }
}
