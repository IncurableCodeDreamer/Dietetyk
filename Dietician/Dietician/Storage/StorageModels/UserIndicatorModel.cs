using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.StorageModels
{
    public class UserIndicatorModel
    {
        public string IdUser { get; set; }
        public string IdIndicators { get; set; }

        public UserIndicatorModel()
        {

        }

        public UserIndicatorModel(string userId, string indicatorId)
        {
            this.IdIndicators = indicatorId;
            this.IdUser = userId;
        }
    }
}
