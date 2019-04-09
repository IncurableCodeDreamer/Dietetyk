using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Models;
using Dietician.Storage;
using Dietician.Storage.Identity;
using Dietician.Storage.StorageModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dietician.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
             _userManager = userManager;
             _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            /* var model = new Registration()
             {
                 PersonalData = new PersonalDataSettings(),
                 Menu = new MenuSettings(),
                 Activity = new ActivitySettings()
             };*/
            return RedirectToAction("Register");
        }

        public IActionResult Register()
        {
           /* var model = new Registration()
            {
                PersonalData = new PersonalDataSettings(),
                Menu = new MenuSettings(),
                Activity = new ActivitySettings()
            };*/
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return View(registration);
                
            }
            var user = new UserEntity { UserName = registration.Login };
            var result = await _userManager.CreateAsync(user, registration.Password);

            if ((result as Result).Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registration);
        }
    }
}
