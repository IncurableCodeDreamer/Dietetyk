using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Models
{
    public class Registration
    {
        [Required, MinLength(4), MaxLength(20), Display(Name = "Login")]
        public string Login { get; set; }

        [Required, MinLength(4), MaxLength(20), DataType(DataType.Password), Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required, MinLength(8), MaxLength(20), DataType(DataType.Password), Display(Name = "Potwierdź hasło")]
        [Compare("Password",ErrorMessage ="Podane hasło jest nieprawidłowe.")]
        public string ConfirmPassword { get; set; }

        /*public PersonalDataSettings PersonalData { get; set; }

        public MenuSettings Menu { get; set; }

        public ActivitySettings Activity { get; set; }*/
    }
}
