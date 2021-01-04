using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VoiceOfKarabakh.Domain.Interfaces.DonationCategory;
using VoiceOfKarabakh.Domain.Models.DonationCategory;
using VoiceOfKarabakh.Infrastructure.Data;

namespace VoiceOfKarabakh.Infrastructure.Repository
{
    public class DonationCategoryRepository : IDonationCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public DonationCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(DonationCategory newDonationCategory)
        {
            _context.DonationCategories.Add(newDonationCategory);
        }

        public void Delete(int id)
        {
            if (!Exists(id))
                throw new ArgumentNullException();

            _context.DonationCategories.Remove(Get(id));
        }

        public bool Exists(int id)
        {
            return _context.DonationCategories.Find(id) != null;
        }

        public DonationCategory Get(int id, string includes = null)
        {
            if (!Exists(id))
                throw new ArgumentNullException();

            if(includes != null)
            {
                var donationCategoryQuery = _context.DonationCategories.Where(c => c.Id == id);

                foreach(string include in includes.Split(','))
                {
                    donationCategoryQuery = donationCategoryQuery.Include(include);
                }

                return donationCategoryQuery.First(c => c.Id == id);
            }

            return _context.DonationCategories.First(d => d.Id == id);
        }

        public IEnumerable<DonationCategory> GetAll(string includes = null)
        {
            var donations = _context.DonationCategories.AsQueryable();

            if (includes != null)
            {
                foreach(string include in includes.Split(','))
                {
                    donations = donations.Include(include);
                }
            }

            return donations.AsEnumerable();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
