using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;
using System;
using System.Data;
using WebApiDemo.Models;

namespace WebApiDemo.Helpers
{
    public class SqlServerHelper
    {
        public SqlServerHelper()
        {
        }

        public DataTable? ExecuteQuery(string query, List<SqlParameter> parameters, SqlServerParams sqlParams)
        {
            using (SqlConnection connection = new SqlConnection(sqlParams.GetConnectionString()))
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand
                {
                    CommandTimeout = 30,
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = query.Trim()
                };

                parameters?.ForEach(p => sqlCommand.Parameters.Add(p));

                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
                DataSet result = new DataSet();
                da.Fill(result);
                da.Dispose();

                connection.Close();

                if (result.Tables.Count == 0) return null;
                return result.Tables[0];
            }
        }

        public DataSet? ExecuteProcedure(string procedure, List<SqlParameter> parameters, SqlServerParams sqlParams, string[,] tables)
        {
            using (SqlConnection connection = new SqlConnection(sqlParams.GetConnectionString()))
            {
                connection.Open();

                SqlCommand sqlCommand = new SqlCommand
                {
                    CommandTimeout = 30,
                    Connection = connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = procedure
                };

                parameters.ForEach(p => sqlCommand.Parameters.Add(p));

                SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

                for (int i = 0; i < tables.Length / 2; i++)
                {
                    da.TableMappings.Add(tables[i, 0], tables[i, 1]);
                }

                DataSet result = new DataSet();
                da.Fill(result);

                da.Dispose();
                connection.Close();

                if (result.Tables.Count == 0) return null;
                return result;
            }
        }

    }
}