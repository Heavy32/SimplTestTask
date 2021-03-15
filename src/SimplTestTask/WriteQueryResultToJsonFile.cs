using Newtonsoft.Json;
using System;
using System.IO;

namespace SimplTestTask
{
    public class WriteQueryResultToJsonFile : ISaveQueryResult
    {
        public void Save<T>(T result, string fileName)
        {
            using StreamWriter writer = new StreamWriter(fileName, false);
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(result);
                writer.Write(contentsToWriteToFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
