using Dietician.Models;
using Dietician.Storage;
using Dietician.Storage.StorageModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Dietician.Storage.Repositories;

namespace Dietician.Controllers
{
    public class SettingsController : BaseController
    {
        private readonly IUserRepository _repository;

        public SettingsController(IAppConfiguration appConfiguration)
        {
            _repository = new UserRepository(appConfiguration);
        }

        [HttpGet]
        public IActionResult Index()
        {
            UserEntity user = GetLoggedUser(_repository);
            var model = new Settings()
            {
                PersonalData = new PersonalDataSettings {
                    Name=user.Name,
                    Lastname=user.Lastname,
                    Age=user.Age,
                    Weight=user.Weight,
                    Height=user.Height,
                    Gender=user.Gender
                },
                Menu = new MenuSettings(),
                Activity = new ActivitySettings()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(Settings settings)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                UserEntity user = GetLoggedUser(_repository);
                ChangePersonalData(user, settings.PersonalData);
                _repository.UpdateUser(user);
            }
            
            return View(settings);
        }

        public UserEntity ChangePersonalData(UserEntity user, PersonalDataSettings personalData)
        {
            user.Name = personalData.Name;
            user.Lastname = personalData.Lastname;
            user.Age = personalData.Age;
            user.Gender = personalData.Gender;
            user.Height = personalData.Height;
            user.Weight = personalData.Weight;

            return user;
        }
    }
}
