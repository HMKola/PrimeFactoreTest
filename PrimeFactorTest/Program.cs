using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrimeFactorTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(SumOfSequence(0, 10).Sum());
            Console.ReadLine();

        }
        private static List<int> data = new List<int>();

        private static IEnumerable<int> SumOfSequence(int startRange, int endRange)
        {
            for (int i = startRange + 1; i <= endRange; i++)
            {
                data = new List<int>();
                data.Add(i);
                var series = getPrimesMultiplyPrimes(i);
                yield return series.Count();
            }
        }

        private static IEnumerable<int> getPrimesMultiplyPrimes(int numbers)
        {
            if (numbers > 0)
            {
                var getPrimeNumber = GetPrimeFactors(numbers, new PrimeNumberCheck());
                var mupltiplydata = getPrimeNumber.Sum() * getPrimeNumber.Count();
                if (!data.Contains(mupltiplydata))
                {

                    data.Add(mupltiplydata);
                    getPrimesMultiplyPrimes(mupltiplydata);
                }
            }
            return data;

        }

        private static IEnumerable<int> GetPrimeFactors(int value, PrimeNumberCheck IsprimeNumbers)
        {
            List<int> factors = new List<int>();

            foreach (int prime in IsprimeNumbers)
            {
                while (value % prime == 0)
                {
                    value /= prime;
                    factors.Add(prime);
                }

                if (value == 1)
                    break;
            }

            return factors;
        }

    }

    public class PrimeNumberCheck : IEnumerable<int>
    {
        private static List<int> _primes = new List<int>();
        private int _lastChecked;


        public PrimeNumberCheck()
        {
            _primes.Add(2);
            _lastChecked = 2;
        }


        private bool IsPrime(int checkValue)
        {
            bool isPrime = true;

            foreach (int prime in _primes)
            {
                if ((checkValue % prime) == 0 && prime <= Math.Sqrt(checkValue))
                {
                    isPrime = false;
                    break;
                }
            }

            return isPrime;
        }


        public IEnumerator<int> GetEnumerator()
        {
            foreach (int prime in _primes)
                yield return prime;

            while (_lastChecked < int.MaxValue)
            {
                _lastChecked++;

                if (IsPrime(_lastChecked))
                {
                    _primes.Add(_lastChecked);
                    yield return _lastChecked;
                }
            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
