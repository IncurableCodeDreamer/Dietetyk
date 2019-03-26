using Dietician.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Models
{
    public class PersonalDataSettings
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public Gender Gender { get; set; }
    }
}
