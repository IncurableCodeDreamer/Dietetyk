using Dietician.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.StorageModels
{
    public class MealSettingsModel
    {
        public string IdMealSettings { get; set; }
        public DietAim DietAim { get; set; }
        public int? MealCount { get; set; }
        public DietPreferences Preferences { get; set; }

        public MealSettingsModel()
        {

        }

        public MealSettingsModel(string idMealSettings, DietAim dietAim, int? mealCount, DietPreferences preferences)
        {
            IdMealSettings = idMealSettings;
            DietAim = dietAim;
            MealCount = mealCount;
            Preferences = preferences;
        }
    }
}
