using System;
using System.Collections.Generic;
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

namespace Dietician.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IRepositoryWrapper _repository;

        public MenuController(IAppConfiguration appConfiguration)
        {
            _repository = new RepositoryWrapper(appConfiguration);
        }

        public FileResult ExportToPdf(List<Meal> meals)
        {
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
            List<FoodModel> dailyMeals = GetDailyMealsForUserAsync(user, 1).Result;
        
            return View(dailyMeals);
        }
        
        public ActionResult AddMeal(AddMeal meal)
        {           
            if (ModelState.IsValid)
            {
                UserEntity user = GetLoggedUser(_repository.User);
                //MealModel m = new MealModel(user.Id, 1, DateTime.Now, meal.Type, meal.Portions);
                //_repository.Meal.InsertMealIntoTable(m); //TODO addmeal to db
            }
            return PartialView("_AddMealModal", meal);
        }

        public ActionResult ChangeMenu(ChangeMenu menu)
        {
            if (ModelState.IsValid)
            {
                //TODO change menu
            }
            return PartialView("_ChangeMenuModal", menu);
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
            model.MealTypes = GetMealsType(user);
            return PartialView("_ChangeMenuModal",model);
        }

        private List<SelectListItem> GetMealsType(UserEntity user)
        {
            List<SelectListItem> mealsType = new List<SelectListItem>();
            //TODO implement - wybrac mealtypes ktore chcemy uwzglednic
            mealsType.Add(new SelectListItem(MealType.Sniadanie.ToString(),MealType.Sniadanie.ToString()));
            mealsType.Add(new SelectListItem(MealType.Sniadanie2.ToString(), MealType.Sniadanie2.ToString()));
            mealsType.Add(new SelectListItem(MealType.Obiad.ToString(), MealType.Obiad.ToString()));
            mealsType.Add(new SelectListItem(MealType.Podwieczorek.ToString(), MealType.Podwieczorek.ToString()));
            mealsType.Add(new SelectListItem(MealType.Kolacja.ToString(), MealType.Kolacja.ToString()));
            
            return mealsType;
        }

        private async Task<List<FoodModel>> GetDailyMealsForUserAsync(UserEntity user, int variant)
        {
            List<FoodModel> dailyMeals = new List<FoodModel>();
            var userMeals = await _repository.Meal.GetIMealFromTable(user.Id);
            foreach (var item in userMeals)
            {
                var id = item.JsonId;
                var meal = await _repository.Food.GetOneFood(id);
                dailyMeals.Add(meal);
            }

            return dailyMeals;
        }
    }
}
