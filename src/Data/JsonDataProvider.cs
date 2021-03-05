using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Data
{
    public class JsonDataProvider<T> : IDataProvider<T>
    {
        private readonly string filePath;

        public JsonDataProvider(string filePath)
        {
            this.filePath = filePath;
        }

        public IEnumerable<T> GetItems()
        {
            using StreamReader reader = new StreamReader(filePath);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(reader.ReadToEnd());
        }
    }
}
