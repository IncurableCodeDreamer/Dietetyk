using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.StorageModels
{
    public class MealEntity:TableEntity
    {
        public MealModel MealsModelData { get; set; }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext context)
        {

            int idMeal=0;
            int jsonId = 0;
            DateTime mealDate = new DateTime();
           int mealTypeId=0;
            foreach (var prop in properties)
            {
                switch (prop.Key.ToLower())
                {
                    case "idmeal":
                        idMeal = (int)prop.Value.Int32Value;
                        break;
                    case "jsonid":
                        jsonId = (int)prop.Value.Int32Value;
                        break;
                    case "mealtypeid":
                        mealTypeId = (int)prop.Value.Int32Value;
                        break;
                    case "mealdate":
                        mealDate = (DateTime)prop.Value.DateTime;
                        break;
                }

                MealsModelData = new MealModel(idMeal, jsonId, mealDate, mealTypeId);
            }
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext context)
        {
            var result = new Dictionary<string, EntityProperty>
            {
                {nameof(MealsModelData.IdMeal), new EntityProperty(MealsModelData.IdMeal)},
                {nameof(MealsModelData.JsonId), new EntityProperty(MealsModelData.JsonId)},
                { nameof(MealsModelData.MealDate), new EntityProperty(MealsModelData.MealDate)},
                {nameof(MealsModelData.MealTypeId), new EntityProperty(MealsModelData.MealTypeId)}
            };
            return result;
        }
    }
}

