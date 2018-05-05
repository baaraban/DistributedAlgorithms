using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapReduce
{
    class Program
    {
        static void Main(string[] args)
        {
            var randomizer = new Random();
            var list = Enumerable.Repeat(1, 1024).Select(x => x * randomizer.Next(0, 10)).ToList();
            
            var mapReduce = new DummySolutions.DummyMapReduce();
            var neededAnswer = mapReduce.DoAction(list);
            foreach(var a in neededAnswer)
            {
                Console.WriteLine("Number {0} - {1}", a.Key, a.Value);
                Console.WriteLine("Assertion {0}", list.Count(x => x == a.Key));
            }
        }
    }
}
