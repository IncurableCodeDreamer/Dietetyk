using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Dietician.Models
{
    public class ChangeMenu
    {
        public DayOfWeek DayOfWeek { get; set; }
        public bool AllDay { get; set; }
        public List<SelectListItem> MealTypes { get; set; }
    }
}
