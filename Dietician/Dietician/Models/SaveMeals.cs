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
        public string Name { get; set; }
    }
}
