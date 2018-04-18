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
            Console.WriteLine("4! = {0}", Stat.Factorial(4));
            Console.WriteLine("5! = {0}", Stat.Factorial(5));
            Console.WriteLine("6! = {0}", Stat.Factorial(6));

        promptshouldcalculategamma:
            Console.Write("Should I calculate Gamma?");
            string command = Console.ReadLine();
            if (command.ToLower() == "quit")
            {
                return;
            }

            if (command.ToLower() == "yes")
            {
                // some people say not to use goto
                // but it makes this code more readable
                // normally I don't use goto either
                goto calculategamma;
            }

            if (command.ToLower() == "no")
            {
                goto skipgamma;
            }

            goto promptshouldcalculategamma;

            calculategamma:

            Console.Write("Gamma(x) will be calculated.  What is the value of x?: ");
            double x_gamma_input = double.Parse(Console.ReadLine());

            Action[] runTheseCalculations = new Action[]
            {
                Calculate("About to calculate Gamma, k = 10:", () => Stat.Gamma(x_gamma_input, 10)),
                Calculate("About to calculate Gamma, k = 100:", () => Stat.Gamma(x_gamma_input, 100)),
                Calculate("About to calculate Gamma, k = 1000:", () => Stat.Gamma(x_gamma_input, 1000)),
                Calculate("About to calculate Gamma, k = 10000:", () => Stat.Gamma(x_gamma_input, 10000)),
                Calculate("About to calculate Gamma, k = 100000:", () => Stat.Gamma(x_gamma_input, 100000)),
                Calculate("About to calculate Gamma, k = 1000000:", () => Stat.Gamma(x_gamma_input, 1000000)),
                Calculate("About to calculate Gamma, k = 10000000:", () => Stat.Gamma(x_gamma_input, 10000000)),
                Calculate("About to calculate Gamma, k = 100000000:", () => Stat.Gamma(x_gamma_input, 100000000)),
                Calculate("About to calculate Gamma Integral, inf = 100000, increment = 1:",
                    () => Stat.GammaIntegral(x_gamma_input, 100000, 1)),
                Calculate("About to calculate Gamma Integral, inf = 100000, increment = .1:",
                    () => Stat.GammaIntegral(x_gamma_input, 100000, .1)),
                Calculate("About to calculate Gamma Integral, inf = 100000, increment = .01:",
                    () => Stat.GammaIntegral(x_gamma_input, 100000, .01)),
                Calculate("About to calculate Gamma Integral, inf = 100000, increment = .001:",
                    () => Stat.GammaIntegral(x_gamma_input, 100000, .001)),
                Calculate("About to calculate Gamma Integral, inf = 10000, increment = .0001:",
                    () => Stat.GammaIntegral(x_gamma_input, 10000, .0001)),
                Calculate("About to calculate Gamma Integral, inf = 1000, increment = .00001:",
                    () => Stat.GammaIntegral(x_gamma_input, 1000, .00001)),
                Calculate("About to calculate Gamma Integral, inf = 100, increment = .000001:",
                    () => Stat.GammaIntegral(x_gamma_input, 100, .000001))
            };

            
            System.Threading.Tasks.Parallel.Invoke(runTheseCalculations);

        skipgamma:

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

        private static object lockObject = new object();

        public static Action Calculate (string headine, Func<double> calculation)
        {
            Action action = () =>
            {
                System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
                timer.Start();
                double result = calculation();
                timer.Stop();
                lock (lockObject)
                {
                    Console.Write("[Time: {0}]", timer.ElapsedMilliseconds);
                    Console.Write(headine);
                    Console.WriteLine(result);
                }
            };
            return action;
        } 
    }
}
