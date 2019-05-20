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
            _mealsTable = configuration.GetVariable("MealTable");
        }
        public async Task InsertMealIntoTable(MealModel model)
        {
            var table = await _tableStorage.GetTableReference(_mealsTable);
            var entity = new MealEntity()
            {
                PartitionKey = Guid.NewGuid().ToString(),
                RowKey = new Guid().ToString(),
                MealsModelData = model
            };

            var tableOperation = TableOperation.InsertOrMerge(entity);
            await table.ExecuteAsync(tableOperation);
        }

        public async Task<List<MealModel>> GetIMealFromTable(string idMeal)
        {
            var cloudTable = await _tableStorage.GetTableReference(_mealsTable);
            TableQuery<MealEntity> query = new TableQuery<MealEntity>()
                .Where(TableQuery.GenerateFilterCondition("IdMeal", QueryComparisons.Equal, idMeal));
            TableContinuationToken tableContinuationToken = new TableContinuationToken();
            
            List<MealModel> result = new List<MealModel>();
            do
            {
                var segmentedResult = await cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = segmentedResult.ContinuationToken;
                result.AddRange(segmentedResult.Results.Select(s => s.MealsModelData));
            } while (tableContinuationToken != null);
            return result;
        }
    }
}
