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
        // to do poprawy-juz zrobie jak dostane laptopa
        public async Task<List<FoodEntity>> GetAllFoodsFromTable(string idUser)
        {
            var cloudTable = await _tableStorage.GetTableReference(_foodTable);
            TableQuery<FoodEntity> query = new TableQuery<FoodEntity>()
                .Where(TableQuery.GenerateFilterCondition("IdUser", QueryComparisons.Equal, idUser));
            TableContinuationToken tableContinuationToken = new TableContinuationToken();
            var result = await cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
            List<FoodEntity> entity = result.Results.ToList();
            return entity;
        }
    }
}
