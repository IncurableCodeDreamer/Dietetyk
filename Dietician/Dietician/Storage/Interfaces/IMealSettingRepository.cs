using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.StorageModels;

namespace Dietician.Storage.Interfaces
{
    public interface IMealSettingRepository
    {
        Task<MealSettingsModel> InsertMealSettingsIntoTable(MealSettingsModel model);
        Task<MealSettingsEntity> GetMealSettingFromTable(string idMealSettings);
        Task UpdateMealSettings(MealSettingsEntity model);
    }
}
