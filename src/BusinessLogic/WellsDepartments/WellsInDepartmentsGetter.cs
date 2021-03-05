using Data;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class WellsInDepartmentsGetter : IQuery<IEnumerable<DepartmentWells>>
    {
        private readonly IDataProvider<Well> wellDataProvider;
        private readonly IDataProvider<Department> departmentDataProvider;

        public WellsInDepartmentsGetter(IDataProvider<Well> wellDataProvider, IDataProvider<Department> departmentDataProvider)
        {
            this.wellDataProvider = wellDataProvider;
            this.departmentDataProvider = departmentDataProvider;
        }

        public IEnumerable<DepartmentWells> GetQueryResult()
        {
            IEnumerable<Well> wells = wellDataProvider.GetItems();
            IEnumerable<Department> departments = departmentDataProvider.GetItems();
            Department[] departmentsArray = departments.ToArray();

            IEnumerable<DepartmentWells> wellsDepartments = Enumerable.Range(0, departmentsArray.Length)
                .Select(i => new DepartmentWells
                {
                    Department = departmentsArray[i].Name,
                    Wells = wells
                    .Where(well => IsInside(departmentsArray[i].X, departmentsArray[i].Y, departmentsArray[i].Radius, well.X, well.Y)).Select(d => d.Name)
                });

            DepartmentWells wellsWithoutDepartment =
                new DepartmentWells
                {
                    Department = "Неизвестное месторождение",
                    Wells = wells.Select(well => well.Name).Except(wellsDepartments.SelectMany(x => x.Wells))
                };

            IEnumerable<DepartmentWells> departmentsWithoutWells =
                departments.Select(department => department.Name)
                .Except(wellsDepartments.Select(x => x.Department))
                .Select(department => new DepartmentWells
                {
                    Department = department, 
                    Wells = null
                });

            return wellsDepartments.Union(departmentsWithoutWells.Append(wellsWithoutDepartment));
        }

        private bool IsInside(float departmentX, float departmentY, float departmentRadius, float? wellX, float? wellY)
            => ((wellX - departmentX) * (wellX - departmentX) +
               (wellY - departmentY) * (wellY - departmentY) <= departmentRadius * departmentRadius)
               == true;          
    }
}
