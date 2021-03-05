using System.Collections.Generic;

namespace Data
{
    public interface IDataProvider<T>
    {
        public IEnumerable<T> GetItems();
    }
}
