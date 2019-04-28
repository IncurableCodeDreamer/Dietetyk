using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.StorageModels
{
    public class MealSettingsModel
    {
        public int IdMealSettings { get; set; }
        public string DietAim { get; set; }
        public int MealCount { get; set; }
        public string Preferences { get; set; }
        public int IngredientsId { get; set; }

        public MealSettingsModel(int idMealSettings, string dietAim, int mealCount, string preferences, int ingredientsId)
        {
            IdMealSettings = idMealSettings;
            DietAim = dietAim;
            MealCount = mealCount;
            Preferences = preferences;
            IngredientsId = ingredientsId;
        }
    }
}
