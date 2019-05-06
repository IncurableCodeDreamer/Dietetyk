using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage;
using Dietician.Storage.StorageModels;
using Microsoft.AspNetCore.Mvc;

namespace Dietician.Controllers
{
    public class BaseController : Controller
    {
        public UserEntity GetLoggedUser(IUserRepository _repository)
        {
            UserEntity user = null;

            if (User.Identity.IsAuthenticated)
            {
                var name = HttpContext.User.Identity.Name.ToUpper();
                user = _repository.GetUserFromTable(name).Result;
            }

            return user;
        }
    }
}
