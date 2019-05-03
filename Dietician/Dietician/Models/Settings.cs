using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Models
{
    public class Settings
    {
        public PersonalDataSettings PersonalData { get; set; }
        public MenuSettings Menu { get; set; }
        public ActivitySettings Activity { get; set; }
        public IngridientsSettings Ingridients { get; set; }
    }
}
