using Newtonsoft.Json;
using System.IO;

namespace SimplTestTask
{
    public class WriteQueryResultToJsonFile : ISaveQueryResult
    {
        public void Save<T>(T result, string fileName)
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(result);
                writer = new StreamWriter(fileName, false);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
