using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VoiceOfKarabakh.Domain.Models;
using VoiceOfKarabakh.Domain.Models.Donation;
using VoiceOfKarabakh.Domain.Models.DonationCategory;
using VoiceOfKarabakh.Domain.Models.Posts;

namespace VoiceOfKarabakh.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LocalizationSet> LocalizationSets { get; set; }
        public DbSet<Localization> Localizations { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<NewsPost> NewsPosts { get; set; }
        public DbSet<HistoryPost> HistoryPosts { get; set; }
        public DbSet<CelebrityPost> CelebrityPosts { get; set; }
        public DbSet<MonumentPost> MonumentPosts { get; set; }
        public DbSet<CulturalHeritagePost> CulturalHeritagePosts { get; set; }
        
        public DbSet<SavedFile> SavedFiles { get; set; }

        public DbSet<Gallery> Galleries { get; set; }

        public DbSet<DonationCategory> DonationCategories { get; set; }
        public DbSet<Donation> Donations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Localization
            builder.Entity<Localization>()
                .HasOne(p => p.LocalizationSet)
                .WithMany(set => set.Localizations)
                .HasForeignKey(p => p.LocalizationSetId);

            builder.Entity<LocalizationSet>()
                .HasMany(p => p.Localizations)
                .WithOne(p => p.LocalizationSet)
                .HasForeignKey(p => p.LocalizationSetId);

            builder.Entity<Localization>()
                .HasKey(l => new { l.LocalizationSetId, l.CultureCode });

            builder.Entity<Localization>()
                .Property(p => p.Value).IsRequired();

            builder.Entity<Post>()
                .Property(p => p.AuthorId).IsRequired();
            builder.Entity<Post>()
                .Property(p => p.Created).IsRequired();
            builder.Entity<Post>()
                .Property(p => p.ReadingCount).IsRequired();
            builder.Entity<Post>()
                .Property(p => p.ReadingTime).IsRequired();

            builder.Entity<SavedFile>()
                .Property(p => p.FilePath).IsRequired();

            builder.Entity<Gallery>()
                .Property(p => p.AddedTime).IsRequired();

            builder.Entity<Donation>()
                .HasOne(p => p.DonationCategory)
                .WithMany(p => p.Donations)
                .HasForeignKey(p => p.DonationCategoryId);

            base.OnModelCreating(builder);
        }
    }
}
