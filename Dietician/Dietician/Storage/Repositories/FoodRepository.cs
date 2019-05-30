using Dietician.Storage.Entities;
using Dietician.Storage.Interfaces;
using Dietician.Storage.StorageModels;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.Repositories
{
    public class FoodRepository:IFoodRepository
    {
        private readonly TableStorage _tableStorage;
        private readonly string _foodTable;

        public FoodRepository(IAppConfiguration configuration)
        {
            _tableStorage = new TableStorage(configuration);
            _foodTable = configuration.GetVariable("FoodTable");
        }
        public async Task InsertFoodIntoTable(FoodModel model)
        {
            var table = await _tableStorage.GetTableReference(_foodTable);
            var entity = new FoodEntity()
            {
                PartitionKey = Guid.NewGuid().ToString(),
                RowKey = new Guid().ToString(),
                FoodModelData = model
            };

            var tableOperation = TableOperation.InsertOrMerge(entity);
            await table.ExecuteAsync(tableOperation);
        }

        public async Task<List<FoodModel>> GetAllFoodsFromTable()
        {
            var table = await _tableStorage.GetTableReference(_foodTable);
            var filterCondition = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual, null);
            var query = new TableQuery<FoodEntity>().Where(filterCondition);
            TableContinuationToken tableContinuationToken = new TableContinuationToken();
            
            List<FoodModel> result = new List<FoodModel>();
            do
            {
                var segmentedResult = await table.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = segmentedResult.ContinuationToken;
                result.AddRange(segmentedResult.Results.Select(s => s.FoodModelData));
            } while (tableContinuationToken != null);
            return result;
        }

        public async Task<FoodModel> GetOneFood(string idFood)
        {
            var cloudTable = await _tableStorage.GetTableReference(_foodTable);
            TableQuery<FoodEntity> query = new TableQuery<FoodEntity>()
                .Where(TableQuery.GenerateFilterCondition("Guid", QueryComparisons.Equal, idFood));
            TableContinuationToken tableContinuationToken = new TableContinuationToken();
            var result = cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
            var entity = result.Result.FirstOrDefault().FoodModelData;
            return entity;
        }
        public async Task<FoodWithDayModel> GetOneFoodWithDay(string idFood, int day)
        {
            var item = await GetOneFood(idFood);
            return new FoodWithDayModel
            {
                Guid = item.Guid,
                Fat = item.Fat,
                Carbohydrates = item.Carbohydrates,
                Prepare = item.Prepare,
                Portions = item.Portions,
                ImageUrl = item.ImageUrl,
                Type = item.Type,
                Kind = item.Kind,
                Calories = item.Calories,
                Url = item.Url,
                Ingredients = item.Ingredients,
                Name = item.Name,
                Day = day

            };
        }
    }
}
