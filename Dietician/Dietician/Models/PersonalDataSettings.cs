using Dietician.Enums;
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
        public double? Weight { get; set; }
        public Lifestyle Lifestyle { get; set; }
    }
}
