using System;
using System.Collections.Generic;
using System.Text;
using VoiceOfKarabakh.Domain.Models;

namespace VoiceOfKarabakh.Domain.Interfaces.Gallery
{
    public interface IGalleryRepository
    {
        Models.Gallery GetGallery(int id, string includes = null);

        IEnumerable<Models.Gallery> GetGalleries(string includes = null);

        void Add(Models.Gallery gallery);

        void Delete(int id);

        void Update(int id, Models.Gallery gallery);

        void Save();
    }
}
