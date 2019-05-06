using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician.Enums
{
    public enum MealType
    {
        [Display(Name ="Śniadanie")]
        Sniadanie,
        [Display(Name = "Drugie śniadanie")]
        Sniadanie2,
        [Display(Name = "Obiad")]
        Obiad,
        [Display(Name = "Podwieczorek")]
        Podwieczorek,
        [Display(Name = "Kolacja")]
        Kolacja
    }
}
