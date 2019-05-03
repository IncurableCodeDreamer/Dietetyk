using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Dietician.Enums
{
    public enum Lifestyle
    {
        [Display(Name ="Nieaktywny (brak ćwiczeń)")]
        Nieaktywny,
        [Display(Name = "Mało aktywny (lekkie ćwiczenia)")]
        MaloAktywny,
        [Display(Name = "Średnio aktywny (wysiłek 3–5 razy tygodniowo)")]
        SrednioAktywny,
        [Display(Name = "Aktywny (wysiłek 6–7 razy w tygodniu)")]
        Aktywny,
        [Display(Name = "Bardzo aktywny (ćwiczenia i praca fizyczna)")]
        BardzoAktywny
    }
}
