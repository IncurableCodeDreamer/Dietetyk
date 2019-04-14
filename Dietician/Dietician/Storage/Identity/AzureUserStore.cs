using Dietician.Storage.Identity;
using Dietician.Storage.StorageModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Dietician.Storage
{
    public class AzureUserStore: IUserStore<UserEntity>, IUserPasswordStore<UserEntity>
    {
        private readonly CloudTable cloudTable;

        public AzureUserStore()
        {
            CloudStorageAccount csa = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=dietdevelop;AccountKey=8i5kPct+ng1gUz6qmCbFeL7j2U1/i6CyuRf+nsJTlLckrHyypoMl3/eWjHGnvK+DjNdRdVEv1MsZmFzNd4JmLg==;EndpointSuffix=core.windows.net");
            cloudTable = csa.CreateCloudTableClient().GetTableReference("userTable");
            cloudTable.CreateIfNotExistsAsync();
        }

        #region UserStore

        public void Dispose()
        {
        }

        public Task<TableResult> CreateAsync(UserEntity user)
        {
            TableOperation op = TableOperation.InsertOrMerge(user);
            var result = cloudTable.ExecuteAsync(op);
            return result;
        }

        public Task<TableResult> DeleteAsync(UserEntity user)
        {
            TableOperation op = TableOperation.Delete(user);
            var result = cloudTable.ExecuteAsync(op);
            return result;
        }

        public Task<TableResult> UpdateAsync(UserEntity user)
        {
            TableOperation op = TableOperation.Replace(user);
            var result = cloudTable.ExecuteAsync(op);
            return result;
        }

        public Task<IdentityResult> CreateAsync(UserEntity user, CancellationToken cancellationToken)
        {
            var result = CreateAsync(user);
            IdentityResult identityResult = new Result { Success = true, ErrorMessages = null };

            return Task.FromResult(identityResult);
        }

        public Task<IdentityResult> DeleteAsync(UserEntity user, CancellationToken cancellationToken)
        {

            var result = DeleteAsync(user);
            IdentityResult identityResult = new Result { Success = true, ErrorMessages = null };

            return Task.FromResult(identityResult);
        }

        public Task<IdentityResult> UpdateAsync(UserEntity user, CancellationToken cancellationToken)
        {
            var result = UpdateAsync(user);
            IdentityResult identityResult = new Result { Success = true, ErrorMessages = null };

            return Task.FromResult(identityResult);
        }

        public Task<UserEntity> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            TableOperation op = TableOperation.Retrieve<AzureUser>("personId", userId);
            var result = cloudTable.ExecuteAsync(op);
            UserEntity userEntity = result.Result.Result as UserEntity;
            return Task.FromResult(userEntity);
        }

        public async Task<UserEntity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var loginFilter = TableQuery.GenerateFilterCondition("UserName", QueryComparisons.Equal, normalizedUserName);
            var query = new TableQuery<UserEntity>().Where(loginFilter);
            TableContinuationToken tableContinuationToken = null;
            UserEntity userEntity;
            do
            {
                var segmentedResult = await cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = segmentedResult.ContinuationToken;
                userEntity = segmentedResult.Results.FirstOrDefault();
            } while (tableContinuationToken != null);

            return await Task.FromResult(userEntity);
        }

        public Task<string> GetNormalizedUserNameAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<string> GetPasswordHashAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(UserEntity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id != null );
        }

        public Task SetNormalizedUserNameAsync(UserEntity user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetPasswordHashAsync(UserEntity user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(UserEntity user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        #endregion
    }
}
