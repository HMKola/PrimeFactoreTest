using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeFactorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = GetPrimeFactorOutPut(1, 10);
            Console.WriteLine(data.Sum());
            Console.ReadLine();

        }
        private static IEnumerable<int> GetPrimeFactorOutPut(int start, int end)
        {
            var rangeData = ParallelEnumerable.Range(start, end);
            var outputData = new List<int>();
            foreach (var number in rangeData)
            {
                outputData = new List<int>();
                outputData.Add(number);
                var series = RecursiveCall(number, outputData);
                yield return series.Count();

            }
        }

        private static IEnumerable<int> GetPrimesWithParellel(int InputData)
        {
            int loop = (int)Math.Sqrt(InputData);
            int first = ParallelEnumerable.Range(2, loop)
                .Where(x => ParallelEnumerable.Range(2, x - 2).All(y => x % y != 0))
                .FirstOrDefault(x => InputData % x == 0);
            var data = first == 0 || first == InputData ?
                       new[] { InputData } :
                       new[] { first }.Concat(GetPrimesWithParellel(InputData / first));
            return data;
        }

        private static IEnumerable<int> RecursiveCall(int numbers, List<int> OutputData)
        {
            if (numbers > 0)
            {
                var primeFactors = GetPrimesWithParellel(numbers);
                var primeCalc = primeFactors.Count() > 1 ? primeFactors.Sum() * primeFactors.Count() : numbers == 1 ? 0 : 1;
                if (!OutputData.Contains(primeCalc))
                {
                    if (primeFactors.Count() == 1)
                    {
                        if (numbers == 1)
                        {
                            OutputData.Add(primeCalc);
                            RecursiveCall(primeCalc, OutputData);
                        }
                        OutputData[0] = 1;
                    }
                    else
                    {
                        OutputData.Add(primeCalc);
                        RecursiveCall(primeCalc, OutputData);
                    }
                }

            }
            return OutputData;

        }
    }


}
