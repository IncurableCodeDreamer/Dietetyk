using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.StorageModels
{
    public class IngredientEntity:TableEntity
    {
        public IngredientsModel IngredientsModelData { get; set; }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext context)
        {

            string idIngredient = "";
            bool milk = false;
            bool eggs = false;
            bool chocolate = false;
            bool potatoes = false;
            bool peanuts = false;
            bool tomatoes = false;
            bool soy = false;
            bool wheat = false;

            foreach (var prop in properties)
            {
                switch (prop.Key.ToLower())
                {
                    case "idingredient":
                        idIngredient = prop.Value.StringValue;
                        break;
                    case "milk":
                        milk = (bool)prop.Value.BooleanValue;
                        break;
                    case "eggs":
                        eggs = (bool)prop.Value.BooleanValue;
                        break;
                    case "chocolate":
                        chocolate = (bool)prop.Value.BooleanValue;
                        break;
                    case "potatoes":
                        potatoes = (bool)prop.Value.BooleanValue;
                        break;
                    case "peanuts":
                        peanuts = (bool)prop.Value.BooleanValue;
                        break;
                    case "tomatoes":
                        tomatoes = (bool)prop.Value.BooleanValue;
                        break;
                    case "soy":
                        soy = (bool)prop.Value.BooleanValue;
                        break;
                    case "wheat":
                        wheat = (bool) prop.Value.BooleanValue;
                        break;
                }

                IngredientsModelData = new IngredientsModel(idIngredient, milk, eggs, chocolate, potatoes, peanuts,
                    tomatoes, soy, wheat);

            }
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext context)
        {
            var result = new Dictionary<string, EntityProperty>
            {
                {nameof(IngredientsModelData.IdIngredient), new EntityProperty(IngredientsModelData.IdIngredient)},
                {nameof(IngredientsModelData.Chocolate), new EntityProperty(IngredientsModelData.Chocolate)},
                {nameof(IngredientsModelData.Milk), new EntityProperty(IngredientsModelData.Milk)},
                { nameof(IngredientsModelData.Eggs), new EntityProperty(IngredientsModelData.Eggs)},
                {nameof(IngredientsModelData.Peanuts), new EntityProperty(IngredientsModelData.Peanuts)},
                { nameof(IngredientsModelData.Potatoes), new EntityProperty(IngredientsModelData.Potatoes)},
                { nameof(IngredientsModelData.Tomatoes), new EntityProperty(IngredientsModelData.Tomatoes)},
                { nameof(IngredientsModelData.Soy), new EntityProperty(IngredientsModelData.Soy)},
                {nameof(IngredientsModelData.Wheat), new EntityProperty(IngredientsModelData.Wheat)}
            };
            return result;
        }
    }
}

