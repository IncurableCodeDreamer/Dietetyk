using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
