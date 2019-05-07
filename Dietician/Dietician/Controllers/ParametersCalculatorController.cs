using Dietician.Models;
using Dietician.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dietician.Controllers
{
    public class ParametersCalculatorController : BaseController
    {     

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
            ParameterResults results = new ParameterResults()
            {
                Bmi = ParameterService.CalculateBMI(Parameters.PresonalData),                
                FatLevel = ParameterService.CalculateBF(Parameters),                 
            };

            results.Cmp = ParametersCalc.CountCPM(Parameters.PresonalData);
            results.Carbohydrate = ParametersCalc.CarbohydratesMinCalculate(results.Cmp);
            results.Protein = ParametersCalc.ProteinsMinCalculate(results.Cmp);
            results.Protein = ParametersCalc.FatsMinCalculate(results.Cmp);
            results.Whr = ParameterService.CalculateWHR(Parameters, results);
            results.BmiLabel = ParameterService.GetBMILabel(results);

            Parameters.ParameterResults = results;
            Parameters.ShowResults = true;
            return View(Parameters);
        }
    }
}
