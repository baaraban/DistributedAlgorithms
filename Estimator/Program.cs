using Estimator.Implementations;
using System.Threading;

namespace Estimator
{
    class Program
    {
        static void Main(string[] args)
        {
            var prefixSumEstimator = new PrefixSumEstimator();
            var mergeSortEstimator = new MergeSortEstimator();

            new Thread(() => mergeSortEstimator.Estimate(@"C:\UCU\DistributedAlgorithms\HomeWorks\output\mergeSort.txt", true)).Start();
            //new Thread(() => prefixSumEstimator.Estimate(@"C:\UCU\DistributedAlgorithms\HomeWorks\output\prefixSum.txt", true)).Start();
        }
    }
}
