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
        public DietPreferences Preferences { get; set; }
        //public List<string> Ingidients { get; set; }
    }
}
