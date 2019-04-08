using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Models
{
    public class LoginModel
    {
        [Required, MinLength(4), MaxLength(20), Display(Name = "Login")]
        public string Login { get; set; }

        [Required, MinLength(4), MaxLength(20), DataType(DataType.Password), Display(Name = "Hasło")]
        public string Password { get; set; }

    }
}
