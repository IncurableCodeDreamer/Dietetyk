using Dietician.Enums;
using System.ComponentModel.DataAnnotations;

namespace Dietician.Models
{
    public class PersonalDataSettings
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Range(1, 120,
        ErrorMessage = "Wartość musi mieścić się od {1} do {2}.")]
        public int? Age { get; set; }
        [Range(1, 220,
        ErrorMessage = "Wartość musi mieścić się od {1} do {2}.")]
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        public int? Height { get; set; }
        public Gender Gender { get; set; }
        [Range(1, 120,
        ErrorMessage = "Wartość musi mieścić się od {1} do {2}.")]
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        public double? Weight { get; set; }
        public Lifestyle Lifestyle { get; set; }
    }
}
