using System.Collections.Generic;
using VoiceOfKarabakh.Domain.Models;

namespace VoiceOfKarabakh.Domain.Interfaces.SaveFile
{
    public interface ISaveFileRepository
    {
        SavedFile GetSavedFile(int id);

        IEnumerable<SavedFile> GetAllSavedFiles();

        void Add(SavedFile savedFile);

        void Delete(int id);

        bool Exists(int id);

        void Save();
    }
}
