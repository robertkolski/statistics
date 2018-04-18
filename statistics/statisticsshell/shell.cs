using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stat = global::Statistics.Statistics;

namespace StatisticsShell
{
    public class Shell
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("About to calculate normal distribution.");
            Console.Write("Input x: ");
            double x = double.Parse(Console.ReadLine());
            Console.Write("Input mean: ");
            double mean = double.Parse(Console.ReadLine());
            Console.Write("Input standard deviation: ");
            double standardDeviation = double.Parse(Console.ReadLine());
            double f_x = Stat.NormalPDF(x, mean, standardDeviation);
            Console.WriteLine("Normal PDF = {0}", f_x);
            double F_x = Stat.NormalCDF(x, mean, standardDeviation);
            Console.WriteLine("Normal CDF = {0}", F_x);
            double confidence = Stat.NormalCDF(-x, x, mean, standardDeviation);
            Console.WriteLine("Confidence = {0}", confidence);
        }
    }
}
