using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VoiceOfKarabakh.Domain.Interfaces.Donation;
using VoiceOfKarabakh.Infrastructure.Data;

namespace VoiceOfKarabakh.Infrastructure.Repository.Donation
{
    public class DonationRepository : IDonationRepository
    {
        private readonly ApplicationDbContext _context;

        public DonationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Domain.Models.Donation.Donation donation)
        {
            _context.Donations.Add(donation);
        }

        public void Delete(int id)
        {
            if (!Exists(id))
                throw new ArgumentNullException();

            _context.Donations.Remove(Get(id));
        }

        public bool Exists(int id)
        {
            return _context.Donations.Find(id) != null;
        }

        public Domain.Models.Donation.Donation Get(int id, string includes = null)
        {
            if(includes != null)
            {
                var getDonationQuery = _context.Donations.Where(d => d.Id == id);

                foreach(string include in includes.Split(','))
                {
                    getDonationQuery = getDonationQuery.Include(include);
                }

                return getDonationQuery.FirstOrDefault(d => d.Id == id);
            }

            return _context.Donations.Find(id);
        }

        public IEnumerable<Domain.Models.Donation.Donation> GetAll(string includes = null)
        {
            var donationsAsQueryable = _context.Donations.AsQueryable();

            if(includes != null)
            {
                foreach (string include in includes.Split(','))
                {
                    donationsAsQueryable = donationsAsQueryable.Include(include);
                }
            }

            return donationsAsQueryable.AsEnumerable();
        }

        public IEnumerable<Domain.Models.Donation.Donation> GetByDonationCategory(int donationCategoryId, string includes = null)
        {
            var donations = _context.Donations.Where(d => d.DonationCategoryId == donationCategoryId);

            if(includes != null)
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

        public void Update(int id, Domain.Models.Donation.Donation editedDonation)
        {
            if (!Exists(id))
                throw new ArgumentNullException();

            var donation = Get(id);

            donation.DonationCategoryId = editedDonation.DonationCategoryId;
        }
    }
}
