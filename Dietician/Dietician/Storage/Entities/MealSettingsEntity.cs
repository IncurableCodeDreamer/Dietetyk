using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.StorageModels
{
    public class MealSettingsEntity:TableEntity
    {
        public MealSettingsModel MealSettingsModelData { get; set; }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext context)
        {

            int idMealSetting = 0;
            string dietAim = "";
            int mealCount = 0;
            string preferences = "";
            int ingredientsId = 0;

            foreach (var prop in properties)
            {
                switch (prop.Key.ToLower())
                {
                    case "idmealsetting":
                        idMealSetting = (int) prop.Value.Int32Value;
                        break;
                    case "dietaim":
                        dietAim = prop.Value.StringValue;
                        break;
                    case "preferences":
                        preferences = prop.Value.StringValue;
                        break;
                    case "mealcount":
                        mealCount = (int)prop.Value.Int32Value;
                        break;
                    case "ingredientsid":
                        ingredientsId = (int)prop.Value.Int32Value;
                        break;
                }

                MealSettingsModelData = new MealSettingsModel(idMealSetting, dietAim, mealCount,preferences,ingredientsId);
            }
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext context)
        {
            var result = new Dictionary<string, EntityProperty>
            {
                {nameof(MealSettingsModelData.IdMealSettings), new EntityProperty(MealSettingsModelData.IdMealSettings)},
                {nameof(MealSettingsModelData.DietAim), new EntityProperty(MealSettingsModelData.DietAim)},
                {nameof(MealSettingsModelData.MealCount), new EntityProperty(MealSettingsModelData.MealCount)},
                {nameof(MealSettingsModelData.Preferences), new EntityProperty(MealSettingsModelData.Preferences)},
                {nameof(MealSettingsModelData.IngredientsId), new EntityProperty(MealSettingsModelData.IngredientsId)}
            };
            return result;
        }
    }
}

