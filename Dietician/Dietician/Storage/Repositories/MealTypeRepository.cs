using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.Interfaces;
using Dietician.Storage.StorageModels;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.Repositories
{
    public class MealTypeRepository:IMealTypeRepository
    {
        private readonly TableStorage _tableStorage;
        private readonly string _userMealTypeTable;

        public MealTypeRepository(IAppConfiguration configuration)
        {
            _tableStorage = new TableStorage(configuration);
            _userMealTypeTable = configuration.GetVariable("MealTypeTable");
        }
        public async Task InsertMealTypeIntoTable(MealTypeModel model)
        {
            var table = await _tableStorage.GetTableReference(_userMealTypeTable);
            var entity = new MealTypeEntity()
            {
                PartitionKey = Guid.NewGuid().ToString(),
                RowKey = new Guid().ToString(),
                MealTypeModelData = model
            };

            var tableOperation = TableOperation.InsertOrMerge(entity);
            await table.ExecuteAsync(tableOperation);

        }

        public async Task<MealTypeEntity> GetMealTypeFromTable(string idMealType)
        {
            var cloudTable = await _tableStorage.GetTableReference(_userMealTypeTable);
            TableQuery<MealTypeEntity> query = new TableQuery<MealTypeEntity>()
                .Where(TableQuery.GenerateFilterCondition("IdMealType", QueryComparisons.Equal, idMealType));
            TableContinuationToken tableContinuationToken = new TableContinuationToken();
            var result = cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
            var entity = result.Result.FirstOrDefault();
            return entity;
        }
    }
}
