using NSuiteX.Library.ReverseComplement;
using System;
using System.Diagnostics;

namespace NSuiteX.Benchmark
{
    class Program
    {
        static Stopwatch _reverseComplementSingleStopwatch;

        static void Main(string[] args)
        {
            _reverseComplementSingleStopwatch = new Stopwatch();
            _reverseComplementSingleStopwatch.Start();
            ReverseComplement.RunMultiThread();
            _reverseComplementSingleStopwatch.Stop();
            Console.WriteLine($"Elapsed ms reverse complement: {_reverseComplementSingleStopwatch.ElapsedMilliseconds}");

            Console.Read();
        }
    }
}
