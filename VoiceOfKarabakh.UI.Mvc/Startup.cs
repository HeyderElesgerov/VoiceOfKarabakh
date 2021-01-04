using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using VoiceOfKarabakh.Infrastructure.IoC;

namespace VoiceOfKarabakh.UI.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(op =>
            {
                op.IdleTimeout = TimeSpan.FromMinutes(30);
                op.Cookie.Name = "payment_cart";
                op.Cookie.HttpOnly = true;
                op.Cookie.IsEssential = true;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllersWithViews()
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.AddRazorPages();

            services.RegisterServices(Configuration);

            services.AddLocalization(opt =>
            {
                opt.ResourcesPath = "Resources";
            });

            services.Configure<RequestLocalizationOptions>(op =>
             {
                 var supportedCultures = new CultureInfo[]
                 {
                     new CultureInfo("en"),
                     new CultureInfo("az"),
                     new CultureInfo("tr"),
                     new CultureInfo("ru"),
                     new CultureInfo("fa"),
                 };

                 op.DefaultRequestCulture =
                 new Microsoft.AspNetCore.Localization.RequestCulture("az");

                 op.SupportedCultures = supportedCultures;
                 op.SupportedUICultures = supportedCultures;
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRequestLocalization(
                app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
