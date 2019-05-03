using Dietician.Models;
using Dietician.Storage;
using Dietician.Storage.StorageModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Dietician.Storage.Repositories;
using Dietician.Storage.Interfaces;
using System;

namespace Dietician.Controllers
{
    public class SettingsController : BaseController
    {
        private readonly IRepositoryWrapper _repository;

        public SettingsController(IAppConfiguration appConfiguration)
        {
            _repository = new RepositoryWrapper(appConfiguration);
        }

        [HttpGet]
        public IActionResult Index()
        {
            UserEntity user = GetLoggedUser(_repository.User);
            MealSettingsModel mealSettings = GetOrCreateUserMealSettings(user);
            var indicators = _repository.Indicator.GetLastIndicatorFromTable(user.Id).Result;

            var model = new Settings()
            {
                PersonalData = new PersonalDataSettings {
                    Name = user.Name,
                    Lastname = user.Lastname,
                    Height = indicators!=null? indicators.IndicatorsModelData.Height: null,
                    Weight = indicators != null ? indicators.IndicatorsModelData.Weight : null,
                    Age = user.Age,
                    Gender = user.Gender
                },
                Menu = new MenuSettings {
                    Preferences = mealSettings.Preferences,
                    DietAim = mealSettings.DietAim
                },
                Activity = new ActivitySettings {
                    LifeStyle = user.LifeStyle
                }
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(Settings settings)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                UserEntity user = GetLoggedUser(_repository.User);
                ChangePersonalData(user, settings.PersonalData);
                ChangeMenuSettings(user, settings.Menu);
                AddIndicatorsToTable(user, settings.PersonalData);
            }
            
            return View(settings);
        }

        private MealSettingsModel GetOrCreateUserMealSettings(UserEntity user)
        {
            MealSettingsModel mealSettings;

            if (user.IdMealSetting == null)
            {
                mealSettings = _repository.MealSetting.InsertMealSettingsIntoTable(new MealSettingsModel()).Result;
                user.IdMealSetting = mealSettings.IdMealSettings;
                _repository.User.UpdateUser(user);
            }
            else
            {
                mealSettings = _repository.MealSetting.GetMealSettingFromTable(user.IdMealSetting).Result.MealSettingsModelData;
            }
            return mealSettings;
        }

        private void AddIndicatorsToTable(UserEntity user, PersonalDataSettings personalData)
        {
            IndicatorModel idicatorsModel = new IndicatorModel {
                IdUser = user.Id,
                Weight = personalData.Weight,
                Height = personalData.Height,
                ChangeDate = DateTime.Now
            };
            _repository.Indicator.InsertIndicatorsIntoTable(idicatorsModel);
        }

        private void ChangePersonalData(UserEntity user, PersonalDataSettings personalData)
        {
            user.Name = personalData.Name;
            user.Lastname = personalData.Lastname;
            user.Age = personalData.Age;
            user.Gender = personalData.Gender;
            _repository.User.UpdateUser(user);
        }

        private void ChangeMenuSettings(UserEntity user, MenuSettings menuSettings)
        {
            MealSettingsEntity mealSettings = _repository.MealSetting.GetMealSettingFromTable(user.IdMealSetting).Result;
            if (mealSettings.MealSettingsModelData != null)
            {
                mealSettings.MealSettingsModelData.DietAim = menuSettings.DietAim;
                mealSettings.MealSettingsModelData.Preferences = menuSettings.Preferences;
                _repository.MealSetting.UpdateMealSettings(mealSettings);
            }
        }
    }
}
