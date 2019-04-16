using System.Threading.Tasks;
using Dietician.Models;
using Dietician.Storage;
using Dietician.Storage.Identity;
using Dietician.Storage.StorageModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
        
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                var cookies = Request.Cookies.Keys;
                foreach (var cookie in cookies)
                {
                    Response.Cookies.Delete(cookie, new Microsoft.AspNetCore.Http.CookieOptions()
                    {
                        Domain = Request.Host.Host
                    });
                }
                //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                //var scheme = User.FindFirst("ADMIN2").Value;
                //await HttpContext.SignOutAsync(User.Identity.Name);
                //await _signInManager.SignOutAsync();
            }
            var s = HttpContext.User.Identity.Name;
            return RedirectToAction("Index", "Home");
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
                bool userExist = await _userRepository.CheckIfUserExist(loginModel.Login.ToUpper());

                if (!userExist)
                {
                    ModelState.AddModelError("Login", "Użytkownik o podanym loginie nie istnieje");
                    return View(loginModel);
                }

                var result = await _signInManager.PasswordSignInAsync(loginModel.Login, loginModel.Password, true , false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "Błędne hasło");
                    return View(loginModel);
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
            bool userExist = await _userRepository.CheckIfUserExist(registration.Login.ToUpper());

            if (!ModelState.IsValid)
            {
                return View(registration);
                
            }

            if (userExist)
            {
                ModelState.AddModelError("Login", "Użytkownik o podanym loginie już istnieje");
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
