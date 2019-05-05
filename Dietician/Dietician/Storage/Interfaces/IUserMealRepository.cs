using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.StorageModels;

namespace Dietician.Storage.Interfaces
{
    public interface IUserMealRepository
    {
        Task InsertUserMealIntoTable(UserMealModel model);
        Task<UserMealEntity> GetUserMealFromTableFromUserID(string idUser);
    }
}
