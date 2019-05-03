using Dietician.Models;
using Dietician.Storage;
using Dietician.Storage.StorageModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Dietician.Storage.Repositories;
using Dietician.Storage.Interfaces;

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

            var model = new Settings()
            {
                PersonalData = new PersonalDataSettings {
                    Name = user.Name,
                    Lastname = user.Lastname,
                    Age = user.Age,
                    Gender = user.Gender
                },
                Menu = new MenuSettings {
                    Preferences = Enums.DietPreferences.Brak
                },
                Activity = new ActivitySettings {
                    LifeStyle = Enums.Lifestyle.Aktywny
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
                _repository.User.UpdateUser(user);
            }
            
            return View(settings);
        }

        public UserEntity ChangePersonalData(UserEntity user, PersonalDataSettings personalData)
        {
            user.Name = personalData.Name;
            user.Lastname = personalData.Lastname;
            user.Age = personalData.Age;
            user.Gender = personalData.Gender;

            return user;
        }
    }
}
