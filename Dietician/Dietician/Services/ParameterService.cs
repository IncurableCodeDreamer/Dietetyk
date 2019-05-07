using Dietician.Enums;
using Dietician.Models;
using System;

namespace Dietician.Services
{
    public static class ParameterService
    {
        public static double CalculateBMI(PersonalDataSettings person)
        {
            return person.Weight.Value/((person.Height.Value/(double)100)*(person.Height.Value/(double)100));       
        }

        public static string GetBMILabel(ParameterResults results)
        {
            if (results.Bmi < 18.5)
            {                
                return "niedowaga";
            }
            else if (results.Bmi >= 18.5 && results.Bmi <= 24.99)
            {
                return "waga prawidłowa";
            }
            else if (results.Bmi >= 25 && results.Bmi <= 29.99)
            {
                return "nadwaga";
            }
            else if (results.Bmi >= 30 && results.Bmi <= 39.99)
            {
                return "otyłość";
            }
            else
            {
                return "otyłość dużego stopnia";
            }
        }

        public static double onItemSelected(Lifestyle id)
        {
            switch (id)
            {
                case Lifestyle.Nieaktywny:
                    return 1.2;
                case Lifestyle.MaloAktywny:
                    return 1.4;
                case Lifestyle.SrednioAktywny:
                    return 1.6;
                case Lifestyle.Aktywny:
                    return 1.8;
                case Lifestyle.BardzoAktywny:
                    return 2;
                default:
                    return 1.2;
            }
        }

        public static double CalculateWHR(Parameters parameters, ParameterResults results)
        {
            double WHR = (double)parameters.FatLevel.WaistSize / (double)parameters.FatLevel.HipSize;
            if ((parameters.PresonalData.Gender == Gender.Kobieta && WHR > 0.88) || (parameters.PresonalData.Gender == Gender.Mężczyzna && WHR > 1))
            {
                results.WhrLabel = "Typ androidalny";
            }
            else
            {
                results.WhrLabel = "Typ gynoidalny";
            }
            return WHR;
        }

        public static double CalculateBF(Parameters parameters)
        {
            double bd;
            if (parameters.PresonalData.Gender == Gender.Kobieta)
            {
                double sum = parameters.FatLevel.ChestFold + parameters.FatLevel.BellyFold + parameters.FatLevel.ThighFold;
                bd = 1.0099421 - (0.0009929 * sum) + (0.0000023 * Math.Pow(sum, 2)) - (0.0001392 * parameters.PresonalData.Age.Value);
            }
            else
            {
                double sum = parameters.FatLevel.TricepsFold + parameters.FatLevel.HipFold + parameters.FatLevel.ThighFold;
                bd = 1.10938 - (0.0008267 * sum) + (0.0000016 * Math.Pow(sum, 2)) - (0.0002574 * parameters.PresonalData.Age.Value);
            }
            return Math.Round((495 / bd) - 450);
        }
    }
}
