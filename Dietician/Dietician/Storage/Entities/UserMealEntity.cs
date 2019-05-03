using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.StorageModels
{
    public class UserMealEntity:TableEntity
    {
        public UserMealModel UserMealModelData { get; set; }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext context)
        {

            int idMeal = 0;
            int idUser = 0;
            int idUserMeal = 0;
            foreach (var prop in properties)
            {
                switch (prop.Key.ToLower())
                {
                    case "idmeal":
                        idMeal = (int)prop.Value.Int32Value;
                        break;
                    case "iduser":
                        idUser = (int)prop.Value.Int32Value;
                        break;
                    case "idusermeal":
                        idUserMeal = (int)prop.Value.Int32Value;
                        break;
                }

                UserMealModelData = new UserMealModel(idUserMeal, idUser, idMeal);
            }
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext context)
        {
            var result = new Dictionary<string, EntityProperty>
            {
                {nameof(UserMealModelData.IdUser), new EntityProperty(UserMealModelData.IdUser)},
                { nameof(UserMealModelData.IdMeal), new EntityProperty(UserMealModelData.IdMeal)},
                {nameof(UserMealModelData.IdUserMeal), new EntityProperty(UserMealModelData.IdUserMeal)}
            };
            return result;
        }
    }
}

