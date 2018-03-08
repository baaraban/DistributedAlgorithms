using Infrastructure.Managers.Implementations;
using PrefixSum.Implementations;
using PrefixSum.Interfaces;
using System;
using System.Linq;

namespace Estimator.Implementations
{
    public class PrefixSumEstimator : BaseEstimator<IPrefixSum, int[]>
    {
        protected override void callAppropiateFunction(IPrefixSum imp, int[] param)
        {
            imp.GetPrefixSum(param);
        }

        protected override void initializeInterfaces()
        {
            this.interfacesDictionary.Add("Sequantional", new PrefixSummator());
            this.interfacesDictionary.Add("Parallel for infinite cores(Thread Pool)", new PrefixSummatorForInfiniteThreads());
            this.interfacesDictionary.Add("Parallel 2 threads(without Thread Pool)",
                new PrefixSummatorWithQueue(
                    new ThreadingActionQueue(2)
                    ));
            this.interfacesDictionary.Add("Parallel 2 threads(Thread Pool)",
                new PrefixSummatorWithQueue(
                    new TPLActionQueue(2)
                    ));
            this.interfacesDictionary.Add("Parallel 4 threads(without Thread Pool)",
                new PrefixSummatorWithQueue(
                    new ThreadingActionQueue(4)
                    ));
            this.interfacesDictionary.Add("Parallel 4 threads(Thread Pool)",
                new PrefixSummatorWithQueue(
                    new TPLActionQueue(4)
                    ));
        }

        protected override void initializeTestCases()
        {
            for (var i = 2; i <= 16; ++i)
            {
                this.testCases.Add($"2^{i} elements", Enumerable.Repeat(1, (int)Math.Pow(2, i)).ToArray());
            }
        }
    }
}
