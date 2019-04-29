using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.StorageModels;

namespace Dietician.Storage.Interfaces
{
    interface IUserMealRepository
    {
        Task InsertUserMealIntoTable(UserMealModel model);
        Task<UserMealEntity> GetUserMealFromTableFromMealID(string idMeal);
        Task<UserMealEntity> GetUserMealFromTableFromUserID(string idUser);
    }
}
