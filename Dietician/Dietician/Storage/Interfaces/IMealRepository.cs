using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.StorageModels;

namespace Dietician.Storage.Interfaces
{
    interface IMealRepository
    {
            Task InsertMealIntoTable(MealModel model);
            Task<MealEntity> GetIMealFromTable(string idMeal);        
    }
}
