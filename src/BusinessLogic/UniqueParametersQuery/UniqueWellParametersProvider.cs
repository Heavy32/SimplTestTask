using Data;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class UniqueWellParametersProvider : IQuery<IEnumerable<string>>
    {
        private readonly IDataProvider<WellParameter> wellDataProvider;

        public UniqueWellParametersProvider(IDataProvider<WellParameter> wellDataProvider)
        {
            this.wellDataProvider = wellDataProvider;
        }

        public IEnumerable<string> GetQueryResult()
            => wellDataProvider.GetItems()
                .GroupBy(wellParameter => wellParameter.ParameterName)
                .Select(group => group.Key);
    }
}
