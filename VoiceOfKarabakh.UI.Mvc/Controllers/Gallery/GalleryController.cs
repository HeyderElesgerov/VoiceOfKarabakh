using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VoiceOfKarabakh.Application.ViewModels.Gallery;

namespace VoiceOfKarabakh.UI.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GalleryController : Controller
    {
        List<CultureInfo> cultures;

        public GalleryController(IOptions<RequestLocalizationOptions> cultureOptions)
        {
            cultures = cultureOptions.Value.SupportedCultures.ToList();
        }

        [AllowAnonymous]
        public IActionResult Index(int page = 1, int perPage = 10)
        {
            ViewData["Page"] = page;
            ViewData["PerPage"] = perPage;
            return View();
        }

        public IActionResult AdminIndex()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View(cultures);
        }

        public IActionResult Edit(int id)
        {
            ViewData["id"] = id;

            return View(cultures);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Read(int id)
        {
            string cultureCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            using (HttpClient httpClient = new HttpClient())
            {
                string path = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value;
                var response = await httpClient.GetAsync($"{path}/api/galleries/GetReadGalleryViewModel/{id}/{cultureCode}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var gallery = JsonSerializer.Deserialize<ReadGalleryViewModel>(result, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return View(gallery);
                }
            }

            return NotFound();
        }
    }
}
