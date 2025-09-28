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

        private const string TableKey = "Table";
        private const string FullNameKey = "FullName";
        private const string ManagerIdKey = "ManagerId";
        private const string PositionKey = "Position";
        private const string EmployeeIdKey = "EmployeeId";

        
        public EmployeeService() {
            sqlParams.port = Int32.Parse(Environment.GetEnvironmentVariable("DB_PORT"));
            sqlParams.host = Environment.GetEnvironmentVariable("DB_HOST");
            sqlParams.user = Environment.GetEnvironmentVariable("DB_USER");
            sqlParams.password = Environment.GetEnvironmentVariable("DB_PASS");
            sqlParams.database = Environment.GetEnvironmentVariable("DB_DATABASE");
            sqlParams.trusted_connection = Boolean.Parse(Environment.GetEnvironmentVariable("DB_TRUSTED_CONNECTION"));
        }

        public Object Insert(string position, string fullname, int? managerId = null)
        {
            (string procedure, List<SqlParameter> parameters) = Queries.InsertEmployee(position, fullname, managerId);

            string[,] tables = new string[,]
            {
                { TableKey, "Employee" }
            };

            Object employee = new Object();

            DataSet? ds = sqlServerHelper.ExecuteProcedure(procedure, parameters, sqlParams, tables);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];
                employee = new
                {
                    EmployeeId = (int)row[EmployeeIdKey],
                    Position   = row[PositionKey].ToString(),
                    FullName   = row[FullNameKey].ToString(),
                    ManagerId  = row[ManagerIdKey] as int?
                };
            }

            return employee;
        }
        public object GetById(int employeedId)
        {
            (string procedure, List<SqlParameter> parameters) = Queries.GetById(employeedId);

            string[,] tables = new string[,]
            {
                { TableKey, "Employee" }
            };

            Object employee = new Object();

            DataSet? ds = sqlServerHelper.ExecuteProcedure(procedure, parameters, sqlParams, tables);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];
                employee = new
                {
                    EmployeeId = (int)row[EmployeeIdKey],
                    Position = row[PositionKey].ToString(),
                    FullName = row[FullNameKey].ToString(),
                    ManagerId = row[ManagerIdKey] as int?
                };
            }

            return employee;
        }

        public Object UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            (string procedure, List<SqlParameter> parameters) = Queries.UpdateEmployee(employeeDto);

            string[,] tables = new string[,]
            {
                { TableKey, "Employee" }
            };

            Object employee = new Object();

            DataSet? ds = sqlServerHelper.ExecuteProcedure(procedure, parameters, sqlParams, tables);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var row = ds.Tables[0].Rows[0];
                employee = new
                {
                    EmployeeId = (int)row[EmployeeIdKey],
                    Position = row[PositionKey].ToString(),
                    FullName = row[FullNameKey].ToString(),
                    ManagerId = row[ManagerIdKey] as int?
                };
            }

            return employee;
        }

        public List<EmployeeNode> EmployeeHierarchy(int? rootEmployeeId = null)
        {
            (string procedure, List<SqlParameter> parameters) = Queries.GetEmployeeHierarchy(rootEmployeeId);
            string[,] tables = new string[,]
            {
        { TableKey, "Employees" }
            };

            List<EmployeeNode> employees = new List<EmployeeNode>();
            DataSet? ds = sqlServerHelper.ExecuteProcedure(procedure, parameters, sqlParams, tables);

            if (ds?.Tables.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    employees.Add(MapEmployeeNode(row));
                }
            }

            return employees;
        }

        public List<EmployeeNode> BuildHierarchy(List<EmployeeNode> employeesList)
        {
            List<EmployeeNode> roots = new List<EmployeeNode>();

            foreach (var employee in employeesList)
            {
                var lookup = employeesList.ToDictionary(e => e.EmployeeId);
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

        private EmployeeNode MapEmployeeNode(DataRow row)
        {
            return new EmployeeNode
            {
                EmployeeId = GetNullableInt(row, EmployeeIdKey),
                Position = row[PositionKey]?.ToString() ?? string.Empty,
                FullName = row[FullNameKey]?.ToString() ?? string.Empty,
                Level = GetNullableInt(row, "Level"),
                ManagerId = GetNullableInt(row, ManagerIdKey),
                IsEnabled = GetNullableBool(row, "IsEnabled") ?? true
            };
        }

        private int? GetNullableInt(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? (int?)row[columnName] : null;
        }

        private bool? GetNullableBool(DataRow row, string columnName)
        {
            return row[columnName] != DBNull.Value ? (bool?)row[columnName] : null;
        }

    }
}
