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
        private readonly IUserRepository _userRepository;

        public AccountController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IAppConfiguration appConfiguration)
        {
             _userManager = userManager;
             _signInManager = signInManager;
            _userRepository = new UserRepository(appConfiguration);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.Login, loginModel.Password, true , false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Registration registration)
        {
            bool userExist = await _userRepository.CheckIfUserExist(registration.Login);

            if (!ModelState.IsValid)
            {
                return View(registration);
                
            }

            if (userExist)
            {
                ModelState.AddModelError("userExist", "Użytkownik o podanym loginie już istnieje");
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
