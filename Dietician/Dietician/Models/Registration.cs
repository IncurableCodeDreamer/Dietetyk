using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Models
{
    public class Registration
    {
        [Required(ErrorMessage ="Pole jest wymagane"), 
            MinLength(4, ErrorMessage = "Login musi składać się z min. 4 znaków"), 
            MaxLength(20, ErrorMessage = "Login musi składać się z max. 20 znaków"), Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane"), 
            RegularExpression("^(?=.*?[A-Z])(?=(.*[a-z]){1,})(?=(.*[\\d]){1,})(?=(.*[\\W]){1,})(?!.*\\s).{8,}$",
            ErrorMessage = "Hasło powinno składać się z min. 8 znaków (w tym 1 dużej litery, 1 cyfry i 1 znaku specjalnego"),
            DataType(DataType.Password), Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane"), DataType(DataType.Password), Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage ="Podane hasło jest nieprawidłowe.")]
        public string ConfirmPassword { get; set; }

        /*public PersonalDataSettings PersonalData { get; set; }

        public MenuSettings Menu { get; set; }

        public ActivitySettings Activity { get; set; }*/
    }
}
