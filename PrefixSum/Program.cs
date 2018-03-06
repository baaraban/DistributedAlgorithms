using Infrastructure.Managers.Implementations;
using PrefixSum.Implementations;
using System;
using System.Linq;
using System.Threading;

namespace PrefixSum
{
    class Program
    {
        static void Main(string[] args)
        {
            //var example = Enumerable.Repeat(1, 100).ToArray();
            var example = new int[] { 1, 2, 3, 4, 5, 6, 7};
            //var result = new PrefixSummator().GetPrefixSum(example);
            //var paralelResult = new PrefixTPLSummator().GetPrefixSum(example);
            //Console.WriteLine("RESULT:");
            //
            //Console.WriteLine("PARALLEL RESULT:");
            //foreach (var a in paralelResult)
            //    Console.Write(a + " ");
            //Console.WriteLine();
            var tM = new TPLThreadManager(4);

            var parallelSummator = new PrefixSummatorWithManager(tM);
            var result = parallelSummator.GetPrefixSum(example);
            Console.WriteLine("Prefix Sum with Manager");
            printArrayInConsole(result);
            //for (int i = 0; i < 8; ++i)
            //{
            //    tM.ScheduleAction(() =>
            //    {
            //        Console.WriteLine(i);
            //        Thread.Sleep(2000);
            //    });
            //}
            //tM.WaitAll();
        }

        public static void printArrayInConsole(int[] array)
        {
            foreach (var a in array)
                Console.Write(a + " ");
            Console.WriteLine();
        }
    }
}
