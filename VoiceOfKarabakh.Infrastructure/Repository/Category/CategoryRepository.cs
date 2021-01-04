using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VoiceOfKarabakh.Domain.Interfaces.Category;
using VoiceOfKarabakh.Infrastructure.Data;

namespace VoiceOfKarabakh.Infrastructure.Repository.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Domain.Models.Category category)
        {
            _context.Categories.Add(category);
        }

        public void Delete(int id)
        {
            var category = GetCategory(id, null);
            _context.Categories.Remove(category);
        }

        public IEnumerable<Domain.Models.Category> GetCategories(Expression<Func<Domain.Models.Category, bool>>[] filters = null, string includes = null)
        {
            var categories = _context.Categories.AsQueryable();

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    categories = categories.Where(filter);
                }
            }

            if (includes != null)
            {
                foreach (string include in includes.Split(','))
                {
                    categories = categories.Include(include);
                }
            }

            return categories;
        }

        public Domain.Models.Category GetCategory(int id, string includes = null)
        {
            if (includes != null)
            {
                IQueryable<Domain.Models.Category> categories = _context.Categories.Where(c => c.Id == id);

                foreach (var include in includes.Split(','))
                {
                    categories = categories.Include(include);
                }

                return categories.FirstOrDefault();
            }

            return _context.Categories.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
