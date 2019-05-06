using Dietician.Models;
using System;

namespace Dietician.Services
{
    public class ParameterService
    {
        public double CalculateBMI(PersonalDataSettings person)
        {
            return person.Weight.Value/((person.Height.Value/100)*(person.Height.Value/100));
        }

        public string GetBMILabel(ParameterResults results)
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

        public double onItemSelected(int id)
        {
            switch (id)
            {
                case 0:
                    return 1.2;
                case 1:
                    return 1.4;
                case 2:
                    return 1.6;
                case 3:
                    return 1.8;
                case 4:
                    return 2;
                default:
                    return 1.2;
            }
        }

        public double CalculateWHR(Parameters parameters)
        {
            double WHR = parameters.FatLevel.WaistSize/parameters.FatLevel.HipSize;
            if ((parameters.PresonalData.Gender == Gender.Kobieta && WHR > 0.88) || (parameters.PresonalData.Gender == Gender.Mężczyzna && WHR > 1))
            {
                parameters.ParameterResults.WhrLabel = "Typ androidalny";
            }
            else
            {
                parameters.ParameterResults.WhrLabel = "Typ gynoidalny";
            }
            return WHR;
        }

        public double CalculateBF(Parameters parameters)
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
