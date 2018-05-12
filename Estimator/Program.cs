using Estimator.Implementations;
using System;

namespace Estimator
{
    class Program
    {
        static void estimatePrefixSum()
        {
            var prefixSumEstimator = new PrefixSumEstimator();
            prefixSumEstimator.Estimate(@"C:\UCU\DistributedAlgorithms\HomeWorks\output\prefixSum.txt", true);
        }

        static void estimateMergeSort()
        {
            var mergeSortEstimator = new MergeSortEstimator();
            mergeSortEstimator.Estimate(@"C:\UCU\DistributedAlgorithms\HomeWorks\output\mergeSort.txt", true);
        }

        static void estimateClosestDistance()
        {
            var closestDistanceEstimator = new ClosestDistanceEstimator();
            closestDistanceEstimator.Estimate(@"C:\UCU\DistributedAlgorithms\HomeWorks\output\closest_dist.txt", true);
        }

        static void Main(string[] args)
        {
            estimateClosestDistance();
            Console.ReadLine();
        }
    }
}
