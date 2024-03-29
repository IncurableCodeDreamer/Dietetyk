﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.StorageModels;

namespace Dietician.Storage
{
    public interface IUserRepository
    {
        Task InsertUserIntoTable(AzureUser user);
        Task<bool> CheckIfUserExist(string login);
        Task<bool> VerifyPassword(string login, string password);
        Task<UserEntity> GetUserFromTable(string userName);
        void UpdateUser(UserEntity user);
    }
}
