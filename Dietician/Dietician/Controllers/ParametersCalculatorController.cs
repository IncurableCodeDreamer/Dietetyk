using Dietician.Models;
using Dietician.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dietician.Controllers
{
    public class ParametersCalculatorController : BaseController
    {
        private ParameterService parameterService;

        public IActionResult Index(IFormCollection collection)
        {
            var model = new Parameters()
            {
                PresonalData = new PersonalDataSettings(),
                Params = new CheckboxResult(),
                FatLevel = new FatLevel(),
                ShowResults = false,
                ParameterResults = new ParameterResults()
            };

            return View(model);
        }

        public IActionResult CalculateParameters(Parameters model)
        {
            model.ParameterResults.Bmi = parameterService.CalculateBMI(model.PresonalData);
            model.ParameterResults.BmiLabel = parameterService.GetBMILabel(model.ParameterResults);
            //model.ParameterResults.Whr = parameterService.onItemSelected(id);
            model.ParameterResults.FatLevel = parameterService.CalculateBF(model);
            model.ParameterResults.Whr = parameterService.CalculateWHR(model);
            return View(model);
        }
    }
}
