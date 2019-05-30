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
            DateTime now = DateTime.Now;
            DateTime startDate = new DateTime(year, month, 1);
            DateTime endDate = new DateTime(year, month, now.Month == month ? now.Day : DateTime.DaysInMonth(year, month), 23,59,59);

            UserEntity user = GetLoggedUser(_repository.User);
            var indicatorModels = GetIndicatorsData(startDate, endDate, user);

            if (indicatorModels==null|| indicatorModels.Count==0)
            {
                return Json(new { success = false });
            }

            var weightData = GetWeightData(indicatorModels);
            var heightData = GetHeightData(indicatorModels);
            var bmiData = GetBmiData(indicatorModels);
            var datesData = GetDateNames(indicatorModels);

            return Json (new
            {
                success = true,
                weightDataset = weightData.ToArray(),
                heightDataset = heightData.ToArray(),
                bmiDataset = bmiData.ToArray(),
                datesDataset=datesData.ToArray()
            });
        }

        private List<IndicatorModel> GetIndicatorsData(DateTime startDate, DateTime endDate, UserEntity user)
        {
            List<IndicatorModel> allDataFromMonth = new List<IndicatorModel>();
            var indicators = _repository.Indicator.GetIndicatorsFromTable(user.Id).Result.ToList();
            var indicatorsData = indicators.Select(x => x.IndicatorsModelData)
                 .Where(x => (x.ChangeDate >= startDate && x.ChangeDate <= endDate))
                 .OrderBy(x => x.ChangeDate)
                 .ToList();

            if (indicatorsData.Count == 0 || indicatorsData == null)
            {
                return indicatorsData;
            }

            var lastData = indicators.Select(x => x.IndicatorsModelData)
                 .Where(x => (x.ChangeDate < startDate))
                 .OrderByDescending(x => x.ChangeDate).ToList();

            var lastIndicator = lastData.Count > 0 ? lastData.First() : new IndicatorModel() {
                Weight=0,
                Height=0
            };


            for (int i = 1; i <= endDate.Day; i++)
            {
                var dataFromDay = indicators.FirstOrDefault(x => x.IndicatorsModelData.ChangeDate.Day == i);
                if (dataFromDay!=null)
                {
                    allDataFromMonth.Add(dataFromDay.IndicatorsModelData);
                    lastIndicator = new IndicatorModel()
                    {
                        Weight = dataFromDay.IndicatorsModelData.Weight,
                        Height=dataFromDay.IndicatorsModelData.Height
                    };
                }
                else
                {
                    var changeDate = new DateTime(startDate.Year, startDate.Month, i, 0, 0, 0);
                    lastIndicator= new IndicatorModel()
                    {
                        ChangeDate=changeDate,
                        Weight = lastIndicator.Weight,
                        Height = lastIndicator.Height
                    };

                    allDataFromMonth.Add(lastIndicator);
                }
            }

            return allDataFromMonth;
        }

        private List<string> GetDateNames(List<IndicatorModel> indicatorModels)
        {
            List<string> datesName = indicatorModels.Select(x => String.Format("{0:dd.MM}", x.ChangeDate)).ToList();
            return datesName;
        }

        private List<double> GetWeightData(List<IndicatorModel> indicatorModels)
        {
            List<double> weightData = indicatorModels.Select(x => x.Weight!=null? (double)x.Weight:0).ToList();
            return weightData;
        }

        private List<double> GetHeightData(List<IndicatorModel> indicatorModels)
        {
            List<double> heightData = indicatorModels.Select(x => x.Height != null ? (double)x.Height : 0).ToList();
            return heightData;
        }

        private List<double> GetBmiData(List<IndicatorModel> indicatorModels)
        {
            List<double> bmiData = new List<double>();

            foreach (var indicator in indicatorModels)
            {
                PersonalDataSettings ps = new PersonalDataSettings()
                {
                    Weight = indicator.Weight!=null? indicator.Weight:0,
                    Height = indicator.Height != null ? indicator.Height : 0
                };
                bmiData.Add(ParameterService.CalculateBMI(ps));
            }
            return bmiData;
        }

    }
}
