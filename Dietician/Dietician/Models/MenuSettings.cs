using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Models
{
    public class MenuSettings
    {
        public string DietAim { get; set; }
        public string Preferences { get; set; }
        public List<string> Ingidients { get; set; }
    }
}
