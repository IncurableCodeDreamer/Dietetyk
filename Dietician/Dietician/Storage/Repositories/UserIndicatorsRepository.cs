using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.Interfaces;
using Dietician.Storage.StorageModels;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.Repositories
{
    public class UserIndicatorsRepository: IUserIndicatorsRepository
    {
        
            private readonly TableStorage _tableStorage;
            private readonly string _userIndicatorsTable;

            public UserIndicatorsRepository(IAppConfiguration configuration)
            {
                _tableStorage = new TableStorage(configuration);
                _userIndicatorsTable = configuration.GetVariable("UserIndicatorsTable");
            }
            public async Task InsertUserIndicatorsIntoTable(UserIndicatorModel model)
            {
                var table = await _tableStorage.GetTableReference(_userIndicatorsTable);
                var userIndicatorsEntity = new UserIndicatorsEntity()
                {
                    PartitionKey = model.IdUser.ToString(),
                    RowKey = model.IdIndicators.ToString(),
                    UserIndicatorsModelData = model

                };

                var tableOperation = TableOperation.InsertOrMerge(userIndicatorsEntity);
                await table.ExecuteAsync(tableOperation);

            }

            public async Task<UserIndicatorsEntity> GetUserIndicatorFromTableFromIdUser(string idUser)
            {
                var cloudTable = await _tableStorage.GetTableReference(_userIndicatorsTable);
                TableQuery<UserIndicatorsEntity> query = new TableQuery<UserIndicatorsEntity>()
                    .Where(TableQuery.GenerateFilterCondition("IdUser", QueryComparisons.Equal, idUser));
                TableContinuationToken tableContinuationToken = new TableContinuationToken();
                var result = cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                UserIndicatorsEntity userEntity = result.Result.FirstOrDefault();
                return userEntity;
            }
            public async Task<UserIndicatorsEntity> GetUserIndicatorFromTableFromIdIndicator(string idIndicator)
            {
                var cloudTable = await _tableStorage.GetTableReference(_userIndicatorsTable);
                TableQuery<UserIndicatorsEntity> query = new TableQuery<UserIndicatorsEntity>()
                    .Where(TableQuery.GenerateFilterCondition("IdIndicator", QueryComparisons.Equal, idIndicator));
                TableContinuationToken tableContinuationToken = new TableContinuationToken();
                var result = cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                UserIndicatorsEntity userEntity = result.Result.FirstOrDefault();
                return userEntity;
            }
    }
    }
