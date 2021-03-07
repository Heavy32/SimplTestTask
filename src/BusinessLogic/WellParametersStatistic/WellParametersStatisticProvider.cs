using System.Collections.Generic;
using System.Linq;
using Data;

namespace BusinessLogic
{
    public class WellParametersStatisticProvider : IQuery<IEnumerable<WellParametersStatisticValues>>
    {
        private readonly IDataProvider<WellParameter> wellParameterDataProvider;
        private readonly IDataProvider<Well> wellDataProvider;

        public WellParametersStatisticProvider(IDataProvider<WellParameter> wellParameterDataProvider, IDataProvider<Well> wellDataProvider)
        {
            this.wellParameterDataProvider = wellParameterDataProvider;
            this.wellDataProvider = wellDataProvider;
        }

        public IEnumerable<WellParametersStatisticValues> GetQueryResult()
        {
            IEnumerable<Well> wells = wellDataProvider.GetItems();
            IEnumerable<WellParameter> wellParameters = wellParameterDataProvider.GetItems();

            return wells
                .Where(well => well.Id >= 10 && well.Id <= 30)
                .Join(wellParameters,
                    well => well.Id,
                    wellParameter => wellParameter.WellId,
                    (well, wellParameter) => new { Well = well.Name, wellParameter.ParameterName, wellParameter.Value })
                    .GroupBy(group => group.Well)
                    .Select(group => new WellParametersStatisticValues
                    {
                        Name = group.Key,
                        Parameters = group
                            .GroupBy(g => g.ParameterName)
                            .Select(parameters => new ParametersValues
                            {
                                Name = parameters.Key,
                                Min = parameters.Select(p => p.Value).Min(),
                                Max = parameters.Select(p => p.Value).Max(),
                                Average = parameters.Select(p => p.Value).Average(),
                                Median = parameters.Select(p => p.Value).Median()
                            })
                    });
        }
    }
}
