using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Enums;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.StorageModels
{
    public class MealSettingsEntity:TableEntity
    {
        public MealSettingsModel MealSettingsModelData { get; set; }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext context)
        {

            string idMealSetting = "";
            DietAim dietAim = 0;
            int mealCount = 0;
            DietPreferences preferences = 0;

            foreach (var prop in properties)
            {
                switch (prop.Key.ToLower())
                {
                    case "idmealsettings":
                        idMealSetting = prop.Value.StringValue;
                        break;
                    case "dietaim":
                        dietAim = (DietAim)prop.Value.Int32Value;
                        break;
                    case "preferences":
                        preferences = (DietPreferences)prop.Value.Int32Value;
                        break;
                    case "mealcount":
                        mealCount = (int)prop.Value.Int32Value;
                        break;
                }

                MealSettingsModelData = new MealSettingsModel(idMealSetting, dietAim, mealCount,preferences);
            }
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext context)
        {
            var result = new Dictionary<string, EntityProperty>
            {
                {nameof(MealSettingsModelData.IdMealSettings), new EntityProperty(MealSettingsModelData.IdMealSettings)},
                {nameof(MealSettingsModelData.DietAim), new EntityProperty((int)MealSettingsModelData.DietAim)},
                {nameof(MealSettingsModelData.MealCount), new EntityProperty(MealSettingsModelData.MealCount)},
                {nameof(MealSettingsModelData.Preferences), new EntityProperty((int)MealSettingsModelData.Preferences)}
            };
            return result;
        }
    }
}

