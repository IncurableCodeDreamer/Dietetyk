using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Pole jest wymagane"), Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane"), DataType(DataType.Password), Display(Name = "Hasło")]
        public string Password { get; set; }

    }
}
