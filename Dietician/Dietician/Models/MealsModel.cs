using Dietician.CosmosDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Models
{
    public class MealsModel
    {
        public List<Meal> DailyMeals { get; set; }
        public ChangeMenu ChangeMenu { get; set; }
        public AddMeal AddMeal { get; set; }
    }
}
