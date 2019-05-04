using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Enums
{
    public enum DayOfWeek
    {
        [Display(Name ="Poniedziałek")]
        Poniedziałek,
        [Display(Name = "Wtorek")]
        Wtorek,
        [Display(Name = "Środa")]
        Środa,
        [Display(Name = "Czwartek")]
        Czwartek,
        [Display(Name = "Piątek")]
        Piątek,
        [Display(Name = "Sobota")]
        Sobota,
        [Display(Name = "Niedziela")]
        Niedziela
    }
}
