using MergeSort.Implementations;
using MergeSort.Interfaces;
using System;
using System.Linq;

namespace Estimator.Implementations
{
    public class MergeSortEstimator : BaseEstimator<IMergeSorter, int[]>
    {
        protected override void callAppropiateFunction(IMergeSorter imp, int[] param)
        {
            imp.MergeSort(param);
        }

        protected override void initializeInterfaces()
        {
            this.interfacesDictionary.Add("Sequantional", new SequentialMergeSorter());
            this.interfacesDictionary.Add("Two threads, forming stack of execution", new ParallelWithActionStackForTwoThreads());
            this.interfacesDictionary.Add("Parallel for infinite cores", new ParallelMergeSorterForInfiniteThreads());
        }

        protected override void initializeTestCases()
        {
            for(var i = 2; i < 18; ++i)
            {
                this.testCases.Add($"2^{i} elements", Enumerable.Repeat(1, (int)Math.Pow(2, i)).ToArray());
            }
        }
    }
}
