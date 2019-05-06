using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.StorageModels
{
    public class MealTypeEntity:TableEntity
    {
        public MealTypeModel MealTypeModelData { get; set; }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext context)
        {

            int idMealType = 0;
            string name = "";
            string step = "";
            foreach (var prop in properties)
            {
                switch (prop.Key.ToLower())
                {
                    case "idmealtype":
                        idMealType = (int)prop.Value.Int32Value;
                        break;
                    case "name":
                        name = prop.Value.StringValue;
                        break;
                    case "step":
                        step = prop.Value.StringValue;
                        break;
                }

                MealTypeModelData = new MealTypeModel(idMealType, name,step);
            }
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext context)
        {
            var result = new Dictionary<string, EntityProperty>
            {
                {nameof(MealTypeModelData.IdMealType), new EntityProperty(MealTypeModelData.IdMealType)},
                {nameof(MealTypeModelData.Name), new EntityProperty(MealTypeModelData.Name)},
                {nameof(MealTypeModelData.Step), new EntityProperty(MealTypeModelData.Step)}
            };
            return result;
        }
    }
}

