using Dietician.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dietician.Controllers
{
    public class ParametersCalculatorController : Controller
    {
        public IActionResult Index()
        {
            var model = new Parameters()
            {
                PresonalData = new PersonalDataSettings(),
                Params = new CheckboxResult(),
                FatLevel = new FatLevel()
            };
            return View(model);
        }
    }
}
