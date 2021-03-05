using Data;

namespace BusinessLogic
{
    public interface IQuery<TQueryResult>
    {
        public TQueryResult GetQueryResult();
    }
}
