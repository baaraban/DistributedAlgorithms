using MapReduce.Interfaces;
using System;
using System.Collections.Generic;

namespace MapReduce.Implementation
{
    public class MapReducer<C, KeyType, ValueType, ReduceType> where C : class,
        IMapper<C, KeyType, ValueType>,
        IReducer<KeyType, ValueType, ReduceType>
    {
        public IDictionary<KeyType, IEnumerable<ValueType>> Map(IEnumerable<C> container)
        {
            return null;
        }
        public KeyValuePair<KeyType, ReduceType> Reduce(KeyValuePair<KeyType, IEnumerable<ValueType>> pair)
        {
            throw new NotImplementedException();
        }
    }
}
