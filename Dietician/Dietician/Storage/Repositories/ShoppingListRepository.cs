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
    public class ShoppingListRepository:IShoppingListRepository
    {
        private readonly TableStorage _tableStorage;
        private readonly string _shopTable;

        public ShoppingListRepository(IAppConfiguration configuration)
        {
            _tableStorage = new TableStorage(configuration);
            _shopTable = configuration.GetVariable("shoppingTable");
        }
        public async Task InsertFoodIntoTable(ShoppingListModel model)
        {
            var table = await _tableStorage.GetTableReference(_shopTable);
            var entity = new ShoppingListEntity()
            {
                PartitionKey = Guid.NewGuid().ToString(),
                RowKey = new Guid().ToString(),
                ShopModelData = model
            };

            var tableOperation = TableOperation.InsertOrMerge(entity);
            await table.ExecuteAsync(tableOperation);

        }

        public async Task<List<ShoppingListEntity>> GetAllFoodsFromTable(string idUser)
        {
            var cloudTable = await _tableStorage.GetTableReference(_shopTable);
            TableQuery<ShoppingListEntity> query = new TableQuery<ShoppingListEntity>()
                .Where(TableQuery.GenerateFilterCondition("UserId", QueryComparisons.Equal, idUser));
            TableContinuationToken tableContinuationToken = new TableContinuationToken();
            var result = await cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
            List<ShoppingListEntity> entity = new List<ShoppingListEntity>();
            do
            {
                var segmentedResult = await cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = segmentedResult.ContinuationToken;
                entity.AddRange(segmentedResult.Results);
            } while (tableContinuationToken != null);
            return entity;
        }
        public async Task RemoveFood(ShoppingListModel model)
        {
            var table = await _tableStorage.GetTableReference(_shopTable);
            var idUserFilter = TableQuery.GenerateFilterCondition("UserId", QueryComparisons.Equal, model.UserId);
            var ingFilter = TableQuery.GenerateFilterCondition("Ingredient", QueryComparisons.Equal, model.Ingredient);
            var filter = TableQuery.CombineFilters(idUserFilter, TableOperators.And, ingFilter);
            var query = new TableQuery<ShoppingListEntity>().Where(filter);
            TableContinuationToken tableContinuationToken = null;
            ShoppingListEntity result;
            do
            {
                var segmentedResult = await table.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = segmentedResult.ContinuationToken;
                result = segmentedResult.Results.FirstOrDefault();
            } while (tableContinuationToken != null);
            await table.ExecuteAsync(TableOperation.Delete(result));
        }
    }
}
    
