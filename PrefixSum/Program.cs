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
            //var example = Enumerable.Repeat(1, 1000).ToArray();
            var example = new int[] { 1, 2, 3, 4, 5, 6, 7};
            //var result = new PrefixSummator().GetPrefixSum(example); 
            //var paralelResult = new TplPrefixSummator().GetPrefixSum(example);
            //Console.WriteLine("RESULT:");
            //printArrayInConsole(paralelResult);

            //
            //Console.WriteLine("PARALLEL RESULT:");
            //foreach (var a in paralelResult)
            //    Console.Write(a + " ");
            //Console.WriteLine();
            var tM = new TPLActionQueue(4);

            var parallelSummator = new PrefixSummatorWithManager(tM);
            var result = parallelSummator.GetPrefixSum(example);
            Console.WriteLine("Prefix Sum with Manager");
            printArrayInConsole(result);
            //TPLManagerShowOff();
        }

        public static void printArrayInConsole(int[] array)
        {
            foreach (var a in array)
                Console.Write(a + " ");
            Console.WriteLine();
        }

        public static void TPLManagerShowOff()
        {
            var tM = new TPLActionQueue(4);
            tM.Start();
            for (int i = 0; i < 8; ++i)
            {
                var tmp = i;
                Action a = () =>
                {
                    Thread.Sleep( 2000);
                    Console.WriteLine("I'm - " + tmp);
                    Thread.Sleep(2000);
                    Console.WriteLine(tmp + " is finished");
                };
                tM.ScheduleAction(a);
            }
            tM.SynchronizeQueue();
        }
    }
}
