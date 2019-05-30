using System;
using System.Diagnostics;

namespace NSuiteX.TestCall
{
    class Program
    {
        static Stopwatch _testAlgorithmStopwatch;

        public static void Main(string[] args)
        {
            _testAlgorithmStopwatch = new Stopwatch();
            _testAlgorithmStopwatch.Start();

            // put test call here

            _testAlgorithmStopwatch.Stop();
            Console.WriteLine($"Elapsed ms test algorithm: {_testAlgorithmStopwatch.ElapsedMilliseconds}");
        }
    }
}
