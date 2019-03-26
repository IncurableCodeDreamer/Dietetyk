using Dietician.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dietician.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            var model = new Settings()
            {
                PersonalData = new PersonalDataSettings(),
                Menu = new MenuSettings(),
                Activity = new ActivitySettings()
            };
            return View(model);
        }
    }
}
