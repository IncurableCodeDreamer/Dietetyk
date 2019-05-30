using Dietician.Enums;
using System.ComponentModel.DataAnnotations;

namespace Dietician.Models
{
    public class AddMeal
    {
        public string ImageUrl { get; set; }

        [Required(ErrorMessage ="Pole jest wymagane")]
        public string Name { get; set; }
        public string Prepare { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        public string Ingredients { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        public KindsOfDiet Kind { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane")]
        public MealType Type { get; set; }

        [Range(1, double.PositiveInfinity, ErrorMessage = "Wartość musi być większa od 0")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public int Portions { get; set; }

        [Range(1, double.PositiveInfinity, ErrorMessage = "Wartość musi być większa od 0")]
        [Required(ErrorMessage = "Pole jest wymagane")]
        public int Calories { get; set; }

        [Range(1, double.PositiveInfinity, ErrorMessage = "Wartość musi być większa od 0")]
        public int Carbohydrates { get; set; }

        [Range(1, double.PositiveInfinity, ErrorMessage = "Wartość musi być większa od 0")]
        public int Proteins { get; set; }

        [Range(1, double.PositiveInfinity, ErrorMessage = "Wartość musi być większa od 0")]
        public int Fat { get; set; }

        public string Url { get; set; }
        
    }
}
