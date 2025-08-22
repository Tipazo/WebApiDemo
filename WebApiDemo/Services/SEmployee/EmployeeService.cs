using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApiDemo.DTO;
using WebApiDemo.Helpers;
using WebApiDemo.Models;

namespace WebApiDemo.Services.SEmployee
{
    public class EmployeeService : IEmployee
    {

        SqlServerHelper sqlServerHelper = new SqlServerHelper();
        SqlServerParams sqlParams = new SqlServerParams();

        public EmployeeService() {
            sqlParams.port = Int32.Parse(Environment.GetEnvironmentVariable("DB_PORT"));
            sqlParams.host = Environment.GetEnvironmentVariable("DB_HOST");
            sqlParams.user = Environment.GetEnvironmentVariable("DB_USER");
            sqlParams.password = Environment.GetEnvironmentVariable("DB_PASS");
            sqlParams.database = Environment.GetEnvironmentVariable("DB_DATABASE");
            sqlParams.trusted_connection = Boolean.Parse(Environment.GetEnvironmentVariable("DB_TRUSTED_CONNECTION"));
        }

        public Object Insert(string position, string fullname, int? managerID = null)
        {
            (string procedure, List<SqlParameter> parameters) = Queries.InsertEmployee(position, fullname, managerID);

            string[,] tables = new string[,]
            {
                { "Table", "Employee" }
            };

            Object employee = new Object();

            DataSet? ds = sqlServerHelper.ExecuteProcedure(procedure, parameters, sqlParams, tables);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];
                employee = new
                {
                    EmployeeId = (int)row["EmployeeId"],
                    Position   = row["Position"].ToString(),
                    FullName   = row["FullName"].ToString(),
                    ManagerId  = row["ManagerId"] as int?
                };
            }

            return employee;
        }
        public object GetById(int employeedId)
        {
            (string procedure, List<SqlParameter> parameters) = Queries.GetById(employeedId);

            string[,] tables = new string[,]
            {
                { "Table", "Employee" }
            };

            Object employee = new Object();

            DataSet? ds = sqlServerHelper.ExecuteProcedure(procedure, parameters, sqlParams, tables);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];
                employee = new
                {
                    EmployeeId = (int)row["EmployeeId"],
                    Position = row["Position"].ToString(),
                    FullName = row["FullName"].ToString(),
                    ManagerId = row["ManagerId"] as int?
                };
            }

            return employee;
        }

        public Object UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            (string procedure, List<SqlParameter> parameters) = Queries.UpdateEmployee(employeeDto);

            string[,] tables = new string[,]
            {
                { "Table", "Employee" }
            };

            Object employee = new Object();

            DataSet? ds = sqlServerHelper.ExecuteProcedure(procedure, parameters, sqlParams, tables);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];
                employee = new
                {
                    EmployeeId = (int)row["EmployeeId"],
                    Position = row["Position"].ToString(),
                    FullName = row["FullName"].ToString(),
                    ManagerId = row["ManagerId"] as int?
                };
            }

            return employee;
        }

        public List<EmployeeNode> GetEmployeeHierarchy(int? rootEmployeeId = null)
        {
            (string procedure, List<SqlParameter> parameters) = Queries.GetEmployeeHierarchy(rootEmployeeId);

            string[,] tables = new string[,]
            {
                { "Table", "Employees" }
            };

            List<EmployeeNode> employees = new List<EmployeeNode>();

            DataSet? ds = sqlServerHelper.ExecuteProcedure(procedure, parameters, sqlParams, tables);

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    employees.Add(new EmployeeNode
                    {
                        EmployeeId = row["EmployeeId"] != DBNull.Value ? (int?)row["EmployeeId"] : null,
                        Position = row["Position"].ToString() ?? string.Empty,
                        FullName = row["FullName"].ToString() ?? string.Empty,
                        Level = row["Level"] != DBNull.Value ? (int?)row["Level"] : null,
                        ManagerId = row["ManagerId"] != DBNull.Value ? (int?)row["ManagerId"] : null,
                        IsEnabled = row["IsEnabled"] != DBNull.Value ? (bool?)row["IsEnabled"] : true,
                    });
                }
            }

            return employees;
        }

        public List<EmployeeNode> BuildHierarchy(List<EmployeeNode> employeesList)
        {
            var lookup = employeesList.ToDictionary(e => e.EmployeeId);
            List<EmployeeNode> roots = new List<EmployeeNode>();

            foreach (var employee in employeesList)
            {
                if (employee.ManagerId != null && lookup.ContainsKey(employee.ManagerId.Value))
                {
                    lookup[employee.ManagerId.Value].Children.Add(employee);
                }
                else
                {
                    roots.Add(employee);
                }
            }

            return roots;
        }
    }
}
