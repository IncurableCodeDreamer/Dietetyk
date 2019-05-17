using Dietician.Storage.Entities;
using Dietician.Storage.StorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.Interfaces
{
    interface IShoppingListRepository
    {
        Task<List<ShoppingListEntity>> GetAllFoodsFromTable(string idUser);
        Task RemoveFood(ShoppingListModel model);
        Task InsertFoodIntoTable(ShoppingListModel model);
    }
}

