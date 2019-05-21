using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.CosmosDB;
using Dietician.Enums;
using Dietician.Helpers;
using Dietician.Models;
using Dietician.Storage;
using Dietician.Storage.Interfaces;
using Dietician.Storage.Repositories;
using Dietician.Storage.StorageModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Dietician.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IRepositoryWrapper _repository;

        public MenuController(IAppConfiguration appConfiguration)
        {
            _repository = new RepositoryWrapper(appConfiguration);
        }
               
        public FileResult ExportToPdf(List<FoodWithDayModel> meals)
        {
            List<FoodWithDayModel> mealsList2 = meals
                .OrderBy(x => x.Type)
                .ThenBy(x => x.Day)
                .ToList();

            List<Meal> mealsList = new List<Meal>();
            Array values = Enum.GetValues(typeof(MealType));
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                mealsList.Add(new Meal()
                {
                    Date = DateTime.Now.AddDays(i),
                    CosmosMeal = new CosmosMealModel()
                    {
                        Calories = 24,
                        Carbohydrates = 10,
                        Fat = 10,
                        Guid = "guid",
                        Ingredients = "mleko, jaja, costam",
                        Kind = "typ",
                        Name = "owsianka",
                        Portions = "porcje",
                        Prepare = "nalej mleka do miski, dodaj płatki, wymieszaj w 30 stopniach przez 20 minut i wsio",
                        Proteins = 20,
                        Type = (MealType)values.GetValue(random.Next(values.Length)),
                    },
                    MealType = (MealType)values.GetValue(random.Next(values.Length)),
                    JsonId = 1
                });
            }

            var pdfByteArray = PdfHelper.WritePdf(mealsList);
            return File(pdfByteArray, System.Net.Mime.MediaTypeNames.Application.Octet, "Jadłospis-" + DateTime.Now.ToShortDateString() + ".pdf");                  
        }

        public IActionResult Index()
        {
            UserEntity user = GetLoggedUser(_repository.User);
            //TO DO add variant, day przy zmianie
            List<FoodWithDayModel> dailyMeals = GetDailyMealsForUserAsync(user, 1).Result;
  
            return View(dailyMeals);
        }
        
        public ActionResult AddMeal(AddMeal meal)
        {           
            if (ModelState.IsValid)
            {
                UserEntity user = GetLoggedUser(_repository.User);
                FoodModel mealToAdd = new FoodModel
                {
                    Guid = Guid.NewGuid().ToString(),
                    Name = meal.Name,
                    Ingredients = meal.Ingredients,
                    Portions = meal.Portions.ToString(),
                    Prepare = meal.Prepare,
                    Url = meal.Url,
                    Calories = meal.Calories,
                    Proteins = meal.Proteins,
                    Carbohydrates = meal.Carbohydrates,
                    Fat = meal.Fat,
                    Type = meal.Type,
                    Kind = meal.Kind.ToString(),
                    ImageUrl = meal.ImageUrl,
                };
               _repository.Food.InsertFoodIntoTable(mealToAdd);
                return Json(new { success = true });
            }
            return PartialView("_AddMealModal", meal);
        }

        public ActionResult ChangeMenu(ChangeMenu menu)
        {
            if (ModelState.IsValid)
            {
                //TODO change wariant
                int wariant = 1;

                if (menu.Monday)
                {
                   ChangeMenuForADay(wariant, System.DayOfWeek.Monday);
                }
                if (menu.Tuesday)
                {
                    ChangeMenuForADay(wariant, System.DayOfWeek.Tuesday);
                }
                if (menu.Thursday)
                {
                    ChangeMenuForADay(wariant, System.DayOfWeek.Thursday);
                }
                if (menu.Wednesday)
                {
                    ChangeMenuForADay(wariant, System.DayOfWeek.Wednesday);
                }
                if (menu.Friday)
                {
                    ChangeMenuForADay(wariant, System.DayOfWeek.Friday);
                }
                if (menu.Saturday)
                {
                    ChangeMenuForADay(wariant, System.DayOfWeek.Saturday);
                }
                if (menu.Sunday)
                {
                    ChangeMenuForADay(wariant, System.DayOfWeek.Sunday);
                }

                return Json(new { success = true });
            }
            return PartialView("_ChangeMenuModal", menu);
        }

        private async void ChangeMenuForADay(int wariant, System.DayOfWeek day)
        {
            UserEntity user = GetLoggedUser(_repository.User);
            int numberOfDay = (int)day;

            var dailyMeals =  _repository.Meal.GetMealToOneDayFromTableAsync(user.Id, numberOfDay.ToString(), wariant.ToString()).Result;
            
            SetMealsForUser setMeals = new SetMealsForUser(_repository);
            
            var result = _repository.Meal.RemoveMealToOneDayFromTable(user.Id, numberOfDay.ToString(), wariant.ToString());
           
            var indicators = _repository.Indicator.GetLastIndicatorFromTable(user.Id).Result;
            var cpm = ParametersCalc.CountCPM(new PersonalDataSettings {
                Age=user.Age,
                Gender=user.Gender,
                Height=indicators.IndicatorsModelData.Height,
                Weight=indicators.IndicatorsModelData.Weight
            });

            await setMeals.PlanDiet(user, cpm, numberOfDay, wariant);
            
        }

        public async Task<IActionResult> GenerateMealsAsync()
        {
            UserEntity user = GetLoggedUser(_repository.User);
            SetMealsForUser setMeals = new SetMealsForUser(_repository);
            for (int i = 1; i <= 7; i++)
            {
                await setMeals.PlanDiet(user, 2000, i, 1);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult GetAddMealView()
        {
            AddMeal model = new AddMeal();
            return PartialView("_AddMealModal", model);
        }

        [HttpGet]
        public PartialViewResult GetChangeMenuView()
        {
            ChangeMenu model = new ChangeMenu();
            UserEntity user = GetLoggedUser(_repository.User);
            return PartialView("_ChangeMenuModal",model);
        }
        
        private async Task<List<FoodWithDayModel>> GetDailyMealsForUserAsync(UserEntity user, int variant)
        {
            List<FoodWithDayModel> dailyMeals = new List<FoodWithDayModel>();
            var userMeals = await _repository.Meal.GetIMealFromTable(user.Id);
            foreach (var item in userMeals)
            {
                var day = item.MealNumber;
                var id = item.JsonId;
                var meal = await _repository.Food.GetOneFoodWithDay(id, day);
                dailyMeals.Add(meal);
            }

            return dailyMeals;
        }
    }
}
