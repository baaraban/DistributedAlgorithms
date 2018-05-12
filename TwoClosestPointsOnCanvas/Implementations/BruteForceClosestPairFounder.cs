using System;
using System.Collections.Generic;
using Helpers.ConstructionClasses;
using TwoClosestPointsOnCanvas.Interfaces;
using Helpers;

namespace TwoClosestPointsOnCanvas.Implementations
{
    public class BruteForceClosestPairFounder : IClosestPairFounder
    {
        public Tuple<Point, Point> GetClosestPair(List<Point> points, bool printSortedPointsInConsole = false)
        {
            var firstPoint = new Point();
            var secondPoint = new Point();
            var minDistance = double.MaxValue;
            for(var i = 0; i < points.Count; ++i)
            {
                for(var j = i + 1; j < points.Count; ++j)
                {
                    var localDistance = MathHelper.GetDistance(points[i], points[j]);
                    if(localDistance < minDistance)
                    {
                        firstPoint = points[i];
                        secondPoint = points[j];
                        minDistance = localDistance;
                    }
                }
            }
            return new Tuple<Point, Point>(firstPoint, secondPoint);
        }
    }
}
