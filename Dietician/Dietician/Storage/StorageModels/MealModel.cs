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
        public int MealNumber { get; set; }
        public MealType MealTypeId { get; set; }
        public int Variant { get; set; }
        public MealModel()
        {

        }

        public MealModel(string idUser, string jsonId, int mealDate, MealType mealTypeId, int variant)
        {
            Variant = variant;
            IdUser = idUser;
            JsonId = jsonId;
            MealNumber = mealDate;
            MealTypeId = mealTypeId;
        }
    }
}
