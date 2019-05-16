using Dietician.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.StorageModels
{
    public class FoodModel
    {
        public string ImageUrl { get; set; }
        public MealType Type { get; set; }
        public string Kind { get; set; }
        public double Fat { get; set; }
        public double Carbohydrates { get; set; }
        public double Proteins { get; set; }
        public double Calories { get; set; }
        public string Url { get; set; }
        public string Prepare { get; set; }
        public string Portions { get; set; }
        public string Ingredients { get; set; }
        public string Name { get; set; }
        public string Guid { get; set; }


        public FoodModel(string guid, string name, string ingredients, MealType type, string kind, string portions, string prepare, string url, double calories, double proteins, double carbohydrates, double fat, string imageUrl)
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
    }
}
