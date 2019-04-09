using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dietician.Models;

namespace Dietician.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            DietInfo dietinfo = new DietInfo
            {
                DietName = "Wegetariańska",
                Info = "Najogólniej mówiąc, dieta wegetariańska to taka, która zakłada niejedzenia mięsa."
            };
            return View(dietinfo);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
