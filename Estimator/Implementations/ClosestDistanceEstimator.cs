using Helpers;
using Helpers.ConstructionClasses;
using System;
using System.Collections.Generic;
using TwoClosestPointsOnCanvas.Implementations;
using TwoClosestPointsOnCanvas.Interfaces;

namespace Estimator.Implementations
{
    public class ClosestDistanceEstimator : BaseEstimator<IClosestPairFounder, List<Point>>
    {

        protected override void callAppropiateFunction(IClosestPairFounder imp, List<Point> param)
        {
            imp.GetClosestPair(param);
        }

        protected override void initializeInterfaces()
        {
            this.interfacesDictionary.Add("Sequantional", new SequantionalClosestPairFounder());
            this.interfacesDictionary.Add("Two threads(TPL)", new TPLClosestPairFounder(depthOfParalelization: 1));
            this.interfacesDictionary.Add("Four threads(TPL)", new TPLClosestPairFounder(depthOfParalelization: 2));
            this.interfacesDictionary.Add("Eight threads(TPL)", new TPLClosestPairFounder(depthOfParalelization: 3));
        }

        protected override void initializeTestCases()
        {
            for (var i = 2; i < 24; ++i)
            {
                this.testCases.Add($"2^{i} elements", MathHelper.GenerateRandomCanvas((int)Math.Pow(2,i), 10000, 10000));
            }
        }
    }
}
