using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.StorageModels
{
    public class UserIndicatorModel
    {
        public int IdUser { get; set; }
        public int IdIndicators { get; set; }

        public UserIndicatorModel(int userId, int indicatorId)
        {
            this.IdIndicators = indicatorId;
            this.IdUser = userId;
        }
    }
}
