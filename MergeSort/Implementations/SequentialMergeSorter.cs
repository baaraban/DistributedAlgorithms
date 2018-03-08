using MergeSort.Interfaces;
using System.Linq;

namespace MergeSort.Implementations
{
    class SequentialMergeSorter : IMergeSorter
    {
        private void merge(int[] array, int left, int leftEnd, int right)
        {
            var leftAmount = leftEnd - left + 1;
            var rightAmount = right - leftEnd;
            var l = array.Skip(left).Take(leftAmount).ToArray();
            var r = array.Skip(leftEnd + 1).Take(rightAmount).ToArray();
            var leftIndex = 0;
            var rightIndex = 0;
            var globalIndex = left;
            while(globalIndex <= right)
            {
                int toPut;
                if(l[leftIndex] < r[rightIndex])
                {
                    toPut = l[leftIndex++];
                } else
                {
                    toPut = r[rightIndex++];
                }
                array[globalIndex++] = toPut;
                if(rightIndex == rightAmount)
                {
                    for(var i = leftIndex; i < leftAmount; ++i)
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
            if(from == to)
            {
                return;
            }
            var leftEnd = (int)((from + to)/2);
            internalSort(array, from, leftEnd);
            internalSort(array, leftEnd + 1, to);
            merge(array, from, leftEnd, to);
        }

        public int[] MergeSort(int[] array)
        {
            var ar = (int[])array.Clone();
            internalSort(ar, 0, ar.Length - 1);
            return ar;
        }
    }
}
