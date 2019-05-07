using Dietician.Enums;
using System.ComponentModel.DataAnnotations;

namespace Dietician.Models
{
    public class FatLevel
    {
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        public Lifestyle LifeStyle { get; set; }
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Range(1, 1200,
        ErrorMessage = "Wartość musi być powyżej 0")]
        public int WaistSize { get; set; }
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Range(1, 1200,
        ErrorMessage = "Wartość musi być powyżej 0")]
        public int HipSize { get; set; }
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Range(1, 1200,
          ErrorMessage = "Wartość musi być powyżej 0")]
        public int TricepsFold { get; set; }
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Range(1, 1200,
       ErrorMessage = "Wartość musi być powyżej 0")]
        public int HipFold { get; set; }
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Range(1, 1200,
        ErrorMessage = "Wartość musi być powyżej 0")]
        public int ThighFold { get; set; }
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Range(1, 1200,
         ErrorMessage = "Wartość musi być powyżej 0")]
        public int ChestFold { get; set; }
        [Required(ErrorMessage = "Pole jest obowiązkowe")]
        [Range(1, 1200,
         ErrorMessage = "Wartość musi być powyżej 0")]
        public int BellyFold { get; set; }
    }
}