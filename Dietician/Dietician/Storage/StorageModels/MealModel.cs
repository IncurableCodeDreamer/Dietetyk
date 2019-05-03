using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.StorageModels
{
    public class MealModel
    {
        public int IdMeal { get; set; }
        public int JsonId { get; set; }
        public DateTime MealDate { get; set; }
        public int MealTypeId { get; set; }

        public MealModel()
        {

        }

        public MealModel(int idMeal, int jsonId, DateTime mealDate, int mealTypeId)
        {
            IdMeal = idMeal;
            JsonId = jsonId;
            MealDate = mealDate;
            MealTypeId = mealTypeId;
        }
    }
}
