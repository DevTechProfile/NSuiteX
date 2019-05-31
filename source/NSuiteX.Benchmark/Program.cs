using NSuiteX.Library.Pidigits;
using NSuiteX.Library.ReverseComplement;
using System;
using System.Diagnostics;

namespace NSuiteX.Benchmark
{
    class Program
    {
        static Stopwatch _reverseComplementStopwatch;
        static Stopwatch _piDigitsStopwatch;

        static void Main(string[] args)
        {
            //_reverseComplementStopwatch = new Stopwatch();
            //_reverseComplementStopwatch.Start();
            //ReverseComplement.RunMultiThread();
            //_reverseComplementStopwatch.Stop();
            //Console.WriteLine($"Elapsed ms reverse complement: {_reverseComplementStopwatch.ElapsedMilliseconds}");

            _piDigitsStopwatch = new Stopwatch();
            _piDigitsStopwatch.Start();
            Pidigits.RunSingleThread();
            _piDigitsStopwatch.Stop();
            Console.WriteLine($"Elapsed ms Pi digits: {_piDigitsStopwatch.ElapsedMilliseconds}");

            Console.Read();
        }
    }
}
