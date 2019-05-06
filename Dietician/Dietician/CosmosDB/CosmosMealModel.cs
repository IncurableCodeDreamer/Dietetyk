using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Enums;
using Newtonsoft.Json;

namespace Dietician.CosmosDB
{
    public class CosmosMealModel
    {
        public CosmosMealModel(string guid,string name, string ingredients, MealType type, string kind, string portions, string prepare, string url, double calories, double proteins, double carbohydrates, double fat, string imageUrl)
        {
            this.Guid = guid;
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

        public void ChangeType(CosmosMealModel model, string type)
        {

            if (type.Equals("sn"))
            {
                model.Type = MealType.Sniadanie;
            }

            if (type.Equals("ds"))
            {
                model.Type = MealType.Sniadanie2;
            }

            if (type.Equals("ob"))
            {
                model.Type = MealType.Obiad;
            }

            if (type.Equals("pd"))
            {
                model.Type = MealType.Podwieczorek;
            }

            if (type.Equals("kl"))
            {
                model.Type = MealType.Kolacja;
            }
        }
        [JsonProperty("Url")]
        public string ImageUrl { get; set; }
        [JsonProperty("Rodzaj")]
        public MealType Type { get; set; }
        [JsonProperty("Typ")]
        public string Kind { get; set; }
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
        [JsonProperty("Guid")]
        public string Guid { get; set; }
    }

    public class MealList
    {
        [JsonProperty("Data")]
        public List<CosmosMealModel> Meals { get; set; }
    }
}

