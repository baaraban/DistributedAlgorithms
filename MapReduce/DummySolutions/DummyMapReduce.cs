using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapReduce.DummySolutions
{
    public class DummyMapReduce
    {
        private object locker = new object();
        private List<List<int>> split(List<int> input, int amountOfPartitions)
        {
            var result = new List<List<int>>();
            var total = input.Count;
            int toTake = total / amountOfPartitions;
            int taken = 0;
            int step = 0;
            for(var i = 0; i < amountOfPartitions; ++i)
            {
                result.Add(input.Skip(0).Take(toTake).ToList());
                taken += toTake;
                step++;
                if(step == amountOfPartitions - 1)
                {
                    result.Add(input.Skip(taken).Take(total - taken).ToList());
                    break;
                }
            }
            return result;
        }

        private List<Dictionary<int, List<int>>> map(List<List<int>> splitted)
        {
            var result = new List<Dictionary<int, List<int>>>();
            var tasks = new List<Task>();
            foreach(var list in splitted)
            {
                var task = new Task(() =>
                {
                    var localList = list;
                    var dict = new Dictionary<int, List<int>>();
                    foreach (var number in localList)
                    {
                        if (dict.ContainsKey(number))
                        {
                            dict[number].Add(1);
                        }
                        else
                        {
                            dict.Add(number, new List<int> { 1 });
                        }
                    }
                    lock (locker)
                    {
                        result.Add(dict);
                    }
                });
                tasks.Add(task);
                task.Start();
            }
            Task.WaitAll(tasks.ToArray());
            return result;
        }

        private Dictionary<int, List<int>> mergeAfterMap(List<Dictionary<int, List<int>>> mapped)
        {
            var result = new Dictionary<int, List<int>>();
            foreach(var dict in mapped)
            {
                foreach(var keyValue in dict)
                {
                    if (result.ContainsKey(keyValue.Key))
                    {
                        result[keyValue.Key] = result[keyValue.Key].Concat(keyValue.Value).ToList();
                    }
                    else
                    {
                        result.Add(keyValue.Key, keyValue.Value);
                    }
                }
            }
            return result;
        }

        private List<Dictionary<int, List<int>>> splitBeforeReduce(Dictionary<int, List<int>> mapped, int amountOfPartitions)
        {
            //var result = new List<Dictionary<int, List<int>>>();
            //var total = mapped.Count;
            //var toTake = total / amountOfPartitions;
            //var taken = 0;
            //for (var i = 0; i < amountOfPartitions; ++i)
            //{
            //    result.Add(mapped.Skip(0).Take(toTake).ToList());
            //    taken += toTake;
            //    step++;
            //    if (step == amountOfPartitions - 1)
            //    {
            //        result.Add(input.Skip(taken).Take(total - taken).ToList());
            //        break;
            //    }
            //}
            //return result;
        }

        private List<Dictionary<int, int>> reduce(List<Dictionary<int, List<int>>> mapped)
        {
            var result = new List<Dictionary<int, int>>();

            return result;
        }

        private Dictionary<int, int> mergeAfterReduce(List<Dictionary<int, int>> reduced)
        {
            var result = new Dictionary<int, int>();
            return result;
        }

        public Dictionary<int, int> DoAction(List<int> input)
        {
            var splitted = this.split(input, 4);
            var mapped = this.map(splitted);
            var merged = this.mergeAfterMap(mapped);
            var reduceSplit = this.splitBeforeReduce(merged, 4);
            var reduced = this.reduce(reduceSplit);
            return this.mergeAfterReduce(reduced);
        }
    }
}
