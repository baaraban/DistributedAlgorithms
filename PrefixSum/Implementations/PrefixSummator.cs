using PrefixSum.Interfaces;

namespace PrefixSum.Implementations
{
    public class PrefixSummator: IPrefixSum
    {
        public int[] GetPrefixSum(int[] array)
        {
            var result = new int[array.Length + 1];
            var currentSum = 0;
            result[0] = currentSum;
            for(var i = 1; i < result.Length; ++i)
            {
                currentSum += array[i - 1];
                result[i] = currentSum;
            }
            return result;
        }
    }
}
