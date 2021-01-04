using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VoiceOfKarabakh.Domain.Interfaces.Tag;
using VoiceOfKarabakh.Infrastructure.Data;

namespace VoiceOfKarabakh.Infrastructure.Repository.Tag
{
    class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Domain.Models.Tag newTag)
        {
            _context.Tags.Add(newTag);
        }

        public void Delete(int id)
        {
            if (!Exists(id))
                throw new ArgumentNullException();

            _context.Tags.Remove(GetTag(id));
        }

        public bool Exists(int id)
        {
            return _context.Tags.Any(t => t.Id == id);
        }

        public Domain.Models.Tag GetTag(int id, string includes = null)
        {
            if(includes != null)
            {
                var tags = _context.Tags.Where(t => t.Id == id);

                foreach(string include in includes.Split(','))
                {
                    tags = tags.Include(include);
                }

                return tags.FirstOrDefault();
            }

            return _context.Tags.Find(id);
        }

        public IEnumerable<Domain.Models.Tag> GetTags(Expression<Func<Domain.Models.Tag, bool>>[] filters = null, string includes = null)
        {
            var tags = _context.Tags.AsQueryable();

            if (includes != null)
            {
                foreach (string include in includes.Split(','))
                {
                    tags = tags.Include(include);
                }

            }

            if(filters != null)
            {
                foreach(var filter in filters)
                {
                    tags = tags.Where(filter);
                }
            }

            return tags;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
