using Dietician.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Dietician.Models
{
    public class PersonalDataSettings
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int? Age { get; set; }
        public int? Height { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public int? Weight { get; set; }
    }
}
