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

            int idIngredient = 0;
            int milk = 0;
            int eggs = 0;
            int chocolate = 0;
            int potatoes = 0;
            int peanuts = 0;
            int tomatoes = 0;
            int soy = 0;
            int wheat = 0;

            foreach (var prop in properties)
            {
                switch (prop.Key.ToLower())
                {
                    case "idingredient":
                        idIngredient = (int) prop.Value.Int32Value;
                        break;
                    case "milk":
                        milk = (int) prop.Value.Int32Value;
                        break;
                    case "eggs":
                        eggs = (int) prop.Value.Int32Value;
                        break;
                    case "chocolate":
                        chocolate = (int) prop.Value.Int32Value;
                        break;
                    case "potatoes":
                        potatoes = (int) prop.Value.Int32Value;
                        break;
                    case "peanuts":
                        peanuts = (int) prop.Value.Int32Value;
                        break;
                    case "tomatoes":
                        tomatoes = (int) prop.Value.Int32Value;
                        break;
                    case "soy":
                        soy = (int) prop.Value.Int32Value;
                        break;
                    case "wheat":
                        wheat = (int) prop.Value.Int32Value;
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

