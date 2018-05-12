using Helpers.ConstructionClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    public static class MathHelper
    {
        public static List<Point> GenerateRandomCanvas(int amountOfPoints, int xAbsMax, int yAbsMax)
        {
            var randomizer = new Random();
            var result = Enumerable
                .Repeat(1, amountOfPoints)
                .Select(x => new Point
                {
                    X = randomizer.NextDouble() * randomizer.Next(-xAbsMax, xAbsMax),
                    Y = randomizer.NextDouble() * randomizer.Next(-yAbsMax, yAbsMax)
                })
                .ToList();
            return result;
        }

        public static bool IsPowerOfTwo(int x)
        {
            return (x != 0) && ((x & (x - 1)) == 0);
        }

        public static double GetDistance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }
    }
}
