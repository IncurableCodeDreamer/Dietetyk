using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Models;
using Dietician.Services;
using Dietician.Storage;
using Dietician.Storage.Interfaces;
using Dietician.Storage.Repositories;
using Dietician.Storage.StorageModels;
using Microsoft.AspNetCore.Mvc;


namespace Dietician.Controllers
{
    public class StatisticsController : BaseController
    {
        private readonly IRepositoryWrapper _repository;

        public StatisticsController(IAppConfiguration appConfiguration)
        {
            _repository = new RepositoryWrapper(appConfiguration);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetDatasets(string date)
        {
            string[] yearMonth = date.Split('-');
            var year = int.Parse(yearMonth[0]);
            var month = int.Parse(yearMonth[1]);
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month),23,59,59);

            UserEntity user = GetLoggedUser(_repository.User);
            var indicatorModels = GetIndicatorsData(startDate, endDate, user);

            var weightData = GetWeightData(indicatorModels);
            var heightData = GetHeightData(indicatorModels);
            var bmiData = GetBmiData(indicatorModels);

            return Json (new
            {
                success =true,
                weightDataset =weightData,
                heightDataset = heightData,
                bmiDataset = bmiData
            });
        }

        private List<IndicatorModel> GetIndicatorsData(DateTime startDate, DateTime endDate, UserEntity user)
        {
            var indicators = _repository.Indicator.GetIndicatorsFromTable(user.Id).Result.ToList();
            var indicatorsData = indicators.Select(x => x.IndicatorsModelData)
                 .Where(x => (x.ChangeDate > startDate && x.ChangeDate < endDate))
                 .ToList();

            IndicatorModel lastData = new IndicatorModel() { Weight = 0, Height = 0 };

            for(int i = 1; i <= DateTime.DaysInMonth(endDate.Year, endDate.Month); i++)
            {
                //if ()
                //{
                //TODO interpolate indicators
                //}
            }

            return indicatorsData;
        }

        private List<string> GetDateNames()
        {
            //TODO get x label names
            return null;
        }

        private List<double> GetWeightData(List<IndicatorModel> indicatorModels)
        {
            List<double> weightData = indicatorModels.Select(x => (double)x.Weight).ToList();
            return weightData;
        }

        private List<double> GetHeightData(List<IndicatorModel> indicatorModels)
        {
            List<double> heightData = indicatorModels.Select(x => (double)x.Height).ToList();
            return heightData;
        }

        private List<double> GetBmiData(List<IndicatorModel> indicatorModels)
        {
            List<double> bmiData = new List<double>();

            foreach (var indicator in indicatorModels)
            {
                PersonalDataSettings ps = new PersonalDataSettings()
                {
                    Weight = indicator.Weight,
                    Height = indicator.Height
                };

                bmiData.Add(ParameterService.CalculateBMI(ps));
            }
            return null;
        }

    }
}
