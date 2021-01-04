using Microsoft.AspNetCore.Mvc;

namespace VoiceOfKarabakh.UI.Mvc.Controllers.Help
{
    public class HelpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ToHelp()
        {
            return View();
        }

        public IActionResult ToGetHelp()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
