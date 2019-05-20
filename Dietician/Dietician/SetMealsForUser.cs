using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Enums;
using Dietician.Storage.Interfaces;
using Dietician.Storage.StorageModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.SystemFunctions;

namespace Dietician.CosmosDB
{
    public class SetMealsForUser
    {
        private IRepositoryWrapper _wrapper;

        public SetMealsForUser(IRepositoryWrapper wrapper)
        {
            _wrapper = wrapper;
        }
        private async Task<List<FoodModel>> UpdateListAsync(string idForUser)
        {
            List<FoodModel> _updateList = new List<FoodModel>();
            var list = await _wrapper.Food.GetAllFoodsFromTable();
            var user = _wrapper.MealSetting.GetMealSettingFromTable(idForUser);
            if (user.Result.MealSettingsModelData.Preferences.ToString().Contains("Wegetariańska"))
            {
                foreach (var item in list)
                {
                    if (item.Kind.Equals("w"))
                    {
                        _updateList.Add(item);
                    }
                }
            }
            else
            {
                _updateList = list;
            }

            //TODO uaktualnic warunek na podstawie nowych modeli
            var ingredients = await _wrapper.Ingredients.GetIIngredientsFromTable(idForUser);
            if (ingredients.IngredientsModelData.Chocolate == true)
            {
                var l1 = _updateList.Where(o => o.Ingredients.Contains("czekol")).ToList();
                foreach (var item in l1)
                {
                    _updateList.Remove(item);
                }
            }
            if (ingredients.IngredientsModelData.Eggs == true)
            {
                var l1 = _updateList.Where(o => o.Ingredients.Contains("jaj")).ToList();
                foreach (var item in l1)
                {
                    _updateList.Remove(item);
                }
            }
            if (ingredients.IngredientsModelData.Milk == true)
            {
                var l1 = _updateList.Where(o => o.Ingredients.Contains("mlek")).ToList();
                foreach (var item in l1)
                {
                    _updateList.Remove(item);
                }
            }
            if (ingredients.IngredientsModelData.Peanuts == true)
            {
                var l1 = _updateList.Where(o => o.Ingredients.Contains("orze")).ToList();
                foreach (var item in l1)
                {
                    _updateList.Remove(item);
                }
            }
            if (ingredients.IngredientsModelData.Potatoes == true)
            {
                var l1 = _updateList.Where(o => o.Ingredients.Contains("ziemni")).ToList();
                foreach (var item in l1)
                {
                    _updateList.Remove(item);
                }
            }
            if (ingredients.IngredientsModelData.Soy == true)
            {
                var l1 = _updateList.Where(o => o.Ingredients.Contains("soj")).ToList();
                foreach (var item in l1)
                {
                    _updateList.Remove(item);
                }
            }
            if (ingredients.IngredientsModelData.Tomatoes == true)
            {
                var l1 = _updateList.Where(o => o.Ingredients.Contains("pomid")).ToList();
                foreach (var item in l1)
                {
                    _updateList.Remove(item);
                }
            }
            if (ingredients.IngredientsModelData.Wheat == true)
            {
                var l1 = _updateList.Where(o => o.Ingredients.Contains("pszen")).ToList();
                foreach (var item in l1)
                {
                    _updateList.Remove(item);
                }
            }
            else
            {
                return _updateList;
            }
            return _updateList;
        }

