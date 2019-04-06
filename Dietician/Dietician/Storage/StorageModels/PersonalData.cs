using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Models
{
    public class PersonalData:PersonalDataSettings
    {
        public int PersonId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
