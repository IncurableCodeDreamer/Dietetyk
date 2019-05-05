using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dietician
{
    public class ParametersCalc
    {
        public static double CountCPM(double weight, double height, int age, Gender sex, double pal)
        {

            double cpm;

            if (sex == Gender.Kobieta)
            {
                double ppm = 665.09 + (9.56 * weight) + (1.85 * height) - (4.67 * age);
                cpm = ppm * pal;
            }
            else
            {
                double PPM = 66.47 + (13.75 * weight) + (5 * height) - (6.75 * age);
                cpm = PPM * pal;
            }
            return cpm;
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

