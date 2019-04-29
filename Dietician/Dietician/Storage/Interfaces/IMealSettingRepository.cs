using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.StorageModels;

namespace Dietician.Storage.Interfaces
{
    interface IMealSettingRepository
    {
        Task InsertMealSettingsIntoTable(MealSettingsModel model);
        Task<MealSettingsEntity> GetMealSettingFromTable(string idMealSettings);
    }
}
