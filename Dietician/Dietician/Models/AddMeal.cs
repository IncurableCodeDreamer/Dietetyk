using Dietician.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        [Required(ErrorMessage = "Pole jest wymagane")]
        public int Portions { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        public int Calories { get; set; }
        public int Carbohydrates { get; set; }
        public int Proteins { get; set; }
        public int Fat { get; set; }
        public string Url { get; set; }
        
    }
}
