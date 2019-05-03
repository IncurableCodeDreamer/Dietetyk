using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.Interfaces;
using Dietician.Storage.StorageModels;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.Repositories
{
    public class UserMealRepository: IUserMealRepository
    {
        private readonly TableStorage _tableStorage;
        private readonly string _userMealTable;

        public UserMealRepository(IAppConfiguration configuration)
        {
            _tableStorage = new TableStorage(configuration);
            _userMealTable = configuration.GetVariable("UserMealTable");
        }
        public async Task InsertUserMealIntoTable(UserMealModel model)
        {
            var table = await _tableStorage.GetTableReference(_userMealTable);
            var entity = new UserMealEntity()
            {
                PartitionKey = Guid.NewGuid().ToString(),
                RowKey = new Guid().ToString(),
                UserMealModelData = model
            };

            var tableOperation = TableOperation.InsertOrMerge(entity);
            await table.ExecuteAsync(tableOperation);

        }

        public async Task<UserMealEntity> GetUserMealFromTableFromMealID(string idMeal)
        {
            var cloudTable = await _tableStorage.GetTableReference(_userMealTable);
            TableQuery<UserMealEntity> query = new TableQuery<UserMealEntity>()
                .Where(TableQuery.GenerateFilterCondition("IdMeal", QueryComparisons.Equal, idMeal));
            TableContinuationToken tableContinuationToken = new TableContinuationToken();
            var result = cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
            var entity = result.Result.FirstOrDefault();
            return entity;
        }

        public async Task<UserMealEntity> GetUserMealFromTableFromUserID(string idUser)
        {
            var cloudTable = await _tableStorage.GetTableReference(_userMealTable);
            TableQuery<UserMealEntity> query = new TableQuery<UserMealEntity>()
                .Where(TableQuery.GenerateFilterCondition("IdIUser", QueryComparisons.Equal, idUser));
            TableContinuationToken tableContinuationToken = new TableContinuationToken();
            var result = cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
            var entity = result.Result.FirstOrDefault();
            return entity;
        }
    }
}
