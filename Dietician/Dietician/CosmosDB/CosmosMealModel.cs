using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dietician.CosmosDB
{
    public class CosmosMealModel
    {
        public CosmosMealModel(string name, string ingredients, string type, string kind, string portions, string prepare, string url, double calories, double proteins, double carbohydrates, double fat, string imageUrl)
        {

            this.Name = name;
            this.Ingredients = ingredients;
            this.Portions = portions;
            this.Prepare = prepare;
            this.Url = url;
            this.Calories = calories;
            this.Proteins = proteins;
            this.Carbohydrates = carbohydrates;
            this.Fat = fat;
            this.Type = type;
            this.Kind = kind;
            this.ImageUrl = imageUrl;
        }

        public CosmosMealModel()
        {

        }

        [JsonProperty("Url")]
        public string ImageUrl { get; set; }
        [JsonProperty("Rodzaj")]
        public string Kind { get; set; }
        [JsonProperty("Typ")]
        public string Type { get; set; }
        [JsonProperty("Tłuszcze")]
        public double Fat { get; set; }
        [JsonProperty("Weglowodany")]
        public double Carbohydrates { get; set; }
        [JsonProperty("Białka")]
        public double Proteins { get; set; }
        [JsonProperty("Kalorie")]
        public double Calories { get; set; }
        [JsonProperty("Źródło")]
        public string Url { get; set; }
        [JsonProperty("Przepis")]
        public string Prepare { get; set; }
        [JsonProperty("Porcje")]
        public string Portions { get; set; }
        [JsonProperty("Składniki")]
        public string Ingredients { get; set; }
        [JsonProperty("Nazwa")]
        public string Name { get; set; }
    }

    public class MealList
    {
        [JsonProperty("Data")]
        public List<CosmosMealModel> Meals { get; set; }
    }
}

