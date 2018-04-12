using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapReduce.Interfaces
{
    public interface IReducer<KeyType, ValueType, ReturnType>
    {
        KeyValuePair<KeyType, ReturnType> Reduce(KeyValuePair<KeyType, IEnumerable<ValueType>> pair);
    }
}
