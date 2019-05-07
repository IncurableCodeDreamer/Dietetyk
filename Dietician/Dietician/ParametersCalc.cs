using Dietician.Models;
using Dietician.Services;

namespace Dietician
{
    public class ParametersCalc
    {
        public static double CountCPM(PersonalDataSettings data)
        {
            double pal = ParameterService.onItemSelected(data.Lifestyle);

            if (data.Gender == Gender.Kobieta)
            {
                return (665.09 + (9.56 * data.Weight.Value) + (1.85 * data.Height.Value) - (4.67 * data.Age.Value))* pal;
            }
            else
            {
                return (66.47 + (13.75 * data.Weight.Value) + (5 * data.Height.Value) - (6.75 * data.Age.Value)) * pal;
            }
        }

        public static double ProteinsMinCalculate(double cpm)
        {
            return (cpm * 0.1) / 4;
        }

        public static double ProteinsMaxCalculate(double cpm)
        {
            return (cpm * 0.2) / 4;
        }

        public static double FatsMinCalculate(double cpm)
        {
            return (cpm * 0.2) / 9;
        }

        public static double FatsMaxCalculate(double cpm)
        {
            return (cpm * 0.35) / 9;
        }

        public static double CarbohydratesMinCalculate(double cpm)
        {
            return (cpm * 0.45) / 4;
        }

        public static double CarbohydratesMaxCalculate(double cpm)
        {
            return (cpm * 0.65) / 4;
        }
    }
}

