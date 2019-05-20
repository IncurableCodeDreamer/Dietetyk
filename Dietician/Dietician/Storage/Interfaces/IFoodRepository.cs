using Dietician.Storage.StorageModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dietician.Storage.Interfaces
{
    public interface IFoodRepository
    {
        Task InsertFoodIntoTable(FoodModel model);
        Task<List<FoodModel>> GetAllFoodsFromTable();
        Task<FoodModel> GetOneFood(string idFood);
        }
}
