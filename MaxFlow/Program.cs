using Infrastructure.MathStructures;
using MaxFlow.Implementations;
using System;

namespace MaxFlow
{
    class Program
    {
        const string filePath = @"../../TestingExamples/graph3.txt";

        static void Main(string[] args)
        {
            var graph = NetworkGraph.ComposeFromTextFile(filePath);
            
            var calculator = new SequantionalEdmondKarpMaxFlow();
            Console.WriteLine(calculator.GetMaxFlow(graph));
            Console.ReadLine();
        }
    }
}
