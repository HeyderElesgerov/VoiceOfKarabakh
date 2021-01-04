using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceOfKarabakh.Domain.Interfaces.Localization;
using VoiceOfKarabakh.Infrastructure.Data;

namespace VoiceOfKarabakh.Infrastructure.Repository.Localization
{
    public class LocalizationRepository : ILocalizationRepository
    {
        private readonly ApplicationDbContext _context;

        public LocalizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddLocalization(Domain.Models.Localization newLocalization)
        {
            _context.Localizations.Add(newLocalization);
        }

        public void Delete(int setId, string cultureCode)
        {
            if (!Exists(setId, cultureCode))
            {
                throw new ArgumentNullException();
            }

            _context.Localizations.Remove(GetLocalization(setId, cultureCode));
        }

        public Domain.Models.Localization GetLocalization(int setId, string cultureCode)
        {
            return _context.Localizations.FirstOrDefault(l => l.LocalizationSetId == setId && l.CultureCode == cultureCode);
        }

        public IEnumerable<Domain.Models.Localization> GetLocalizationsBySet(int setId)
        {
            return _context.Localizations.Where(l => l.LocalizationSetId == setId).AsEnumerable();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Domain.Models.Localization editedLocalization)
        {
            string cultureCode = editedLocalization.CultureCode;
            int setId = editedLocalization.LocalizationSetId;
            var loc = GetLocalization(setId, cultureCode);

            if (!Exists(setId, cultureCode))
            {
                throw new ArgumentNullException();
            }

            loc.Value = editedLocalization.Value;
        }

        public bool Exists(int setId, string cultureCode)
        {
            return _context.Localizations.Any(l => l.LocalizationSetId == setId && l.CultureCode == cultureCode);
        }
    }
}
