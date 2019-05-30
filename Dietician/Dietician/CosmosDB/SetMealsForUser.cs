﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Enums;
using Dietician.Storage.Interfaces;
using Dietician.Storage.StorageModels;
using Microsoft.Azure.Documents.SystemFunctions;

namespace Dietician.CosmosDB
{
    public class SetMealsForUser
    {

        private readonly ICosmosRepository _cosmosRepository;
        private List<CosmosMealModel> _updateList;
        private IRepositoryWrapper _wrapper;


        public SetMealsForUser(ICosmosRepository cosmosRepository, IRepositoryWrapper wrapper)
        {
            _cosmosRepository = cosmosRepository;
            _wrapper = wrapper;
        }
        private List<CosmosMealModel> UpdateList(string idForUser)
        {

            _updateList = new List<CosmosMealModel>();
            var list = _cosmosRepository.GetAllMeals().Meals;
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
            //if (user.Result.MealSettingsModelData.IngredientsId==0)
            if (true)
            {
                return _updateList;
            }
            else
            {
                var ingredients = _wrapper.Ingredients.GetIIngredientsFromTable(idForUser);
                //TODO: ustalić jak zapiszemy skladniki zeby je wyrzucic - enum? int to nie najlepsza opcja
                return _updateList;
            }

        }

        public void PlanDiet(string idUser, double cpmDaily, DateTime date)
        {
            var userSetting = _wrapper.MealSetting.GetMealSettingFromTable(idUser).Result.MealSettingsModelData;
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

            var breakfastList = new List<CosmosMealModel>();
            var secondBreakfastList = new List<CosmosMealModel>();
            var dinnerList = new List<CosmosMealModel>();
            var supperList = new List<CosmosMealModel>();
            var dessertList = new List<CosmosMealModel>();

            var breakfast = new CosmosMealModel();
            var secondBreakfast = new CosmosMealModel();
            var dinner = new CosmosMealModel();
            var dessert = new CosmosMealModel();
            var supper = new CosmosMealModel();
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

                _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, breakfast.Guid, date, breakfast.Type));
                _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, secondBreakfast.Guid, date, secondBreakfast.Type));
                _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, dinner.Guid, date, dinner.Type));
                _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, dessert.Guid, date, dessert.Type));
                _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, supper.Guid, date, supper.Type));
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
                _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, breakfast.Guid, date, breakfast.Type));
                _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, secondBreakfast.Guid, date, secondBreakfast.Type));
                _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, dinner.Guid, date, dinner.Type));
                _wrapper.Meal.InsertMealIntoTable(new MealModel(idUser, supper.Guid, date, supper.Type));
            }
        }
    }
}
