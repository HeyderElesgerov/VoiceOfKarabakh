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
    public class DonationController : Controller
    {
        List<CultureInfo> cultureInfos;

        public DonationController(IOptions<RequestLocalizationOptions> options)
        {
            cultureInfos = options.Value.SupportedCultures.ToList();
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(CultureInfo.CurrentCulture);
        }

        public IActionResult AdminIndex()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View(cultureInfos);
        }

        public IActionResult Edit(int id)
        {
            ViewData["id"] = id;
            return View(cultureInfos);
        }
    }
}
