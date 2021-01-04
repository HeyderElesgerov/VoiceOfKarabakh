using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace VoiceOfKarabakh.UI.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DonationCategoryController : Controller
    {
        IEnumerable<CultureInfo> cultureInfos;

        public DonationCategoryController(IOptions<RequestLocalizationOptions> options)
        {
            cultureInfos = options.Value.SupportedCultures;
        }

        public IActionResult Index()
        {
            return View(cultureInfos.ToList());
        }
    }
}
