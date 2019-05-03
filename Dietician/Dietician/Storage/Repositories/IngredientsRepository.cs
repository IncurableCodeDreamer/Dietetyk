using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.Interfaces;
using Dietician.Storage.StorageModels;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.Repositories
{
    public class IngredientsRepository:IIngredientsRepository
    {
        private readonly TableStorage _tableStorage;
        private readonly string _ingredientsTable;

        public IngredientsRepository(IAppConfiguration configuration)
        {
            _tableStorage = new TableStorage(configuration);
            _ingredientsTable = configuration.GetVariable("IngredientsTable");
        }
        public async Task<IngredientsModel> InsertIngredientsIntoTable(IngredientsModel model)
        {
            var table = await _tableStorage.GetTableReference(_ingredientsTable);
            model.IdIngredient = Guid.NewGuid().ToString();
            var entity = new IngredientEntity()
            {
                PartitionKey = model.IdIngredient,
                RowKey = new Guid().ToString(),
                IngredientsModelData = model
            };

            var tableOperation = TableOperation.InsertOrMerge(entity);
            await table.ExecuteAsync(tableOperation);
            return model;
        }

        public async Task UpdateIngridients(IngredientEntity model)
        {
            var cloudTable = await _tableStorage.GetTableReference(_ingredientsTable);
            TableOperation op = TableOperation.Replace(model);
            var result = cloudTable.ExecuteAsync(op);
        }

        public async Task<IngredientEntity> GetIIngredientsFromTable(string idIngredients)
        {
        var cloudTable = await _tableStorage.GetTableReference(_ingredientsTable);
        TableQuery<IngredientEntity> query = new TableQuery<IngredientEntity>()
        .Where(TableQuery.GenerateFilterCondition("IdIngredient", QueryComparisons.Equal, idIngredients));
        TableContinuationToken tableContinuationToken = new TableContinuationToken();
        var result = cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
        var entity = result.Result.FirstOrDefault();
        return entity;
        }
    }
}
