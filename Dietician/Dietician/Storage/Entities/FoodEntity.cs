using Dietician.Enums;
using Dietician.Storage.StorageModels;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;

namespace Dietician.Storage.Entities
{
    public class FoodEntity : TableEntity
    {
        public FoodModel FoodModelData { get; set; }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext context)
        {
            string imageUrl = "";
            MealType mealTypeId = 0;
            string kind = "";
            double fat = 0;
            double carbohydrates = 0;
            double proteins = 0;
            double calories = 0;
            string url = "";
            string prepare = "";
            string portions = "";
            string ingredients = "";
            string name = "";
            string guid = "";

            foreach (var prop in properties)
            {
                switch (prop.Key.ToLower())
                {
                    case "imageurl":
                        imageUrl = prop.Value.StringValue;
                        break;
                    case "kind":
                        kind = prop.Value.StringValue;
                        break;
                    case "type":
                        mealTypeId = (MealType)prop.Value.Int32Value;
                        break;
                    case "carbohydrates":
                        carbohydrates = (Double)prop.Value.DoubleValue;
                        break;
                    case "fat":
                        fat = (Double)prop.Value.DoubleValue;
                        break;
                    case "proteins":
                        proteins = (Double)prop.Value.DoubleValue;
                        break;
                    case "calories":
                        calories = (Double)prop.Value.DoubleValue;
                        break;
                    case "url":
                        url = prop.Value.StringValue;
                        break;
                    case "prepare":
                        prepare = prop.Value.StringValue;
                        break;
                    case "portions":
                        portions = prop.Value.StringValue;
                        break;
                    case "ingredients":
                        ingredients = prop.Value.StringValue;
                        break;
                    case "name":
                        name = prop.Value.StringValue;
                        break;
                    case "guid":
                        guid = prop.Value.StringValue;
                        break;
                }

                FoodModelData = new FoodModel(guid, name, ingredients, mealTypeId, kind, portions, prepare, url, calories,
                    proteins, carbohydrates, fat, imageUrl);
            }
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext context)
        {
            var result = new Dictionary<string, EntityProperty>
            {
                {nameof(FoodModelData.Guid), new EntityProperty(FoodModelData.Guid)},
                {nameof(FoodModelData.Name), new EntityProperty(FoodModelData.Name)},
                { nameof(FoodModelData.Ingredients), new EntityProperty(FoodModelData.Ingredients)},
                {nameof(FoodModelData.Portions), new EntityProperty(FoodModelData.Portions)},
                {nameof(FoodModelData.Prepare), new EntityProperty(FoodModelData.Prepare)},
                {nameof(FoodModelData.Url), new EntityProperty(FoodModelData.Url)},
                {nameof(FoodModelData.Calories), new EntityProperty(FoodModelData.Calories)},
                {nameof(FoodModelData.Proteins), new EntityProperty(FoodModelData.Proteins)},
                {nameof(FoodModelData.Carbohydrates), new EntityProperty(FoodModelData.Carbohydrates)},
                {nameof(FoodModelData.Fat), new EntityProperty(FoodModelData.Fat)},
                {nameof(FoodModelData.Type), new EntityProperty((int)FoodModelData.Type)},
                {nameof(FoodModelData.Kind), new EntityProperty(FoodModelData.Kind)},
                {nameof(FoodModelData.ImageUrl), new EntityProperty(FoodModelData.ImageUrl)}
            };
            return result;
        }
    }
}
