using MergeSort.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] x = new int[] { 2, 1, 19, 24, 1, 89, 3, 15 };
            //var result = new SequentialMergeSorter().MergeSort(x);
            var result = new ParallelMergeSorterForInfiniteThreads().MergeSort(x);
            printArrayInConsole(result);
        }
        public static void printArrayInConsole(int[] array)
        {
            foreach (var a in array)
                Console.Write(a + " ");
            Console.WriteLine();
        }

    }
}
