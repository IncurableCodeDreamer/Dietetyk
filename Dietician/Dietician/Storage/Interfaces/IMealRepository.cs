using System.Collections.Generic;
using System.Threading.Tasks;
using Dietician.Storage.StorageModels;

namespace Dietician.Storage.Interfaces
{
    public interface IMealRepository
    {
        Task InsertMealIntoTable(MealModel model);
        Task<List<MealModel>> GetIMealFromTable(string idUser);
        Task<List<MealModel>> GetMealToOneDayFromTableAsync(string idUser, string day, string variant);
        Task<List<MealModel>> GetMealToOneVariantFromTableAsync(string idUser, string variant);
        Task RemoveMealToOneDayFromTable(string idUser, string day, string variant);
        Task RemoveMealToOneVariantFromTableAsync(string idUser, string variant);
        Task<string> GetIdOfMEalIfExist(string variantName, string idUser);
    }
}
