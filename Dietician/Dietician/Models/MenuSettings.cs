using Dietician.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Models
{
    public class MenuSettings
    {
        public DietAim DietAim { get; set; }
        public KindsOfDiet Preferences { get; set; }
        public int MealCount { get; set; }
    }
}
