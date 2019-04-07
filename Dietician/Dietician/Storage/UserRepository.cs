using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Models;
using Dietician.Storage.StorageModels;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage
{
    public class UserRepository:IUserRepository
    { 
        private readonly TableStorage _tableStorage;
        private readonly string _userTable;
        public UserRepository(IAppConfiguration configuration)
        {
            _tableStorage = new TableStorage(configuration);
            _userTable = configuration.GetVariable("UserTable");
        }
        public async Task InsertUserIntoTable(PersonalData user)
        {
            var table = await _tableStorage.GetTableReference(_userTable);
            var userEntity = new UserEntity()
            {
                PartitionKey = user.Login,
                RowKey = user.PersonId.ToString(),
                UserModelData = user
            };

            var tableOperation = TableOperation.InsertOrMerge(userEntity);
            await table.ExecuteAsync(tableOperation);

        }

        public async Task<bool> CheckIfUserExist(string login)
        {
            var table = await _tableStorage.GetTableReference(_userTable);
            var loginFilter = TableQuery.GenerateFilterCondition("Login", QueryComparisons.Equal, login);
            var query = new TableQuery<UserEntity>().Where(loginFilter);
            TableContinuationToken tableContinuationToken = null;
            UserEntity result;
            do
            {
                var segmentedResult = await table.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = segmentedResult.ContinuationToken;
                result=segmentedResult.Results.FirstOrDefault();
            } while (tableContinuationToken != null);

            return result != null;
        }

        public async Task<bool> VerifyPassword(string login, string password)
        {
            var table = await _tableStorage.GetTableReference(_userTable);
            var loginFilter = TableQuery.GenerateFilterCondition("Login", QueryComparisons.Equal, login);
            var passwordFilter = TableQuery.GenerateFilterCondition("Password", QueryComparisons.Equal, password);
            var filter = TableQuery.CombineFilters(loginFilter, TableOperators.And, passwordFilter);
            var query = new TableQuery<UserEntity>().Where(filter);
            TableContinuationToken tableContinuationToken = null;
            UserEntity result;
            do
            {
                var segmentedResult = await table.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = segmentedResult.ContinuationToken;
                result = segmentedResult.Results.FirstOrDefault();
            } while (tableContinuationToken != null);

            return result != null;
        }
    }
}

    


