using Dietician.Models;
using Dietician.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dietician.Controllers
{
    public class ParametersCalculatorController : BaseController
    {
        private ParameterService parameterService;

        public IActionResult Index()
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

        [HttpPost]
        public IActionResult Index(Parameters Parameters)
        {
            Parameters.ParameterResults.Bmi = parameterService.CalculateBMI(Parameters.PresonalData);
            Parameters.ParameterResults.BmiLabel = parameterService.GetBMILabel(Parameters.ParameterResults);
            //model.ParameterResults.Whr = parameterService.onItemSelected(id);
            Parameters.ParameterResults.FatLevel = parameterService.CalculateBF(Parameters);
            Parameters.ParameterResults.Whr = parameterService.CalculateWHR(Parameters);
            return View(Parameters);
        }
    }
}
