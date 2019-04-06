using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Models;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage
{
    public class UserRepository:IUserRepository
    { 
        private readonly TableStorage _tableStorage;
        private readonly string _userTable;
        public UserRepository(IAppConfiguration configuration)
        {
            _tableStorage = new TableStorage(configuration);
            _userTable = configuration.GetVariable("UserTable");
        }
        public async Task InsertUserIntoTable(PersonalData user)
        {
            var table = await _tableStorage.GetTableReference(_userTable);

            var userEntity = "";
                
              //  var tableOperation = TableOperation.InsertOrMerge(userEntity);
              //  await table.ExecuteAsync(tableOperation);
            }
        }

    
}  

