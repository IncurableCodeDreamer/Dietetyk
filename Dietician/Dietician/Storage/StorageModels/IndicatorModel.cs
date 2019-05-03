using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Storage.StorageModels
{
    public class IndicatorModel
    {
        public int IdIndicators { get; set; }
        public  int Height { get; set; }
        public int Weight { get; set; }
        public DateTime ChangeDate { get; set; }
        public IndicatorModel(int indicatorsId, int height, int weight, DateTime date)
        {
            this.IdIndicators = indicatorsId;
            this.Height = height;
            this.Weight = weight;
            this.ChangeDate = date;
        }
    }
}

