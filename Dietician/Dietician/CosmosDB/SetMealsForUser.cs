using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.Interfaces;
using Microsoft.Azure.Documents.SystemFunctions;

namespace Dietician.CosmosDB
{
    public class SetMealsForUser
    {
        private readonly ICosmosRepository _cosmosRepository;
        private readonly IMealSettingRepository _mealSetting;
        private List<CosmosMealModel> _updateList;
        private readonly IIngredientsRepository _ingredientsRepository;
        private IIndicatorRepository _indicatorRepository;

        public SetMealsForUser(ICosmosRepository cosmosRepository, IMealSettingRepository mealSetting, IIngredientsRepository ingredientsRepository, IIndicatorRepository indicatorRepository)
        {
            _cosmosRepository = cosmosRepository;
            _mealSetting = mealSetting;
            _ingredientsRepository = ingredientsRepository;
            _indicatorRepository = indicatorRepository;
        }
        private List<CosmosMealModel> UpdateList( string idForUser)
        {

            _updateList = new List<CosmosMealModel>();
            var list = _cosmosRepository.GetAllMeals().Meals;
            var user = _mealSetting.GetMealSettingFromTable(idForUser);
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
            if(true)
            {
                return _updateList;
            }
            else
            {
                var ingredients = _ingredientsRepository.GetIIngredientsFromTable(idForUser);
                //TODO: ustalić jak zapiszemy skladniki zeby je wyrzucic - enum? int to nie najlepsza opcja
               return _updateList;
            }

        }

        public void PlanDiet(string idUser)
        {
            var user = _mealSetting.GetMealSettingFromTable(idUser);
            var goal = user.Result.MealSettingsModelData.DietAim.ToString();
            double cpmDaily = 0; //TODO:znalezc obliczanie CPM

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
            /*double proteinsMin = CPM.ProteinsMinCalculate(cpm1);
            double proteinsMax = CPM.ProteinsMaxCalculate(cpm2);
            double fatsMin = CPM.FatsMinCalculate(cpm1);
            double fatsMax = CPM.FatsMaxCalculate(cpm2);
            double carbohydratesMin = CPM.CarbohydratesMinCalculate(cpm1);
            double carbohydratesMax = CPM.CarbohydratesMaxCalculate(cpm2);



            int a, b, c, d, e;

            var breakfastList = new List<CosmosMealModel>();
            var secondBreakfastList = new List<CosmosMealModel>();
            var dinnerList = new List<CosmosMealModel>();
            var supperList =new List<CosmosMealModel>();
           var dessertList = new List<CosmosMealModel>();

             var breakfast = new CosmosMealModel();
        var secondBreakfast = new CosmosMealModel();
        var dinner = new CosmosMealModel();
        var dessert = new CosmosMealModel();
        var supper = new CosmosMealModel();

            foreach (var dailyMeal in _updateList)
            {

                if (dailyMeal.Type.Equals("sn"))
                {
                    breakfastList.Add(dailyMeal);
                }
                if (dailyMeal.Type.Equals("ds"))
                {
                    secondBreakfastList.Add(dailyMeal);
                }
                if (dailyMeal.Type.Equals("ob"))
                {
                    dinnerList.Add(dailyMeal);
                }
                if (dailyMeal.Type.Equals("pd"))
                {
                    dessertList.Add(dailyMeal);
                }
                if (dailyMealType.Equals("kl"))
                {
                    supperList.Add(dailyMeal);
                }
            }
          

            while (!((sum > cpm1) & (sum < cpm2) & (sumProteins > proteinsMin) & (sumProteins < proteinsMax) & (sumCarbohydrates > carbohydratesMin) & (sumCarbohydrates < carbohydratesMax) & (sumFats > fatsMin) & (sumFats < fatsMax)))
            {

                a = r.Next((breakfastList.Count() - 1) + 1);
                b = r.Next((secondBreakfastList.Count() - 1) + 1);
                c = r.Next((dinnerList.Count() - 1) + 1);
                d = r.Next((dessertList.Count() - 1) + 1);
                e = r.Next((supperList.Count() - 1) + 1);

                breakfast = breakfastList.Get(a);
                secondBreakfast = secondBreakfastList.Get(b);
                dinner = dinnerList.Get(c);
                dessert = dessertList.Get(d);
                supper = supperList.Get(e);

                sum = breakfast.Calories + secondBreakfast.Calories + dinner.Calories + dessert.Calories + supper.Calories();
                sumProteins = breakfast.Proteins + secondBreakfast.Proteins + dinner.Proteins + dessert.Proteins + supper.Proteins;
                sumFats = breakfast.Fat + secondBreakfast.Fat + dinner.Fat + dessert.Fat + supper.Fat;
                sumCarbohydrates = breakfast.Carbohydrates + secondBreakfast.Carbohydrates + dinner.Carbohydrates + dessert.Carbohydrates + supper.Carbohydrates;        

            }

           
        }*/

            //TODO wpisać wylosowany jadlospis na 1 dzien to tabel?ale których?

        }

    }
}
