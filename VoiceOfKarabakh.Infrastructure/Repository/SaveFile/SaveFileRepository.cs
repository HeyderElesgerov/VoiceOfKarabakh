using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceOfKarabakh.Domain.Interfaces.SaveFile;
using VoiceOfKarabakh.Domain.Models;
using VoiceOfKarabakh.Infrastructure.Data;

namespace VoiceOfKarabakh.Infrastructure.Repository.SaveFile
{
    class SaveFileRepository : ISaveFileRepository
    {
        private readonly ApplicationDbContext _context;

        public SaveFileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(SavedFile savedFile)
        {
            _context.SavedFiles.Add(savedFile);
        }

        public void Delete(int id)
        {
            if (!Exists(id))
                throw new ArgumentNullException();

            _context.SavedFiles.Remove(GetSavedFile(id));
        }

        public bool Exists(int id)
        {
            return _context.SavedFiles.Any(f => f.Id == id);
        }

        public IEnumerable<SavedFile> GetAllSavedFiles()
        {
            return _context.SavedFiles.AsEnumerable();
        }

        public SavedFile GetSavedFile(int id)
        {
            return _context.SavedFiles.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
