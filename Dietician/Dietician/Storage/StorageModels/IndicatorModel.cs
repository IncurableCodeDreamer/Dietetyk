using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.StorageModels
{
    public class IndicatorModel
    {
        public int IdIndicators { get; set; }
        public int? Height { get; set; }
        public double? Weight { get; set; }
        public string IdUser { get; set; }
        public DateTime ChangeDate { get; set; }

        public IndicatorModel()
        {

        }
        
        public IndicatorModel(string idUser, int indicatorsId, int? height, double? weight, DateTime date)
        {
            this.IdUser = idUser;
            this.IdIndicators = indicatorsId;
            this.Height = height;
            this.Weight = weight;
            this.ChangeDate = date;
        }
    }
}

