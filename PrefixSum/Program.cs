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
            var example = Enumerable.Repeat(1, 1000000).ToArray();
            //var example = new int[] { 1, 2, 3, 4, 5, 6, 7};
            //var result = new PrefixSummator().GetPrefixSum(example);
            Console.WriteLine("Not Parallel is done");
            //var paralelResult = new PrefixTPLSummator().GetPrefixSum(example);
            //Console.WriteLine("RESULT:");
            //
            //Console.WriteLine("PARALLEL RESULT:");
            //foreach (var a in paralelResult)
            //    Console.Write(a + " ");
            //Console.WriteLine();
            var tM = new TPLThreadManager(4);

            var parallelSummator = new PrefixSummatorWithManager(tM);
            //result = parallelSummator.GetPrefixSum(example);
            //Console.WriteLine("Prefix Sum with Manager");
            TPLManagerShowOff();   
        }

        public static void printArrayInConsole(int[] array)
        {
            foreach (var a in array)
                Console.Write(a + " ");
            Console.WriteLine();
        }

        public static void TPLManagerShowOff()
        {
            var tM = new TPLThreadManager(4);
            for (int i = 0; i < 8; ++i)
            {
                var tmp = i;
                Action a = () =>
                {
                    Console.WriteLine("I'm - " + tmp);
                    Thread.Sleep(tmp * 1000);
                    Console.WriteLine(tmp + "is finished");
                };
                tM.ScheduleAction(a);
            }
            tM.WaitAll();
        }
    }
}
