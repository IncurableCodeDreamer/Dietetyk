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
using System.Linq;

namespace Dietician.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IRepositoryWrapper _repository;
        const string defaultVariantName = "default";
        const int defaultVariantId = 1;

        public MenuController(IAppConfiguration appConfiguration)
        {
            _repository = new RepositoryWrapper(appConfiguration);
        }
               
        public async Task<FileResult> ExportToPdf()
        {
            UserEntity user = GetLoggedUser(_repository.User);
            string variantName = user.MenuWariantName;

            List<FoodWithDayModel> mealsList = GetDailyMealsForUserAsync(user, variantName).Result;    
            var pdfByteArray = PdfHelper.WritePdf(mealsList);
            return File(pdfByteArray, System.Net.Mime.MediaTypeNames.Application.Octet, "Jadłospis-" + DateTime.Now.ToShortDateString() + ".pdf");                  
        }

        public async Task<IActionResult> Index()
        {
            UserEntity user = GetLoggedUser(_repository.User);
            string variantName = user.MenuWariantName;
            List<FoodWithDayModel> dailyMeals = new List<FoodWithDayModel>();
            if (variantName != null)
            {
                dailyMeals = GetDailyMealsForUserAsync(user, variantName).Result;
            }
  
            return View(dailyMeals);
        }
        
        public ActionResult AddMeal(AddMeal meal)
        {           
            if (ModelState.IsValid)
            {
                string defaultUrl = "https://firebasestorage.googleapis.com/v0/b/hallowed-ray-197220.appspot.com/o/images%2F800px-Good_Food_In_Dishes_-_NCI_Visuals_Online.jpg?alt=media&token=48fa6ca1-7973-4bff-9fde-f480ab760ad57";
                UserEntity user = GetLoggedUser(_repository.User);
                FoodModel mealToAdd = new FoodModel
                {
                    Guid = Guid.NewGuid().ToString(),
                    Name = meal.Name,
                    Ingredients = meal.Ingredients,
                    Portions = meal.Portions.ToString(),
                    Prepare = meal.Prepare,
                    Url = defaultUrl,
                    Calories = meal.Calories,
                    Proteins = meal.Proteins,
                    Carbohydrates = meal.Carbohydrates,
                    Fat = meal.Fat,
                    Type = meal.Type,
                    Kind = meal.Kind.ToString(),
                    ImageUrl = null,
                };
               _repository.Food.InsertFoodIntoTable(mealToAdd);
                return Json(new { success = true });
            }
            return PartialView("_AddMealModal", meal);
        }

        public async Task<ActionResult> ChangeMenu(ChangeMenu menu)
        {
            if (ModelState.IsValid)
            {
                UserEntity user = GetLoggedUser(_repository.User);
                string variantName = user.MenuWariantName;
                var variantId = await _repository.Meal.GetIdOfMEalIfExist(variantName, user.Id);
                var wariant = int.Parse(variantId);

                if (menu.Monday)
                {
                   await ChangeMenuForADay(wariant, System.DayOfWeek.Monday);
                }
                if (menu.Tuesday)
                {
                    await ChangeMenuForADay(wariant, System.DayOfWeek.Tuesday);
                }
                if (menu.Thursday)
                {
                    await ChangeMenuForADay(wariant, System.DayOfWeek.Thursday);
                }
                if (menu.Wednesday)
                {
                    await ChangeMenuForADay(wariant, System.DayOfWeek.Wednesday);
                }
                if (menu.Friday)
                {
                    await ChangeMenuForADay(wariant, System.DayOfWeek.Friday);
                }
                if (menu.Saturday)
                {
                    await ChangeMenuForADay(wariant, System.DayOfWeek.Saturday);
                }
                if (menu.Sunday)
                {
                    await ChangeMenuForADay(wariant, System.DayOfWeek.Sunday);
                }

                return Json(new { success = true });
            }

            return PartialView("_ChangeMenuModal", menu);
        }

        private async Task ChangeMenuForADay(int wariant, System.DayOfWeek day)
        {
            UserEntity user = GetLoggedUser(_repository.User);
            int numberOfDay = (int)day;

            var dailyMeals =  _repository.Meal.GetMealToOneDayFromTableAsync(user.Id, numberOfDay.ToString(), wariant.ToString()).Result;
            
            SetMealsForUser setMeals = new SetMealsForUser(_repository);

            await RemoveMealsFromDay(user,numberOfDay,wariant);
            
            var indicators = _repository.Indicator.GetLastIndicatorFromTable(user.Id).Result;
            var cpm = ParametersCalc.CountCPM(new PersonalDataSettings {
                Age=user.Age,
                Gender=user.Gender,
                Height=indicators.IndicatorsModelData.Height,
                Weight=indicators.IndicatorsModelData.Weight
            });

            await setMeals.PlanDiet(user, cpm, numberOfDay, wariant);
            
        }

        private async Task RemoveMealsFromDay(UserEntity user, int numberOfDay, int wariant)
        {
            await _repository.Meal.RemoveMealToOneDayFromTable(user.Id, numberOfDay.ToString(), wariant.ToString());
        }

        public async Task<IActionResult> RefreshReceipeView()
        {
            UserEntity user = GetLoggedUser(_repository.User);
            string variantName = user.MenuWariantName;
            List<FoodWithDayModel> dailyMeals = GetDailyMealsForUserAsync(user, variantName).Result;
            return PartialView("_Recipe", dailyMeals);
        }

        [HttpGet]
        public async Task<IActionResult> GenerateMealsAsync()
        {
            UserEntity user = GetLoggedUser(_repository.User);
            SetMealsForUser setMeals = new SetMealsForUser(_repository);

            var variantId = await _repository.Meal.GetIdOfMEalIfExist(defaultVariantName, user.Id);
            if (variantId != null)
            {
                await _repository.Meal.RemoveMealToOneVariantFromTableAsync(user.Id, variantId);
            }

            double cpm = 0;
            try {
                cpm = await GetCPM(user);
            }
            catch (Exception e){
                return Json(new { success = false });
            }
            

            for (int i = 1; i <= 7; i++)
            {
                await setMeals.PlanDiet(user, cpm, i, defaultVariantId);
            }
            
            List<FoodWithDayModel> dailyMeals = GetDailyMealsForUserAsync(user, defaultVariantName).Result;
            user.MenuWariantName = defaultVariantName;
            _repository.User.UpdateUser(user);

            return PartialView("_Recipe", dailyMeals);
        }

        private async Task<double> GetCPM(UserEntity user)
        {
            var indicators = await _repository.Indicator.GetLastIndicatorFromTable(user.Id);

            var personalData = new PersonalDataSettings
            {
                Age = user.Age,
                Lifestyle = user.LifeStyle,
                Gender = user.Gender,
                Height = indicators.IndicatorsModelData.Height,
                Weight = indicators.IndicatorsModelData.Weight,
            };

            var cpm = ParametersCalc.CountCPM(personalData);

            return cpm;
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

        [HttpGet]
        public PartialViewResult GetSaveMealsView()
        {
            var model = new SaveMeals();
            return PartialView("_SaveMealsModal", model);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetVariantsFromDbView()
        {
            var user = GetLoggedUser(_repository.User);
            var variants = new List<Variants>();
            var variantsFromDb = await _repository.Meal.GetAllVariantsName(user.Id);
            variantsFromDb = variantsFromDb.Where(x => x != defaultVariantName).ToList();
            foreach (var variant in variantsFromDb)
            {
                variants.Add(new Variants
                {
                    Name = variant,
                    Selected=false
                });
            }
            return PartialView("_VariantsFromDbModal", variants);
        }

        [HttpPost]
        public async Task<IActionResult> SaveVariant(SaveMeals model)
        {
            if (ModelState.IsValid)
            {
                var user = GetLoggedUser(_repository.User);
                var wariantId = await _repository.Meal.GetIdOfMEalIfExist(model.Name.ToLower(), user.Id);
                if (wariantId == null)
                {
                    await SaveVariant(model.Name, user);
                    return Json(new { success = true });
                }
                return Json(new { success = false, nameExist = true });
            }

            return PartialView("_SaveMealsModal", model);
        }

        private async Task SaveVariant(string variantName, UserEntity user)
        {
            var maxVariantId = await _repository.Meal.GetMaxVariantId(user.Id);
            var variantId = maxVariantId + 1;
            var meals = await _repository.Meal.GetMealsFromTable(user.Id, defaultVariantName);
            foreach(var meal in meals)
            {
                await _repository.Meal.InsertMealIntoTable(new MealModel(user.Id, meal.JsonId,meal.MealNumber,meal.MealTypeId,variantId,variantName.ToLower()));
            }
        }

        [HttpGet]
        public ActionResult ChooseVariant(string variantName)
        {
            var user = GetLoggedUser(_repository.User);
            user.MenuWariantName = variantName;
            _repository.User.UpdateUser(user);
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<ActionResult> DeleteVariant(string variantName)
        {
            var user = GetLoggedUser(_repository.User);
            int daysCount = 7;
            var variantId = await _repository.Meal.GetIdOfMEalIfExist(variantName, user.Id);
            for(int i = 1; i < daysCount+1; i++)
            {
                await RemoveMealsFromDay(user,i, int.Parse(variantId));
            }
            
            if (variantName == user.MenuWariantName)
            {
                user.MenuWariantName = defaultVariantName;
                _repository.User.UpdateUser(user);
            }

            return RedirectToAction("GetVariantsFromDbView");
        }

        private async Task<List<FoodWithDayModel>> GetDailyMealsForUserAsync(UserEntity user, string variantName)
        {
            List<FoodWithDayModel> dailyMeals = new List<FoodWithDayModel>();
            var userMeals = await _repository.Meal.GetMealsFromTable(user.Id, variantName);
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
