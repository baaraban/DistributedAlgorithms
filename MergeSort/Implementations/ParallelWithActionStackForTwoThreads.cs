using MergeSort.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MergeSort.Implementations
{
    public class ParallelWithActionStackForTwoThreads : IMergeSorter
    {
        private Stack<Action> actionStack = new Stack<Action>();
        private void merge(int[] array, int left, int leftEnd, int right)
        {
            var leftAmount = leftEnd - left + 1;
            var rightAmount = right - leftEnd;
            var l = array.Skip(left).Take(leftAmount).ToArray();
            var r = array.Skip(leftEnd + 1).Take(rightAmount).ToArray();
            var leftIndex = 0;
            var rightIndex = 0;
            var globalIndex = left;
            while (globalIndex <= right)
            {
                int toPut;
                if (l[leftIndex] < r[rightIndex])
                {
                    toPut = l[leftIndex++];
                }
                else
                {
                    toPut = r[rightIndex++];
                }
                array[globalIndex++] = toPut;
                if (rightIndex == rightAmount)
                {
                    for (var i = leftIndex; i < leftAmount; ++i)
                    {
                        array[globalIndex++] = l[i];
                    }
                    break;
                }
                if (leftIndex == leftAmount)
                {
                    for (var i = rightIndex; i < rightAmount; ++i)
                    {
                        array[globalIndex++] = r[i];
                    }
                    break;
                }
            }
        }

        private void internalSort(int[] array, int from, int to)
        {
            {
                if (from == to)
                {
                    return;
                }
                var leftEnd = (int)((from + to) / 2);
                internalSort(array, from, leftEnd);
                internalSort(array, leftEnd + 1, to);
                merge(array, from, leftEnd, to);
            }
        }

        private void formStack(int[] array, int from, int to)
        { 
            var leftEnd = (int)((from + to) / 2);
            actionStack.Push(() => internalSort(array, from, leftEnd));
            actionStack.Push(() => internalSort(array, leftEnd + 1, to));
            actionStack.Push(() => merge(array, from, leftEnd, to));
            if(from != leftEnd)
            {
                formStack(array, from, leftEnd);
            }
            if(leftEnd + 1 != to)
            {
                formStack(array, leftEnd + 1, to);
            }
        }

        private void executeStack()
        {
            while (!(this.actionStack.Count == 0))
            {

                var task1 = Task.Factory.StartNew(() => actionStack?.Pop()?.Invoke());

                if (actionStack.Count == 0)
                {
                    break;
                }

                var task2 = Task.Factory.StartNew(() => actionStack?.Pop()?.Invoke());
                Task.WaitAll(task1, task2);
                if (actionStack.Count != 0)
                {
                    actionStack?.Pop()?.Invoke();
                }

            }
        }

        public int[] MergeSort(int[] array)
        {
            var ar = (int[])array.Clone();
            formStack(ar, 0, ar.Length - 1);
            executeStack();
            return ar;
        }
    }
}
