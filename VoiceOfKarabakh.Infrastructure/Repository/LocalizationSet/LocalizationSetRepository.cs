using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceOfKarabakh.Domain.Interfaces.LocalizationSet;
using VoiceOfKarabakh.Infrastructure.Data;

namespace VoiceOfKarabakh.Infrastructure.Repository.LocalizationSet
{
    public class LocalizationSetRepository : ILocalizationSetRepository
    {
        readonly ApplicationDbContext _context;

        public LocalizationSetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddLocalizationSet(Domain.Models.LocalizationSet localizationSet)
        {
            _context.LocalizationSets.Add(localizationSet);
        }

        public void DeleteLocalizationSet(int setId)
        {
            var set = GetLocalizationSetById(setId);

            if (set == null)
                throw new ArgumentNullException();

            _context.LocalizationSets.Remove(set);
        }

        public Domain.Models.LocalizationSet GetLocalizationSetById(int id, string includes = null)
        {
            if(includes != null)
            {
                var localizationSetQuery = _context.LocalizationSets.Where(l => l.Id == id);

                foreach(var include in includes.Split(','))
                {
                    localizationSetQuery = localizationSetQuery.Include(include);
                }

                return localizationSetQuery.FirstOrDefault();
            }

            return _context.LocalizationSets.FirstOrDefault(l => l.Id == id);
        }

        public IEnumerable<Domain.Models.LocalizationSet> GetLocalizationSets(string includes = null)
        {
            if(includes != null)
            {
                var localizationsQuery = _context.LocalizationSets.AsQueryable();

                foreach (var include in includes.Split(','))
                {
                    localizationsQuery = localizationsQuery.Include(include);
                }

                return localizationsQuery.AsEnumerable();
            }
            return _context.LocalizationSets.AsEnumerable();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
