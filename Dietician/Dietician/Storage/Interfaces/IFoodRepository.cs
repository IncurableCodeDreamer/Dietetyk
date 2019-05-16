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
        // to do poprawy-juz zrobie jak dostane laptopa
        Task<List<FoodEntity>> GetAllFoodsFromTable(string idUser);
    }
}
