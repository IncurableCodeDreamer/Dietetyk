using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Enums;

namespace Dietician.Storage.StorageModels
{
    public class MealModel
    {
        public string IdUser { get; set; }
        public string JsonId { get; set; }
        public DateTime MealDate { get; set; }
        public MealType MealTypeId { get; set; }

        public MealModel()
        {

        }

        public MealModel(string idUser, string jsonId, DateTime mealDate, MealType mealTypeId)
        {
            IdUser = idUser;
            JsonId = jsonId;
            MealDate = mealDate;
            MealTypeId = mealTypeId;
        }
    }
}
