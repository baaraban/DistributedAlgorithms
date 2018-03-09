using MergeSort.Implementations;
using System;
using System.Linq;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] x = new int[] { 2, 1, 19, 24, 1, 89, 3, 15 };
            //var result = new SequentialMergeSorter().MergeSort(x);
            var randomizer = new Random();
            var result = Enumerable.Repeat(1, (int)Math.Pow(2, 12)).Select(y => randomizer.Next(100)).ToArray();
            //printArrayInConsole(result);
            Console.WriteLine("Result");
            printArrayInConsole(new ParallelWithActionStackForTwoThreads().MergeSort(result));
            Console.ReadLine();
        }
        public static void printArrayInConsole(int[] array)
        {
            foreach (var a in array)
                Console.Write(a + " ");
            Console.WriteLine();
        }

    }
}
