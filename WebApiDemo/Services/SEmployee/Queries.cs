using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using WebApiDemo.DTO;
using WebApiDemo.Models;

namespace WebApiDemo.Services.SEmployee
{
    public static class Queries
    {
        public static (string, List<SqlParameter>) InsertEmployee(string position, string fullname, int? managerID = null)
        {
            string procedure = @$"dbo.sp_InsertEmployee";

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@Position", SqlDbType.NVarChar, 100)
            {
                Value = position ?? (object)DBNull.Value
            });

            parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 150)
            {
                Value = fullname ?? (object)DBNull.Value
            });

            parameters.Add(new SqlParameter("@ManagerId", SqlDbType.Int)
            {
                Value = managerID
            });

            return (procedure, parameters);
        }


        public static (string, List<SqlParameter>) GetById(int? employeeId = null)
        {
            string procedure = @$"dbo.sp_GetEmployeesById";

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int)
            {
                Value = employeeId
            });

            return (procedure, parameters);
        }


        public static (string, List<SqlParameter>) UpdateEmployee(UpdateEmployeeDto employeeDto)
        {
            string procedure = @$"dbo.sp_UpdateEmployee";

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int)
            {
                Value = (object?)employeeDto.EmployeeId ?? DBNull.Value
            });

            parameters.Add(new SqlParameter("@Position", SqlDbType.NVarChar, 100)
            {
                Value = employeeDto.Position ?? (object)DBNull.Value
            });

            parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 150)
            {
                Value = employeeDto.FullName ?? (object)DBNull.Value
            });

            parameters.Add(new SqlParameter("@ManagerId", SqlDbType.Int)
            {
                Value = (object?)employeeDto.ManagerId ?? DBNull.Value
            });

            parameters.Add(new SqlParameter("@IsEnabled", SqlDbType.Int)
            {
                Value = (object?)employeeDto.IsEnabled ?? DBNull.Value
            });

            return (procedure, parameters);
        }

        public static (string, List<SqlParameter>) GetEmployeeHierarchy(int? rootEmployeeId = null)
        {
            string procedure = @$"dbo.sp_GetEmployeeHierarchy";

            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@RootEmployeeId", SqlDbType.Int)
            {
                Value = rootEmployeeId
            }); 

            return (procedure, parameters);
        }
    }
}
