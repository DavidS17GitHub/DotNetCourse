using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace _06._Entity_Framework.Data
{
    public class DataContextDapper
    {
        private string _connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

        public IEnumerable<T> LoadData<T>(string sql) // The method will need the <T> as well, because you need to define the type for when the method is called
        // If we used IEnumerable<Computer> then it would only return `Computer` objects, but
        // what if it's not always `Computer`, we want to do this dynamic and for that reason we use Generics
        // We use Generics so we can use Dynamic Types, and hence we use instead the Capital Letter T: <T>
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sql);
        }
        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingle<T>(sql);
        }
        public bool ExecuteSql(string sql) 
        // The return type is not necessary but it is better to have it in place, even better if we use an `int` to know how many rows were affected
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql) > 0;
        }
        public int ExecuteSqlWithRowCount(string sql) 
        // The return type is not necessary but it is better to have it in place, even better if we use an `int` to know how many rows were affected
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sql);
        }
    }
}