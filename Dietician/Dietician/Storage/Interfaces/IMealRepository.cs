using System.Collections.Generic;
using System.Threading.Tasks;
using Dietician.Storage.StorageModels;

namespace Dietician.Storage.Interfaces
{
    public interface IMealRepository
    {
        Task InsertMealIntoTable(MealModel model);
        Task<List<MealModel>> GetIMealFromTable(string idMeal);
    }
}
