using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.Interfaces;
using Dietician.Storage.StorageModels;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.Repositories
{
    public class MealSettingRepository: IMealSettingRepository
    {
        private readonly TableStorage _tableStorage;
        private readonly string _mealSettingTable;

        public MealSettingRepository(IAppConfiguration configuration)
        {
            _tableStorage = new TableStorage(configuration);
            _mealSettingTable = configuration.GetVariable("MealSettingsTable");
        }

        public async Task<MealSettingsModel> InsertMealSettingsIntoTable(MealSettingsModel model)
        {
            var table = await _tableStorage.GetTableReference(_mealSettingTable);
            model.IdMealSettings = Guid.NewGuid().ToString();

            var entity = new MealSettingsEntity()
            {
                PartitionKey = model.IdMealSettings,
                RowKey = new Guid().ToString(),
                MealSettingsModelData = model
            };

            var tableOperation = TableOperation.InsertOrMerge(entity);
            await table.ExecuteAsync(tableOperation);
            
            return model;
        }

        public async Task UpdateMealSettings(MealSettingsEntity model)
        {
            var cloudTable = await _tableStorage.GetTableReference(_mealSettingTable);
            TableOperation op = TableOperation.Replace(model);
            var result = cloudTable.ExecuteAsync(op);
        }

        public async Task<MealSettingsEntity> GetMealSettingFromTable(string idMealSettings)
        {
            var cloudTable = await _tableStorage.GetTableReference(_mealSettingTable);
            if (cloudTable == null) throw new ArgumentNullException(nameof(cloudTable));
            TableQuery<MealSettingsEntity> query = new TableQuery<MealSettingsEntity>()
                .Where(TableQuery.GenerateFilterCondition("IdMealSettings", QueryComparisons.Equal, idMealSettings));
            TableContinuationToken tableContinuationToken = new TableContinuationToken();
            var result = cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
            var entity = result.Result.FirstOrDefault();
            return entity;
        }
    }
}
