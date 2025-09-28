using WebApiDemo.DTO;

namespace WebApiDemo.Services.SEmployee
{
    public interface IEmployee
    {
        public Object Insert(string position, string fullname, int? managerId = null);
        public Object UpdateEmployee(UpdateEmployeeDto employeeDto);

        public Object GetById(int employeedId);
        public List<EmployeeNode> EmployeeHierarchy(int? rootEmployeeId = null);
        public List<EmployeeNode> BuildHierarchy(List<EmployeeNode> employeesList);
    }
}
