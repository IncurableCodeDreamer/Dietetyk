﻿using System.Collections.Generic;

namespace Dietician.Models
{
    public class Parameters
    {
        public PersonalDataSettings PresonalData { get; set; }
        public FatLevel FatLevel { get; set; }     
        public CheckboxResult Params { get; set; }
    }
}
