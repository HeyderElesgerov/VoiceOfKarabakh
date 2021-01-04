using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;

namespace VoiceOfKarabakh.UI.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        IEnumerable<CultureInfo> cultures;

        public CategoryController(IOptions<RequestLocalizationOptions> options)
        {
            cultures = options.Value.SupportedCultures;
        }

        public IActionResult Index()
        {
            return View(cultures);
        }
    }
}
