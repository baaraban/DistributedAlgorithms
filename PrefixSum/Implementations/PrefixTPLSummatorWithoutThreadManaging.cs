using Helpers;
using PrefixSum.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrefixSum.Implementations
{
    internal class PrefixTPLSummatorWithoutThreadManaging: IPrefixSum
    {
        private int[] preProcessArray(int[] array)
        {
            var result = (int[])array.Clone();
            var n = result.Length;
            if (!MathHelper.IsPowerOfTwo(n)) {
                var populateTo = (int)(Math.Pow(2, ((int)Math.Log(n, 2) + 1)));
                result = result.Concat(Enumerable.Repeat<int>(0, populateTo - n)).ToArray();
            }
            return result;
        }

        private void UpSweepPhase(int[] array)
        {
            for(var depth = 0; depth < (Math.Log(array.Length-1, 2)); depth++)
            {
                var tasks = new List<Task>();
                for (int i = 0; i < array.Length - 1; i+= (int)Math.Pow(2, depth + 1))
                {
                    var resultI = (int)(i + Math.Pow(2, depth + 1) - 1);
                    var firstI = (int)(i + Math.Pow(2, depth) - 1);
                    var task = Task.Factory.StartNew(() => {
                        array[resultI] += array[firstI];
                    });
                    tasks.Add(task);
                }
                Task.WaitAll(tasks.ToArray());
            }
        }

        private void DownSweepPhase(int[] array)
        {
            array[array.Length - 1] = 0;
            for (var depth = (int)Math.Log(array.Length - 1, 2); depth >= 0; --depth)
            {
                var tasks = new List<Task>();
                for (var i = 0; i < array.Length - 1; i += (int)Math.Pow(2, depth + 1))
                {
                    var subI = (int)(i + Math.Pow(2, depth) - 1);
                    var firstI = (int)(i + Math.Pow(2, depth + 1) - 1);
                    var task = Task.Factory.StartNew(() =>
                    {
                        var sub = array[subI];
                        array[subI] = array[firstI];
                        array[firstI] += sub;
                    });
                    tasks.Add(task);
                }
                Task.WaitAll(tasks.ToArray());
            }
        }

        private int[] afterProcessing(int[] input, int[] result)
        {
            if (input.Length == result.Length)
            {
                var toConcat = Enumerable.Repeat(result.Last() + input.Last(), 1);
                return result.Concat(toConcat).ToArray();
            }
            else
            {
                return result.Take(input.Length + 1).ToArray();
            }
        }

        public int[] GetPrefixSum(int[] array)
        {
            var initialSize = array.Length;
            var result = preProcessArray(array);
            UpSweepPhase(result);
            DownSweepPhase(result);
            return afterProcessing(array, result);
        }
    }
}
