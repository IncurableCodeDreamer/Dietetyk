using Microsoft.AspNetCore.Identity;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.Identity
{
    public class Result : IdentityResult
    {
        public new bool Success { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
        public new bool Succeeded
        {
            get
            {
                return Success;
            }
        }

        public Result()
        {

        }

        public Result(TableResult tableResult)
        {

        }
    }
}
