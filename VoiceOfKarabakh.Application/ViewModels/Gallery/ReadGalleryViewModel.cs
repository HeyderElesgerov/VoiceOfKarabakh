using System.Collections.Generic;
using VoiceOfKarabakh.Application.ViewModels.SaveFile;

namespace VoiceOfKarabakh.Application.ViewModels.Gallery
{
    public class ReadGalleryViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<SaveFileViewModel> Photos { get; set; }
    }
}
