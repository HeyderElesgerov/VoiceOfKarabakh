using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceOfKarabakh.Domain.Interfaces.Gallery;
using VoiceOfKarabakh.Infrastructure.Data;

namespace VoiceOfKarabakh.Infrastructure.Repository.Gallery
{
    public class GalleryRepository : IGalleryRepository
    {
        private readonly ApplicationDbContext _context;

        public GalleryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Domain.Models.Gallery gallery)
        {
            _context.Galleries.Add(gallery);
        }

        public void Delete(int id)
        {
            _context.Galleries.Remove(GetGallery(id));
        }

        public IEnumerable<Domain.Models.Gallery> GetGalleries(string includes = null)
        {
            var galleries = _context.Galleries.AsQueryable();

            if(includes != null)
            {
                foreach(string include in includes.Split(','))
                {
                    galleries = galleries.Include(include);
                }
            }

            return galleries.AsEnumerable();
        }

        public Domain.Models.Gallery GetGallery(int id, string includes = null)
        {
            if(includes != null)
            {
                var gallery = _context.Galleries.Where(g => g.Id == id);

                foreach(string include in includes.Split(','))
                {
                    gallery = gallery.Include(include);
                }

                return gallery.FirstOrDefault();
            }

            return _context.Galleries.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(int id, Domain.Models.Gallery gallery)
        {
            var existingGallery = GetGallery(id);

            existingGallery.Photos = gallery.Photos;
        }
    }
}
