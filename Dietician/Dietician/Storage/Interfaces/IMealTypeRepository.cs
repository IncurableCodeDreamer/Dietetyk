using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.StorageModels;

namespace Dietician.Storage.Interfaces
{
    interface IMealTypeRepository
    {
        Task InsertMealTypeIntoTable(MealTypeModel model);
        Task<MealTypeEntity> GetMealTypeFromTable(string idMealType);
    }
}
