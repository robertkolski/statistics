﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics
{
    public static class Statistics
    {
        public static double NormalPDF(double z)
        {
            return (Math.Exp(-z * z / 2.0)) / (Math.Sqrt(2 * Math.PI));
        }

        public static double NormalPDF(double x, double mean, double standardDeviation)
        {
            double z = (x - mean) / standardDeviation;
            return (Math.Exp(-z * z / 2.0)) / (Math.Sqrt(2 * Math.PI) * standardDeviation);
        }

        public static double NormalCDF(double z)
        {
            return NormalCDF(z, 0.0, 1.0);
        }

        public static double NormalCDF(double from, double to)
        {
            return NormalCDF(from, to, 0.0, 1.0);
        }

        public static double NormalCDF(double x, double mean, double standardDeviation)
        {
            // TODO: create unit tests for this
            double z = (x - mean) / standardDeviation;

            double factor = 1.0 / Math.Sqrt(2.0 * Math.PI) * Math.Exp(-z * z / 2.0);

            double series = z;
            double lastSeriesValue = series;
            double delta = Math.Abs(z);

            double doubleFactorial = 1.0;
            for (int i = 3; delta > 0.0; i+=2)
            {
                lastSeriesValue = series;

                doubleFactorial *= i;
                series += Math.Pow(z, i) / doubleFactorial;

                delta = Math.Abs(series - lastSeriesValue);
            }

            return (1.0 / standardDeviation) * ((1.0 / 2.0) + (factor * series));
        }

        public static double NormalCDF(double from, double to, double mean, double standardDeviation)
        {
            double fromCDF = 0.0;
            if (from != double.NegativeInfinity)
            {
                fromCDF = NormalCDF(from, mean, standardDeviation);
            }

            double toCDF = 1.0;
            if (to != double.PositiveInfinity)
            {
                toCDF = NormalCDF(to, mean, standardDeviation);
            }

            return toCDF - fromCDF;
        }
    }
}