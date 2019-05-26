using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.Interfaces;
using Dietician.Storage.StorageModels;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly TableStorage _tableStorage;
        private readonly string _mealsTable;

        public MealRepository(IAppConfiguration configuration)
        {
            _tableStorage = new TableStorage(configuration);
            _mealsTable = configuration.GetVariable("MealTable");
        }
        public async Task InsertMealIntoTable(MealModel model)
        {
            var table = await _tableStorage.GetTableReference(_mealsTable);
            var entity = new MealEntity()
            {
                PartitionKey = Guid.NewGuid().ToString(),
                RowKey = new Guid().ToString(),
                MealsModelData = model
            };

            var tableOperation = TableOperation.InsertOrMerge(entity);
            await table.ExecuteAsync(tableOperation);
        }

        public async Task<List<MealModel>> GetIMealFromTable(string idMeal)
        {
            var cloudTable = await _tableStorage.GetTableReference(_mealsTable);
            TableQuery<MealEntity> query = new TableQuery<MealEntity>()
                .Where(TableQuery.GenerateFilterCondition("IdUser", QueryComparisons.Equal, idMeal));
            TableContinuationToken tableContinuationToken = new TableContinuationToken();

            List<MealModel> result = new List<MealModel>();
            do
            {
                var segmentedResult = await cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = segmentedResult.ContinuationToken;
                result.AddRange(segmentedResult.Results.Select(s => s.MealsModelData));
            } while (tableContinuationToken != null);
            return result;
        }

        public async Task<List<MealModel>> GetMealToOneDayFromTableAsync(string idMeal, string day, string variant)
        {
            var table = await _tableStorage.GetTableReference(_mealsTable);
            var idUserFilter = TableQuery.GenerateFilterCondition("IdUser", QueryComparisons.Equal, idMeal);
            var ingFilter = TableQuery.GenerateFilterConditionForInt("MealNumber", QueryComparisons.Equal, int.Parse(day));
            var varFilter = TableQuery.GenerateFilterConditionForInt("Variant", QueryComparisons.Equal, int.Parse(variant));
            var filter = TableQuery.CombineFilters(idUserFilter, TableOperators.And, ingFilter);
            var filterSecond = TableQuery.CombineFilters(filter, TableOperators.And, varFilter);
            var query = new TableQuery<MealEntity>().Where(filterSecond);
            TableContinuationToken tableContinuationToken = null;
            var result= new List <MealModel>();
            do
            {
                var segmentedResult = await table.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = segmentedResult.ContinuationToken;
                result.AddRange(segmentedResult.Results.Select(s => s.MealsModelData));
            } while (tableContinuationToken != null);
            return result;
        }    

        public async Task<List<MealModel>> GetMealToOneVariantFromTableAsync(string idMeal, string variant)
        {
           var table = await _tableStorage.GetTableReference(_mealsTable);
            var idUserFilter = TableQuery.GenerateFilterCondition("IdUser", QueryComparisons.Equal, idMeal);
            var varFilter = TableQuery.GenerateFilterConditionForInt("Variant", QueryComparisons.Equal, int.Parse(variant));
            var filterSecond = TableQuery.CombineFilters(idUserFilter, TableOperators.And, varFilter);
            var query = new TableQuery<MealEntity>().Where(filterSecond);
            TableContinuationToken tableContinuationToken = null;
            var result= new List <MealModel>();
            do
            {
                var segmentedResult = await table.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = segmentedResult.ContinuationToken;
                result.AddRange(segmentedResult.Results.Select(s => s.MealsModelData));
            } while (tableContinuationToken != null);
            return result;
        }

        public async Task RemoveMealToOneDayFromTable(string idMeal, string day, string variant)
        {
            var table = await _tableStorage.GetTableReference(_mealsTable);
            var idUserFilter = TableQuery.GenerateFilterCondition("IdUser", QueryComparisons.Equal, idMeal);
            var ingFilter = TableQuery.GenerateFilterConditionForInt("MealNumber", QueryComparisons.Equal, int.Parse(day));
            var varFilter = TableQuery.GenerateFilterConditionForInt("Variant", QueryComparisons.Equal, int.Parse(variant));
            var filter = TableQuery.CombineFilters(idUserFilter, TableOperators.And, ingFilter);
            var filterSecond = TableQuery.CombineFilters(filter, TableOperators.And, varFilter);
            var query = new TableQuery<MealEntity>().Where(filterSecond);
            TableContinuationToken tableContinuationToken = null;
            var result = new List<MealEntity>();
            do
            {
                var segmentedResult = await table.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = segmentedResult.ContinuationToken;
                result.AddRange(segmentedResult);
            } while (tableContinuationToken != null);
            foreach (var item in result)
            {
                await table.ExecuteAsync(TableOperation.Delete(item));
            }
        }

        public async Task RemoveMealToOneVariantFromTableAsync(string idMeal, string variant)
        {
            var table = await _tableStorage.GetTableReference(_mealsTable);
            var idUserFilter = TableQuery.GenerateFilterCondition("IdUser", QueryComparisons.Equal, idMeal);
            var varFilter = TableQuery.GenerateFilterConditionForInt("Variant", QueryComparisons.Equal, int.Parse(variant));
            var filter = TableQuery.CombineFilters(idUserFilter, TableOperators.And, varFilter);
            var query = new TableQuery<MealEntity>().Where(filter);
            TableContinuationToken tableContinuationToken = null;
            var result = new List<MealEntity>();
            do
        {
            var segmentedResult = await table.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
            tableContinuationToken = segmentedResult.ContinuationToken;
                result.AddRange(segmentedResult);
        } while (tableContinuationToken != null);
            foreach (var item in result)
            {
                await table.ExecuteAsync(TableOperation.Delete(item));
            }
        }

        public async Task<string> GetIdOfMEalIfExist(string mealName, string idUser)
        {
            var table = await _tableStorage.GetTableReference(_mealsTable);
            var idUserFilter = TableQuery.GenerateFilterCondition("IdUser", QueryComparisons.Equal, idUser);
            var varFilter = TableQuery.GenerateFilterCondition("VariantName", QueryComparisons.Equal, mealName);
            var filter = TableQuery.CombineFilters(idUserFilter, TableOperators.And, varFilter);
            var query = new TableQuery<MealEntity>().Where(filter);
            TableContinuationToken tableContinuationToken = null;
            var result = new List<MealEntity>();
            do
            {
                var segmentedResult = await table.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = segmentedResult.ContinuationToken;
                result.AddRange(segmentedResult);
            } while (tableContinuationToken != null);

            if (result.Count == 0)
            {
                return null;
            }
            else
            {
                var variantId = result.Select(x => x.MealsModelData.Variant).First();
                return variantId.ToString();
            }
            
        }

        public async Task<int> GetMaxVariantId(string idUser)
        {
            var cloudTable = await _tableStorage.GetTableReference(_mealsTable);
            TableQuery<MealEntity> query = new TableQuery<MealEntity>()
                .Where(TableQuery.GenerateFilterCondition("IdUser", QueryComparisons.Equal, idUser));
            TableContinuationToken tableContinuationToken = new TableContinuationToken();

            List<MealModel> result = new List<MealModel>();
            do
            {
                var segmentedResult = await cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = segmentedResult.ContinuationToken;
                result.AddRange(segmentedResult.Results.Select(s => s.MealsModelData));
            } while (tableContinuationToken != null);

            var maxId = result.Max(x => x.Variant);
            return maxId;
        }

        public async Task<List<MealModel>> GetMealsFromTable(string idUser, string variantName)
        {
            var cloudTable = await _tableStorage.GetTableReference(_mealsTable);
            var idUserFilter = TableQuery.GenerateFilterCondition("IdUser", QueryComparisons.Equal, idUser);
            var varFilter = TableQuery.GenerateFilterCondition("VariantName", QueryComparisons.Equal, variantName);
            var filter = TableQuery.CombineFilters(idUserFilter, TableOperators.And, varFilter);
            var query = new TableQuery<MealEntity>().Where(filter);
            TableContinuationToken tableContinuationToken = new TableContinuationToken();

            List<MealModel> result = new List<MealModel>();
            do
            {
                var segmentedResult = await cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = segmentedResult.ContinuationToken;
                result.AddRange(segmentedResult.Results.Select(s => s.MealsModelData));
            } while (tableContinuationToken != null);

            return result;
        }

        public async Task<List<string>> GetAllVariantsName(string idUser)
        {
            var cloudTable = await _tableStorage.GetTableReference(_mealsTable);
            TableQuery<MealEntity> query = new TableQuery<MealEntity>()
                .Where(TableQuery.GenerateFilterCondition("IdUser", QueryComparisons.Equal, idUser));
            TableContinuationToken tableContinuationToken = new TableContinuationToken();

            List<MealModel> result = new List<MealModel>();
            do
            {
                var segmentedResult = await cloudTable.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = segmentedResult.ContinuationToken;
                result.AddRange(segmentedResult.Results.Select(s => s.MealsModelData));
            } while (tableContinuationToken != null);

            var variantsName = result.Select(x => x.VariantName).Distinct().ToList();

            return variantsName;
        }

    }
}
    
