using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;

namespace VoiceOfKarabakh.UI.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TagController : Controller
    {
        IEnumerable<CultureInfo> _cultures;

        public TagController(IOptions<RequestLocalizationOptions> options)
        {
            _cultures = options.Value.SupportedCultures;
        }

        public IActionResult Index()
        {
            return View(_cultures);
        }
    }
}
