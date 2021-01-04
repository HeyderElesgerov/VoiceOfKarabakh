using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VoiceOfKarabakh.Application.Interfaces;
using VoiceOfKarabakh.Application.Interfaces.Category;
using VoiceOfKarabakh.Application.Interfaces.Donation;
using VoiceOfKarabakh.Application.Interfaces.DonationCategory;
using VoiceOfKarabakh.Application.Interfaces.Email;
using VoiceOfKarabakh.Application.Interfaces.Gallery;
using VoiceOfKarabakh.Application.Interfaces.Localization;
using VoiceOfKarabakh.Application.Interfaces.LocalizationSet;
using VoiceOfKarabakh.Application.Interfaces.PaymentCart;
using VoiceOfKarabakh.Application.Interfaces.SaveFile;
using VoiceOfKarabakh.Application.Interfaces.Tag;
using VoiceOfKarabakh.Application.Options;
using VoiceOfKarabakh.Application.Services.Category;
using VoiceOfKarabakh.Application.Services.Donation;
using VoiceOfKarabakh.Application.Services.DonationCategory;
using VoiceOfKarabakh.Application.Services.Email;
using VoiceOfKarabakh.Application.Services.Gallery;
using VoiceOfKarabakh.Application.Services.Localization;
using VoiceOfKarabakh.Application.Services.LocalizationSet;
using VoiceOfKarabakh.Application.Services.PaymentCart;
using VoiceOfKarabakh.Application.Services.Post;
using VoiceOfKarabakh.Application.Services.SaveFile;
using VoiceOfKarabakh.Application.Services.Tag;
using VoiceOfKarabakh.Domain.Factory.Category;
using VoiceOfKarabakh.Domain.Factory.Localization;
using VoiceOfKarabakh.Domain.Factory.LocalizationSet;
using VoiceOfKarabakh.Domain.Factory.Tag;
using VoiceOfKarabakh.Domain.Interfaces.Category;
using VoiceOfKarabakh.Domain.Interfaces.Donation;
using VoiceOfKarabakh.Domain.Interfaces.DonationCategory;
using VoiceOfKarabakh.Domain.Interfaces.Gallery;
using VoiceOfKarabakh.Domain.Interfaces.Localization;
using VoiceOfKarabakh.Domain.Interfaces.LocalizationSet;
using VoiceOfKarabakh.Domain.Interfaces.Post;
using VoiceOfKarabakh.Domain.Interfaces.SaveFile;
using VoiceOfKarabakh.Domain.Interfaces.Tag;
using VoiceOfKarabakh.Infrastructure.Data;
using VoiceOfKarabakh.Infrastructure.Repository;
using VoiceOfKarabakh.Infrastructure.Repository.Category;
using VoiceOfKarabakh.Infrastructure.Repository.Donation;
using VoiceOfKarabakh.Infrastructure.Repository.Gallery;
using VoiceOfKarabakh.Infrastructure.Repository.Localization;
using VoiceOfKarabakh.Infrastructure.Repository.LocalizationSet;
using VoiceOfKarabakh.Infrastructure.Repository.Post;
using VoiceOfKarabakh.Infrastructure.Repository.SaveFile;
using VoiceOfKarabakh.Infrastructure.Repository.Tag;

