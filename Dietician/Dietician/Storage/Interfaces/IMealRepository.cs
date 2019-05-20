using System.Collections.Generic;
using System.Threading.Tasks;
using Dietician.Storage.StorageModels;

namespace Dietician.Storage.Interfaces
{
    public interface IMealRepository
    {
        Task InsertMealIntoTable(MealModel model);
        Task<List<MealModel>> GetIMealFromTable(string idMeal);
        Task<List<MealModel>> GetMealToOneDayFromTableAsync(string idMeal, string day, string variant);
        Task<List<MealModel>> GetMealToOneVariantFromTableAsync(string idMeal, string variant);
        Task RemoveMealToOneDayFromTable(string idMeal, string day, string variant);
        Task RemoveMealToOneVariantFromTableAsync(string idMeal, string variant);
    }
}
