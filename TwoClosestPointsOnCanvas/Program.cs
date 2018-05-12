using Helpers;
using Helpers.ConstructionClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoClosestPointsOnCanvas.Implementations;

namespace TwoClosestPointsOnCanvas
{
    public class Program
    {
        private static List<Point> generateRandomCanvas(int amountOfPoints, int xAbsMax, int yAbsMax)
        {
            var randomizer = new Random();
            var result = Enumerable
                .Repeat(1, amountOfPoints)
                .Select(x => new Point {
                    X = randomizer.NextDouble() * randomizer.Next(-xAbsMax, xAbsMax),
                    Y = randomizer.NextDouble() * randomizer.Next(-yAbsMax, yAbsMax)
                })
                .ToList();
            return result;
        }

        private static void printCanvas(List<Point> canvas)
        {
            Console.WriteLine("INITIAL SOMETHING");
            foreach (var a in canvas)
            {
                Console.WriteLine(a);
            }
            Console.WriteLine("_______________________");
        }
        static void Main(string[] args)
        {
            var toTest = generateRandomCanvas(10000, 100, 100);
            var brute = new BruteForceClosestPairFounder();
            var seq = new SequantionalClosestPairFounder();
           
            var bruteRes = brute.GetClosestPair(toTest);
            Console.WriteLine("brute: {0} ", bruteRes);
            var seqRes = seq.GetClosestPair(toTest, printSortedPointsInConsole : true);
            Console.WriteLine("seq: {1}", seqRes);
            Console.ReadLine();
        }
    }
}