namespace VoiceOfKarabakh.Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
            serviceCollection.AddScoped<ICategoryService, CategoryService>();
            serviceCollection.AddScoped<CategoryFactory, CategoryFactory>();

            serviceCollection.AddScoped<ITagRepository, TagRepository>();
            serviceCollection.AddScoped<ITagService, TagService>();
            serviceCollection.AddScoped<TagFactory, TagFactory>();

            serviceCollection.AddScoped<ILocalizationRepository, LocalizationRepository>();
            serviceCollection.AddScoped<ILocalizationService, LocalizationService>();
            serviceCollection.AddScoped<LocalizationFactory, LocalizationFactory>();

            serviceCollection.AddScoped<ILocalizationSetRepository, LocalizationSetRepository>();
            serviceCollection.AddScoped<ILocalizationSetService, LocalizationSetService>();
            serviceCollection.AddScoped<LocalizationSetFactory, LocalizationSetFactory>();

            serviceCollection.AddScoped<IPostRepository<Domain.Models.Posts.Post>, PostRepository<Domain.Models.Posts.Post>>();
            serviceCollection.AddScoped<IPostService<Domain.Models.Posts.Post>, PostService<Domain.Models.Posts.Post>>();
            serviceCollection.AddScoped<IPostRepository<Domain.Models.Posts.NewsPost>, PostRepository<Domain.Models.Posts.NewsPost>>();
            serviceCollection.AddScoped<IPostService<Domain.Models.Posts.NewsPost>, PostService<Domain.Models.Posts.NewsPost>>();
            serviceCollection.AddScoped<IPostRepository<Domain.Models.Posts.HistoryPost>, PostRepository<Domain.Models.Posts.HistoryPost>>();
            serviceCollection.AddScoped<IPostService<Domain.Models.Posts.HistoryPost>, PostService<Domain.Models.Posts.HistoryPost>>();
            serviceCollection.AddScoped<IPostRepository<Domain.Models.Posts.MonumentPost>, PostRepository<Domain.Models.Posts.MonumentPost>>();
            serviceCollection.AddScoped<IPostService<Domain.Models.Posts.MonumentPost>, PostService<Domain.Models.Posts.MonumentPost>>();
            serviceCollection.AddScoped<IPostRepository<Domain.Models.Posts.CulturalHeritagePost>, PostRepository<Domain.Models.Posts.CulturalHeritagePost>>();
            serviceCollection.AddScoped<IPostService<Domain.Models.Posts.CulturalHeritagePost>, PostService<Domain.Models.Posts.CulturalHeritagePost>>();
            serviceCollection.AddScoped<IPostRepository<Domain.Models.Posts.CelebrityPost>, PostRepository<Domain.Models.Posts.CelebrityPost>>();
            serviceCollection.AddScoped<IPostService<Domain.Models.Posts.CelebrityPost>, PostService<Domain.Models.Posts.CelebrityPost>>();

            serviceCollection.AddScoped<ISaveFileService, SaveFileService>();
            serviceCollection.AddScoped<ISaveFileRepository, SaveFileRepository>();

            serviceCollection.AddScoped<IGalleryRepository, GalleryRepository>();
            serviceCollection.AddScoped<IGallerySevice, GalleryService>();

            serviceCollection.AddScoped<IDonationCategoryRepository, DonationCategoryRepository>();
            serviceCollection.AddScoped<IDonationCategoryService, DonationCategoryService>();

            serviceCollection.AddScoped<IDonationRepository, DonationRepository>();
            serviceCollection.AddScoped<IDonationService, DonationService>();

            serviceCollection.AddScoped<IPaymentCartService, PaymentCartService>();

            serviceCollection.AddScoped<IEmailSenderService, EmailSenderService>();

            serviceCollection.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString, providerOptions =>
                {
                    providerOptions.CommandTimeout(60);
                });
            });

            serviceCollection
                .AddIdentity<IdentityUser, IdentityRole>(
                    opt =>
                    {
                        opt.SignIn.RequireConfirmedEmail = true;
                    })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<UserManager<IdentityUser>>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders();

            serviceCollection.Configure<FilePathOption>(opt =>
            {
                opt.PhotosFolder = configuration.GetSection("PhotosFolder").Value;
            });

            serviceCollection.Configure<CookieKeyOption>(opt =>
            {
                opt.PaymentCart = configuration.GetSection("PaymentCart").Value;
            });

            serviceCollection.Configure<PaymentOption>(op =>
            {
                op.SecretKey = configuration.GetSection("Stripe:SecretKey").Value;
                op.SuccessPath = configuration.GetSection("Stripe:SuccessPath").Value;
                op.CanclePath = configuration.GetSection("Stripe:CanclePath").Value;
            });

            serviceCollection.Configure<SmtpOption>(opt =>
            {
                opt.Host = configuration.GetSection("Smtp:Host").Value;
                opt.Email = configuration.GetSection("Smtp:Email").Value;
                opt.Port = int.Parse(configuration.GetSection("Smtp:Port").Value);
                opt.Password = configuration.GetSection("Smtp:Password").Value;
            });
        }
    }
}
