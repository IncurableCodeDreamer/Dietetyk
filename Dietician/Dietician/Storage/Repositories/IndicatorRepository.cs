using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.Interfaces;
using Dietician.Storage.StorageModels;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.Repositories
{
    public class IndicatorRepository:IIndicatorRepository
    {
        private readonly TableStorage _tableStorage;
        private readonly string _indicatorsTable;

        public IndicatorRepository(IAppConfiguration configuration)
        {
            _tableStorage = new TableStorage(configuration);
            _indicatorsTable = configuration.GetVariable("IndicatorsTable");
        }
        public async Task InsertIndicatorsIntoTable(IndicatorModel model)
        {
            var table = await _tableStorage.GetTableReference(_indicatorsTable);
            var entity = new IndicatorEntity()
            {
                PartitionKey = Guid.NewGuid().ToString(),
                RowKey = new Guid().ToString(),
                IndicatorsModelData = model
            };

            var tableOperation = TableOperation.InsertOrMerge(entity);
            await table.ExecuteAsync(tableOperation);

        }

        public async Task<List<IndicatorEntity>> GetIndicatorsFromTable(string idUser)
        {
            var cloudTable = await _tableStorage.GetTableReference(_indicatorsTable);
            TableQuery<IndicatorEntity> query = new TableQuery<IndicatorEntity>()
                .Where(TableQuery.GenerateFilterCondition("IdUser", QueryComparisons.Equal, idUser));
            TableContinuationToken tableContinuationToken = new TableContinuationToken();
            var result = await cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
            List<IndicatorEntity> entity = result.Results.ToList();
            return entity;
        }

        public async Task<IndicatorEntity> GetLastIndicatorFromTable(string idUser)
        {
            var cloudTable = await _tableStorage.GetTableReference(_indicatorsTable);
            TableQuery<IndicatorEntity> query = new TableQuery<IndicatorEntity>()
                .Where(TableQuery.GenerateFilterCondition("IdUser", QueryComparisons.Equal, idUser));
            TableContinuationToken tableContinuationToken = new TableContinuationToken();
            var result = await cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
            List<IndicatorEntity> entityList = result.Results.ToList();
            IndicatorEntity lastIndicators = entityList.OrderByDescending(x => x.IndicatorsModelData.ChangeDate).FirstOrDefault();
            return lastIndicators;
        }
    }
}
