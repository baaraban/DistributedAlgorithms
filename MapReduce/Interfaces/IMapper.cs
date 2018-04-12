using System;
using System.Collections.Generic;

namespace MapReduce.Interfaces
{
    public interface IMapper<C, KeyType, ValueType> where C : class
    {
        IDictionary<KeyType, IEnumerable<ValueType>> Map(IEnumerable<C> container);
    }
}
