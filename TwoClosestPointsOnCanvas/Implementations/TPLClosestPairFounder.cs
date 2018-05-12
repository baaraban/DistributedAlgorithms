using System;
using System.Collections.Generic;
using System.Linq;
using Helpers.ConstructionClasses;
using TwoClosestPointsOnCanvas.Interfaces;
using System.Threading.Tasks;
using Helpers;

namespace TwoClosestPointsOnCanvas.Implementations
{
    class TPLClosestPairFounder : IClosestPairFounder
    {
        private BruteForceClosestPairFounder bruteForce = new BruteForceClosestPairFounder();
        public readonly int DepthOfParalelization;
        public TPLClosestPairFounder(int depthOfParalelization = 2)
        {
            this.DepthOfParalelization = depthOfParalelization;
        }

        private class MinimumEntity
        {
            public double Distance { get; set; }
            public Tuple<Point, Point> Between { get; set; }
        }
        private const int _magicConstForNLogN = 7;

        private Tuple<List<Point>, MinimumEntity> recursiveProcess(List<Point> points, int depth)
        {
            if (points.Count < 2)
            {
                return new Tuple<List<Point>, MinimumEntity>(points, new MinimumEntity { Distance = Double.MaxValue });
            }
            var medianX = points.Average(x => x.X);
            var left = points.Where(x => x.X < medianX);
            var right = points.Except(left);

            //condition for points with the same median
            if (!left.Any() || !right.Any())
            {
                var half = points.Count / 2;
                left = points.Take(half);
                right = points.Skip(half).Take(points.Count - half);
            }
            Tuple<List<Point>, MinimumEntity> leftResult;
            Tuple<List<Point>, MinimumEntity> rightResult;
            if (depth >= this.DepthOfParalelization)
            {
                leftResult = recursiveProcess(left.ToList(), depth+1);
                rightResult = recursiveProcess(right.ToList(), depth+1);
            }
            else
            {
                var tl = new Task<Tuple<List<Point>, MinimumEntity>>(() => recursiveProcess(left.ToList(), depth + 1));
                var tr = new Task<Tuple<List<Point>, MinimumEntity>>(() => recursiveProcess(right.ToList(), depth + 1));
                tl.Start();
                tr.Start();
                Task.WaitAll(tl, tr);
                leftResult = tl.Result;
                rightResult = tr.Result;
            }

            var newPoints = mergeByY(leftResult.Item1, rightResult.Item1);
            var newDistance = boundaryMerge(newPoints, leftResult.Item2, rightResult.Item2, medianX);
            return new Tuple<List<Point>, MinimumEntity>(newPoints, newDistance);
        }

        private MinimumEntity boundaryMerge(List<Point> newPoints,
            MinimumEntity leftClosest,
            MinimumEntity rightClosest,
            double medianX)
        {
            var initialMin = Math.Min(leftClosest.Distance, rightClosest.Distance);
            var points = leftClosest.Distance < rightClosest.Distance ? leftClosest.Between : rightClosest.Between;
            var mSet = newPoints
                .Where(x => x.X >= medianX - initialMin && x.X <= medianX + initialMin)
                .ToList();
            var mMin = double.MaxValue;
            var localRes = new Tuple<Point, Point>(null, null);
            for (var i = 0; i < mSet.Count; ++i)
            {
                for (var j = 1; j <= Math.Min(mSet.Count - i - 1, _magicConstForNLogN); ++j)
                {
                    var dist = MathHelper.GetDistance(mSet[i], mSet[i + j]);
                    if (dist <= mMin)
                    {
                        mMin = dist;
                        localRes = new Tuple<Point, Point>(mSet[i], mSet[i + j]);
                    }
                }
            }

            var result = Math.Min(initialMin, mMin);
            if (mMin <= initialMin)
            {
                points = localRes;
            }
            return new MinimumEntity { Distance = result, Between = points };
        }

        private List<Point> mergeByY(List<Point> left, List<Point> right)
        {
            var result = new List<Point>();
            var leftI = 0;
            var rightI = 0;
            while (leftI < left.Count && rightI < right.Count)
            {
                if (left[leftI].Y < right[rightI].Y)
                {
                    result.Add(left[leftI++]);
                }
                else
                {
                    result.Add(right[rightI++]);
                }
            }
            while (leftI < left.Count)
            {
                result.Add(left[leftI++]);
            }
            while (rightI < right.Count)
            {
                result.Add(right[rightI++]);
            }
            return result;
        }

        public Tuple<Point, Point> GetClosestPair(List<Point> points)
        {
            throw new NotImplementedException();
        }
    }
}
