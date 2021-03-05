using System.Collections.Generic;

namespace BusinessLogic
{
    public class WellParametersStatisticValues
    {
        public string Name { get; set; }
        public IEnumerable<ParametersValues> Parameters { get; set; }
    }
}
