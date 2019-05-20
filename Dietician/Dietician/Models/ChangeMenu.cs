using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Dietician.Models
{
    public class ChangeMenu
    {
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Thursday { get; set; }
        public bool Wednesday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}
