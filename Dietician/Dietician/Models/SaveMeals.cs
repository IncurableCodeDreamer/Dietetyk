using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Models
{
    public class SaveMeals
    {
        [Required(ErrorMessage = "Pole jest wymagane")]
        [MaxLength(20,ErrorMessage ="Maksymalna długość nazwy to 20 znaków")]
        public string Name { get; set; }
    }
}
