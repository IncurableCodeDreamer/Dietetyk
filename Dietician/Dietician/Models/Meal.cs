using Dietician.CosmosDB;
using Dietician.Enums;
using System;

namespace Dietician.Models
{
    public class Meal
    {
        public DateTime Date { get; set; }
        public MealType MealType { get; set; }
        public int JsonId { get; set; }
        public CosmosMealModel CosmosMeal{ get; set; }
    }
}
