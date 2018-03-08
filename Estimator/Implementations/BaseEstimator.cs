using System.Collections.Generic;
using System.IO;

namespace Estimator.Implementations
{
    public abstract class BaseEstimator <T, P>
        //interface type
        where T : class 
        //parameter type
        where P : class
    {
        private Dictionary<string, T> interfacesDictionary;
        private Dictionary<string, P> testCases;

        protected abstract void callAppropiateFunction(T imp, P param);

        protected abstract void initializeInterfaces();

        protected abstract void initializeTestCases();

        private void initialize()
        {
            this.initializeInterfaces();
            this.initializeTestCases();
        }
        public BaseEstimator()
        {
            this.initialize();
        }

        public void Estimate(string outputPath)
        {
            using(var output = new StreamWriter(outputPath))
            {
                foreach(var test in testCases)
                {
                    output.WriteLine("TEST - {0}", test.Key);
                    foreach(var implementation in interfacesDictionary)
                    {
                        var watch = System.Diagnostics.Stopwatch.StartNew();
                        this.callAppropiateFunction(implementation.Value, test.Value);
                        watch.Stop();
                        var elapsedMs = watch.ElapsedMilliseconds;
                        output.WriteLine("{0} - {1} ms", implementation.Key, elapsedMs);
                    }
                }
            }
        }
    }
}
