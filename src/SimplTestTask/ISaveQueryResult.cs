namespace SimplTestTask
{
    public interface ISaveQueryResult
    {
        public void Save<T>(T result, string fileName);
    }
}