        public async Task<IActionResult> PlanDiet(string idUser, double cpmDaily, DateTime date, int variable)
        {
            var userSetting = _wrapper.MealSetting.GetMealSettingFromTable(idUser).Result.MealSettingsModelData;
            var myTask = await Task.Run(() => UpdateListAsync(idUser));// await UpdateListAsync(idUser).Result;
            var goal = userSetting.DietAim.ToString();
            var count = userSetting.MealCount;

            switch (goal)
            {
                case "Redukcja":
                    cpmDaily = cpmDaily - 200;
                    break;
                case "Masa":
                    cpmDaily = cpmDaily + 200;
                    break;
            }

            Random r = new Random();
            double sum = 0;
            double sumProteins = 0;
            double sumFats = 0;
            double sumCarbohydrates = 0;
            double cpm1 = cpmDaily - 150;
            double cpm2 = cpmDaily + 150;
            double proteinsMin = ParametersCalc.ProteinsMinCalculate(cpm1);
            double proteinsMax = ParametersCalc.ProteinsMaxCalculate(cpm2);
            double fatsMin = ParametersCalc.FatsMinCalculate(cpm1);
            double fatsMax = ParametersCalc.FatsMaxCalculate(cpm2);
            double carbohydratesMin = ParametersCalc.CarbohydratesMinCalculate(cpm1);
            double carbohydratesMax = ParametersCalc.CarbohydratesMaxCalculate(cpm2);

            int a, b, c, d, e;

            var breakfastList = new List<FoodModel>();
            var secondBreakfastList = new List<FoodModel>();
            var dinnerList = new List<FoodModel>();
            var supperList = new List<FoodModel>();
            var dessertList = new List<FoodModel>();

            var breakfast = new FoodModel();
            var secondBreakfast = new FoodModel();
            var dinner = new FoodModel();
            var dessert = new FoodModel();
            var supper = new FoodModel();

            List<FoodModel> _updateList = null;// await myTask;

            foreach (var dailyMeal in _updateList)
            {
                switch (dailyMeal.Type)
                {
                    case MealType.Sniadanie:
                        breakfastList.Add(dailyMeal);
                        break;
                    case MealType.Sniadanie2:
                        secondBreakfastList.Add(dailyMeal);
                        break;
                    case MealType.Obiad:
                        dinnerList.Add(dailyMeal);
                        break;
                    case MealType.Podwieczorek:
                        dessertList.Add(dailyMeal);
                        break;
                    case MealType.Kolacja:
                        supperList.Add(dailyMeal);
                        break;
                }
            }

            if (count == 5)
            {
                while (!((sum > cpm1) & (sum < cpm2) & (sumProteins > proteinsMin) & (sumProteins < proteinsMax) &
                         (sumCarbohydrates > carbohydratesMin) & (sumCarbohydrates < carbohydratesMax) &
                         (sumFats > fatsMin) & (sumFats < fatsMax)))
                {

                    a = r.Next((breakfastList.Count() - 1) + 1);
                    b = r.Next((secondBreakfastList.Count() - 1) + 1);
                    c = r.Next((dinnerList.Count() - 1) + 1);
                    d = r.Next((dessertList.Count() - 1) + 1);
                    e = r.Next((supperList.Count() - 1) + 1);

                    breakfast = breakfastList[a];
                    secondBreakfast = secondBreakfastList[b];
                    dinner = dinnerList[c];
                    dessert = dessertList[d];
                    supper = supperList[e];

                    sum = breakfast.Calories + secondBreakfast.Calories + dinner.Calories + dessert.Calories +
                          supper.Calories;
                    sumProteins = breakfast.Proteins + secondBreakfast.Proteins + dinner.Proteins + dessert.Proteins +
                                  supper.Proteins;
                    sumFats = breakfast.Fat + secondBreakfast.Fat + dinner.Fat + dessert.Fat + supper.Fat;
                    sumCarbohydrates = breakfast.Carbohydrates + secondBreakfast.Carbohydrates + dinner.Carbohydrates +
                                       dessert.Carbohydrates + supper.Carbohydrates;

                }

                await _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, breakfast.Guid, date, breakfast.Type,variable));
                await _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, secondBreakfast.Guid, date, secondBreakfast.Type,variable));
                await _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, dinner.Guid, date, dinner.Type,variable));
                await _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, dessert.Guid, date, dessert.Type,variable));
                await _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, supper.Guid, date, supper.Type,variable));
            }
            else
            {
                while (!((sum > cpm1) & (sum < cpm2) & (sumProteins > proteinsMin) & (sumProteins < proteinsMax) &
                         (sumCarbohydrates > carbohydratesMin) & (sumCarbohydrates < carbohydratesMax) &
                         (sumFats > fatsMin) & (sumFats < fatsMax)))
                {

                    a = r.Next((breakfastList.Count() - 1) + 1);
                    b = r.Next((secondBreakfastList.Count() - 1) + 1);
                    c = r.Next((dinnerList.Count() - 1) + 1);
                    e = r.Next((supperList.Count() - 1) + 1);

                    breakfast = breakfastList[a];
                    secondBreakfast = secondBreakfastList[b];
                    dinner = dinnerList[c];
                    supper = supperList[e];

                    sum = breakfast.Calories + secondBreakfast.Calories + dinner.Calories +
                          supper.Calories;
                    sumProteins = breakfast.Proteins + secondBreakfast.Proteins + dinner.Proteins  +
                                  supper.Proteins;
                    sumFats = breakfast.Fat + secondBreakfast.Fat + dinner.Fat  + supper.Fat;
                    sumCarbohydrates = breakfast.Carbohydrates + secondBreakfast.Carbohydrates + dinner.Carbohydrates +
                                        supper.Carbohydrates;

                }
                await _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, breakfast.Guid, date, breakfast.Type,variable));
                await _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, secondBreakfast.Guid, date, secondBreakfast.Type,variable));
                await _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, dinner.Guid, date, dinner.Type,variable));
                await  _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, supper.Guid, date, supper.Type,variable));
            }
            return null;
        }
    }
}

