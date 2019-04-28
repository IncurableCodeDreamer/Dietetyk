using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.Interfaces;
using Dietician.Storage.StorageModels;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.Repositories
{
    public class MealRepository:IMealRepository
    {
        private readonly TableStorage _tableStorage;
        private readonly string _mealsTable;

        public MealRepository(IAppConfiguration configuration)
        {
            _tableStorage = new TableStorage(configuration);
            _mealsTable = configuration.GetVariable("IndicatorsTable");
        }
        public async Task InsertMealIntoTable(MealModel model)
        {
            var table = await _tableStorage.GetTableReference(_mealsTable);
            var entity = new MealEntity()
            {
                PartitionKey = model.IdMeal.ToString(),
                RowKey = new Guid().ToString(),
                MealsModelData = model
            };

            var tableOperation = TableOperation.InsertOrMerge(entity);
            await table.ExecuteAsync(tableOperation);
        }

        public async Task<MealEntity> GetIMealFromTable(string idMeal)
        {
            var cloudTable = await _tableStorage.GetTableReference(_mealsTable);
            TableQuery<MealEntity> query = new TableQuery<MealEntity>()
                .Where(TableQuery.GenerateFilterCondition("IdMeal", QueryComparisons.Equal, idMeal));
            TableContinuationToken tableContinuationToken = new TableContinuationToken();
            var result = cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
            var entity = result.Result.FirstOrDefault();
            return entity;
        }
    }
}
