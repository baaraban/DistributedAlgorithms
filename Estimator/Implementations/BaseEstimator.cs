using System;
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
        protected Dictionary<string, T> interfacesDictionary;
        protected Dictionary<string, P> testCases;

        protected abstract void callAppropiateFunction(T imp, P param);

        protected abstract void initializeInterfaces();

        protected abstract void initializeTestCases();

        private void initialize()
        {
            this.interfacesDictionary = new Dictionary<string, T>();
            this.testCases = new Dictionary<string, P>();
            this.initializeInterfaces();
            this.initializeTestCases();
        }
        public BaseEstimator()
        {
            this.initialize();
        }

        public void Estimate(string outputPath, bool withLogging = false)
        {
            using(var output = new StreamWriter(outputPath))
            {
                foreach(var test in testCases)
                {
                    if (withLogging)
                    {
                        System.Console.WriteLine("Starting {0} test", test.Key);
                    }
                    output.WriteLine("TEST - {0}", test.Key);
                    foreach(var implementation in interfacesDictionary)
                    {
                        try
                        {
                            if (withLogging)
                            {
                                System.Console.WriteLine("Starting {0} implementation", implementation.Key);
                            }
                            var watch = System.Diagnostics.Stopwatch.StartNew();
                            this.callAppropiateFunction(implementation.Value, test.Value);
                            watch.Stop();
                            var elapsedMs = watch.ElapsedMilliseconds;
                            output.WriteLine("{0} - {1} ms", implementation.Key, elapsedMs);
                            if (withLogging)
                            {
                                System.Console.WriteLine("Finishing {0} implementation", implementation.Key);
                            }
                        }
                        catch (Exception ex)
                        {
                            output.WriteLine("{0} - exception", implementation.Key);
                        }
                    }
                    if (withLogging)
                    {
                        System.Console.WriteLine("Finishing {0} test", test.Key);
                    }
                }
            }
        }
    }
}
