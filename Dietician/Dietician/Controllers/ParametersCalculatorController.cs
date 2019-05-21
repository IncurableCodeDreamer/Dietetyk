using Dietician.Models;
using Dietician.Services;
using Dietician.Storage;
using Dietician.Storage.Interfaces;
using Dietician.Storage.Repositories;
using Dietician.Storage.StorageModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Dietician.Controllers
{
    public class ParametersCalculatorController : BaseController
    {
        private readonly IRepositoryWrapper _repository;

        public ParametersCalculatorController(IAppConfiguration appConfiguration)
        {
            _repository = new RepositoryWrapper(appConfiguration);
        }

        public IActionResult Index()
        {
            UserEntity user = GetLoggedUser(_repository.User);
            var indicators = _repository.Indicator.GetIndicatorsFromTable(user.Id).Result.ToList();
            var model = new Parameters()
            {
                PresonalData = new PersonalDataSettings()
                {
                    Age = user.Age,
                    Gender = user.Gender,
                    Weight = indicators[0].IndicatorsModelData.Weight,
                    Height = indicators[0].IndicatorsModelData.Height,
                     Lifestyle = user.LifeStyle
                },
                Params = new CheckboxResult(),
                FatLevel = new FatLevel(),
                ShowResults = false,
                ParameterResults = new ParameterResults()
            };

            model = CalculateParams(model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(Parameters Parameters)
        {
            if (ModelState.IsValid)
            {
                CalculateParams(Parameters);
                return View(Parameters);
            }
            else
            {
                return View(Parameters);
            }
        }

        private Parameters CalculateParams(Parameters Parameters)
        {
            ParameterResults results = new ParameterResults()
            {
                Bmi = ParameterService.CalculateBMI(Parameters.PresonalData),
                FatLevel = ParameterService.CalculateBF(Parameters),
            };

            results.Cmp = ParametersCalc.CountCPM(Parameters.PresonalData);
            results.Carbohydrate = ParametersCalc.CarbohydratesMinCalculate(results.Cmp);
            results.Protein = ParametersCalc.ProteinsMinCalculate(results.Cmp);
            results.Fat = ParametersCalc.FatsMinCalculate(results.Cmp);
            results.Whr = ParameterService.CalculateWHR(Parameters, results);
            results.BmiLabel = ParameterService.GetBMILabel(results);

            Parameters.ParameterResults = results;
            Parameters.ShowResults = true;
            return Parameters;
        }
    }
}
