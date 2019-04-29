using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.StorageModels;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.Repositories
{
    public class UserRepository: IUserRepository
    { 
        private readonly TableStorage _tableStorage;
        private readonly string _userTable;

        public UserRepository(IAppConfiguration configuration)
        {
            _tableStorage = new TableStorage(configuration);
            _userTable = configuration.GetVariable("UserTable");
        }
        public async Task InsertUserIntoTable(AzureUser user)
        {
            var table = await _tableStorage.GetTableReference(_userTable);
            var userEntity = new UserEntity()
            {
                PartitionKey = user.PartitionKey,
                RowKey = user.Id
            };

            var tableOperation = TableOperation.InsertOrMerge(userEntity);
            await table.ExecuteAsync(tableOperation);

        }

        public async Task<UserEntity> GetUserFromTable(string userName)
        {
            var cloudTable = await _tableStorage.GetTableReference(_userTable);
            TableQuery<UserEntity> query = new TableQuery<UserEntity>()
                .Where(TableQuery.GenerateFilterCondition("UserName", QueryComparisons.Equal, userName));
            TableContinuationToken tableContinuationToken = new TableContinuationToken();
            var result = cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
            UserEntity userEntity = result.Result.FirstOrDefault();
            return userEntity;
        }

        public async void UpdateUser(UserEntity user)
        {
            var cloudTable = await _tableStorage.GetTableReference(_userTable);
            TableOperation op = TableOperation.Replace(user);
            var result = cloudTable.ExecuteAsync(op);
        }

        public async Task<bool> CheckIfUserExist(string login)
        {
            var table = await _tableStorage.GetTableReference(_userTable);
            var loginFilter = TableQuery.GenerateFilterCondition("UserName", QueryComparisons.Equal, login);
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

    


