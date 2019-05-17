using Dietician.Storage.Entities;
using Dietician.Storage.StorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.Interfaces
{
    public interface IFoodRepository
    {
        Task InsertFoodIntoTable(FoodModel model);       
        Task<List<FoodModel>> GetAllFoodsFromTable();
    }
}
