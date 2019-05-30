using Dietician.Storage.Entities;
using Dietician.Storage.StorageModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dietician.Storage.Interfaces
{
    public interface IShoppingListRepository
    {
        Task<List<ShoppingListEntity>> GetAllFoodsFromTable(string idUser);
        Task RemoveFood(ShoppingListModel model);
        Task InsertFoodIntoTable(ShoppingListModel model);
    }
}

