using Helpers;
using Infrastructure.Managers.Interfaces;
using PrefixSum.Interfaces;
using System;
using System.Linq;

namespace PrefixSum.Implementations
{
    public class PrefixSummatorWithManager: IPrefixSum
    {
        private IParallelActionQueue manager;

        private int[] preProcessArray(int[] array)
        {
            var result = (int[])array.Clone();
            var n = result.Length;
            if (!MathHelper.IsPowerOfTwo(n))
            {
                var populateTo = (int)(Math.Pow(2, ((int)Math.Log(n, 2) + 1)));
                result = result.Concat(Enumerable.Repeat<int>(0, populateTo - n)).ToArray();
            }
            return result;
        }

        private void UpSweepPhase(int[] array)
        {
            for (var depth = 0; depth < (Math.Log(array.Length - 1, 2)); depth++)
            {
                manager.Start();
                for (int i = 0; i < array.Length - 1; i += (int)Math.Pow(2, depth + 1))
                {
                    var resultI = (int)(i + Math.Pow(2, depth + 1) - 1);
                    var firstI = (int)(i + Math.Pow(2, depth) - 1);
                    Action a = () =>
                    {
                        array[resultI] += array[firstI];
                    };
                    manager.ScheduleAction(a);
                }
                manager.SynchronizeQueue();
            }
        }

        private void DownSweepPhase(int[] array)
        {
            array[array.Length - 1] = 0;
            for (var depth = (int)Math.Log(array.Length - 1, 2); depth >= 0; --depth)
            {
                manager.Start();
                for (var i = 0; i < array.Length - 1; i += (int)Math.Pow(2, depth + 1))
                {
                    var subI = (int)(i + Math.Pow(2, depth) - 1);
                    var firstI = (int)(i + Math.Pow(2, depth + 1) - 1);
                    Action a = () =>
                    {
                        var sub = array[subI];
                        array[subI] = array[firstI];
                        array[firstI] += sub;
                    };
                    this.manager.ScheduleAction(a);
                }
                manager.SynchronizeQueue();
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

        public PrefixSummatorWithManager(IParallelActionQueue manager)
        {
            this.manager = manager;
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
