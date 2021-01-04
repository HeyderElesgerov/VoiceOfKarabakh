using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using VoiceOfKarabakh.Application.ViewModels.SaveFile;
using VoiceOfKarabakh.Domain.Models;

namespace VoiceOfKarabakh.Application.Interfaces.SaveFile
{
    public interface ISaveFileService
    {
        SaveFileViewModel GetSaveFile(int id);

        IEnumerable<SaveFileViewModel> GetSaveFiles();

        void Add(NewSaveFileViewModel newSaveFileViewModel);
        /*
        void Edit(EditSaveFileViewModel editSaveFileViewModel);
        */
        void Delete(int id);

        bool Exists(int id);

        void Save();
    }
}
