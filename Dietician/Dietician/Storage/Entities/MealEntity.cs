using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Enums;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.StorageModels
{
    public class MealEntity:TableEntity
    {
        public MealModel MealsModelData { get; set; }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext context)
        {

            string idMeal="";
            string jsonId = "";
            DateTime mealDate = new DateTime();
           MealType mealTypeId=0;
            foreach (var prop in properties)
            {
                switch (prop.Key.ToLower())
                {
                    case "idmeal":
                        idMeal = prop.Value.StringValue;
                        break;
                    case "jsonid":
                        jsonId = prop.Value.StringValue;
                        break;
                    case "mealtypeid":
                        mealTypeId = (MealType) prop.Value.Int32Value;
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
                {nameof(MealsModelData.IdUser), new EntityProperty(MealsModelData.IdUser)},
                {nameof(MealsModelData.JsonId), new EntityProperty(MealsModelData.JsonId)},
                { nameof(MealsModelData.MealDate), new EntityProperty(MealsModelData.MealDate)},
                {nameof(MealsModelData.MealTypeId), new EntityProperty((int)MealsModelData.MealTypeId)}
            };
            return result;
        }
    }
}

