using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.StorageModels
{
    public class MealTypeModel
    {
        public int IdMealType { get; set; }
        public string Name { get; set; }
        public string Step { get; set; }

        public MealTypeModel()
        {

        }

        public MealTypeModel(int idMealType, string name, string step)
        {
            IdMealType = idMealType;
            Name = name;
            Step = step;
        }
    }
}
