using BusinessLogic;
using Data;

namespace SimplTestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var departmentDataProvider = new JsonDataProvider<Department>(@"../../../../../departments.json");
            var wellDataProvider = new JsonDataProvider<Well>(@"../../../../../wells.json");
            var wellParameterDataProvider = new JsonDataProvider<WellParameter>(@"../../../../../wellParameters.json");
            var writeQueryResultToFile = new WriteQueryResultToJsonFile();

            var uniqueWellParametersProvider = new UniqueWellParametersProvider(wellParameterDataProvider);
            var uniqueParametersNames = uniqueWellParametersProvider.GetQueryResult();
            writeQueryResultToFile.Save(uniqueParametersNames, "UniqueParametersNames.json");

            var wellParametersStatisticProvider = new WellParametersStatisticProvider(wellParameterDataProvider, wellDataProvider);
            var wellParametersStatistic = wellParametersStatisticProvider.GetQueryResult();
            writeQueryResultToFile.Save(wellParametersStatistic, "wellParametersStatistic.json");

            var wellsInDepartmentsGetter = new WellsInDepartmentsGetter(wellDataProvider, departmentDataProvider);
            var wellsInDepartments = wellsInDepartmentsGetter.GetQueryResult();
            writeQueryResultToFile.Save(wellsInDepartments, "wellsInDepartments.json");
        }
    }
}
