using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VoiceOfKarabakh.Application.Interfaces.SaveFile;
using VoiceOfKarabakh.Application.Options;
using VoiceOfKarabakh.Application.Utility;
using VoiceOfKarabakh.Application.ViewModels.SaveFile;
using VoiceOfKarabakh.Domain.Interfaces.SaveFile;
using VoiceOfKarabakh.Domain.Models;

namespace VoiceOfKarabakh.Application.Services.SaveFile
{
    public class SaveFileService : ISaveFileService
    {
        private readonly ISaveFileRepository _saveFileRepository;
        string PhotosFolder;

        public SaveFileService(ISaveFileRepository saveFileRepository, IOptions<FilePathOption> options)
        {
            _saveFileRepository = saveFileRepository;
            PhotosFolder = options.Value.PhotosFolder;
        }

        public void Add(NewSaveFileViewModel newSaveFileViewModel)
        {
            _saveFileRepository.Add(new SavedFile()
            {
                FilePath = newSaveFileViewModel.Path
            });
        }

        public void Delete(int id)
        {
            if (!Exists(id))
                throw new ArgumentNullException();

            var savedFile = GetSaveFile(id);
            _saveFileRepository.Delete(id);
        }

        //we just change content in saved file path, we dont change path of file
        /* public void Edit(EditSaveFileViewModel editSaveFileViewModel)
         {
             var savedFile = GetSaveFile(editSaveFileViewModel.SaveFileId);
             FileOperations.Upload(editSaveFileViewModel.NewFile, savedFile.FilePath);
         }*/

        public bool Exists(int id)
        {
            return _saveFileRepository.Exists(id);
        }

        public SaveFileViewModel GetSaveFile(int id)
        {
            if (!Exists(id))
                throw new ArgumentNullException();

            var savedFile = _saveFileRepository.GetSavedFile(id);

            SaveFileViewModel saveFileViewModel = new SaveFileViewModel()
            {
                FilePath = savedFile.FilePath,
                Id = id
            };

            return saveFileViewModel;
        }

        public IEnumerable<SaveFileViewModel> GetSaveFiles()
        {
            List<SaveFileViewModel> saveFileViewModels = new List<SaveFileViewModel>();

            foreach(var savedFile in _saveFileRepository.GetAllSavedFiles())
            {
                saveFileViewModels.Add(new SaveFileViewModel()
                {
                    FilePath = savedFile.FilePath,
                    Id = savedFile.Id
                });
            }

            return saveFileViewModels;
        }

        public void Save()
        {
            _saveFileRepository.Save();
        }
    }
}
