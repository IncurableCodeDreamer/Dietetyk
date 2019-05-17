using Dietician.Storage.StorageModels;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.Entities
{
    public class ShoppingListEntity:TableEntity
    {
        public ShoppingListModel ShopModelData { get; set; }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext context)
        {

            string userId = "";
            string ingredient = "";
            
            foreach (var prop in properties)
            {
                switch (prop.Key.ToLower())
                {
                    case "userid":
                        userId = prop.Value.StringValue;
                        break;
                    case "ingredient":
                        ingredient = prop.Value.StringValue;
                        break;
                }

                ShopModelData = new ShoppingListModel(userId, ingredient);
            }
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext context)
        {
            var result = new Dictionary<string, EntityProperty>
            {
                {nameof(ShopModelData.UserId), new EntityProperty(ShopModelData.UserId)},
                {nameof(ShopModelData.Ingredient), new EntityProperty(ShopModelData.Ingredient)},
            };
            return result;
        }
    }
}

    