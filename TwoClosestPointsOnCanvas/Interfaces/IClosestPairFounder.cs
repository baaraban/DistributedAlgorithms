using Helpers.ConstructionClasses;
using System;
using System.Collections.Generic;

namespace TwoClosestPointsOnCanvas.Interfaces
{
    public interface IClosestPairFounder
    {
        Tuple<Point, Point> GetClosestPair(List<Point> points);
    }
}
