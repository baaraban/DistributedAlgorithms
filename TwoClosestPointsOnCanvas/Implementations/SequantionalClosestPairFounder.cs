using System;
using System.Collections.Generic;
using Helpers.ConstructionClasses;
using TwoClosestPointsOnCanvas.Interfaces;
using System.Linq;
using Helpers;

namespace TwoClosestPointsOnCanvas.Implementations
{
    public class SequantionalClosestPairFounder : IClosestPairFounder
    {
        private const int _magicConstForNLogN = 7;
        private Tuple<List<Point>, double> recursiveProcess(List<Point> points)
        {
            if(points.Count < 2)
            {
                return new Tuple<List<Point>, double>(points, Double.MaxValue);
            }
            var medianX = points.Average(x => x.X);
            var left = points.Where(x => x.X < medianX);
            var right = points.Except(left);
            var leftResult = recursiveProcess(left.ToList());
            var rightResult = recursiveProcess(right.ToList());
            var newPoints = mergeByY(leftResult.Item1, rightResult.Item1);
            var newDistance = boundaryMerge(newPoints, leftResult.Item2, rightResult.Item2, medianX);
            return new Tuple<List<Point>, double>(newPoints, newDistance);
        }

        private double boundaryMerge(List<Point> newPoints, 
            double leftClosestDistance, 
            double rightClosestDistance, 
            double medianX)
        {
            var initialMin = Math.Min(leftClosestDistance, rightClosestDistance);
            var mSet = newPoints
                .Where(x => x.X >= medianX - initialMin && x.X < medianX + initialMin)
                .ToList();
            var mMin = double.MaxValue;
            for (var i = 0; i < mSet.Count; ++i)
            {
                for(var j = 1; j <= Math.Min(mSet.Count - i - 1, _magicConstForNLogN); ++j)
                {
                    var dist = MathHelper.GetDistance(mSet[i], mSet[i + j]);
                    if (dist < mMin)
                    {
                        mMin = dist;
                    }
                }
            }
            return Math.Min(initialMin, mMin);
        }

        private List<Point> mergeByY(List<Point> left, List<Point> right)
        {
            var result = new List<Point>();
            var leftI = 0;
            var rightI = 0;
            while (leftI < left.Count && rightI < right.Count)
            {
                if(left[leftI].Y < right[rightI].Y)
                {
                    result.Add(left[leftI++]);
                }
                else
                {
                    result.Add(right[rightI++]);
                }
            }
            while(leftI < left.Count)
            {
                result.Add(left[leftI++]);
            }
            while (rightI < right.Count)
            {
                result.Add(left[rightI++]);
            }
            return result;
        }

        private void printSortedPointsInConsole(List<Point> points)
        {
            foreach(var p in points)
            {
                Console.WriteLine(p);
            }
        }

        public Tuple<Point, Point> GetClosestPair(List<Point> points, bool printSortedPointsInConsole = false)
        {
            var result = recursiveProcess(points);
            if (printSortedPointsInConsole)
            {
                this.printSortedPointsInConsole(result.Item1);
            }
            throw new NotImplementedException();
        }
    }
}
