using Dietician.Models;
using Dietician.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Dietician.Controllers
{
    public class ParametersCalculatorController : Controller
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

        public IActionResult CalculateParameters(Parameters parameters)
        {
            parameters.ParameterResults.Bmi = parameterService.CalculateBMI(parameters.PresonalData);
            parameters.ParameterResults.BmiLabel = parameterService.GetBMILabel(parameters.ParameterResults);
            //parameters.ParameterResults.Whr = parameterService.onItemSelected(id);
            parameters.ParameterResults.FatLevel = parameterService.CalculateBF(parameters);
            parameters.ParameterResults.Whr = parameterService.CalculateWHR(parameters);
            return View(parameters);
        }
    }
}
