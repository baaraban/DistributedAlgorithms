using PrefixSum.Implementations;
using System;
using System.Linq;

namespace PrefixSum
{
    class Program
    {
        static void Main(string[] args)
        {
            var example = Enumerable.Repeat(1, 100).ToArray();
            var result = new PrefixSummator().GetPrefixSum(example);
            var paralelResult = new PrefixTPLSummator().GetPrefixSum(example);
            Console.WriteLine("RESULT:");
            foreach (var a in result)
                Console.Write(a + " ");
            Console.WriteLine();
            Console.WriteLine("PARALLEL RESULT:");
            foreach (var a in paralelResult)
                Console.Write(a + " ");
            Console.WriteLine();
        }
    }
}
