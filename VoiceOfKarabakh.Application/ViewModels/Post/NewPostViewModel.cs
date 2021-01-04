using System.Collections.Generic;
using VoiceOfKarabakh.Application.ViewModels.Localization;
using VoiceOfKarabakh.Application.ViewModels.SaveFile;

namespace VoiceOfKarabakh.Application.ViewModels.Post
{
    public class NewPostViewModel
    {
        public string AuthorId { get; set; }

        public int ReadingTime { get; set; }

        public bool Drafted { get; set; }

        public NewSaveFileViewModel NewPhoto { get; set; }
        
        public List<Domain.Models.Category> SelectedCategories { get; set; }
        public List<Domain.Models.Tag> SelectedTags { get; set; }

        public List<NewLocalizationViewModel> TitleLocalizations { get; set; }
        public List<NewLocalizationViewModel> MetaTitleLocalizations { get; set; }
        public List<NewLocalizationViewModel> ContentLocalizations { get; set; }
    }
}
