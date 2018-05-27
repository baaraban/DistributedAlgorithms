using Infrastructure.MathStructures;
using System;

namespace MaxFlow
{
    class Program
    {
        const string filePath = @"../../TestingExamples/graph2.txt";

        static void Main(string[] args)
        {
            var graph = NetworkGraph.ComposeFromTextFile(filePath);
            var result = graph.GetAugmentedPathBFS();
            foreach (var edge in result)
            {
                Console.WriteLine(edge);
            }
            Console.ReadLine();
        }
    }
}
