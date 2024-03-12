using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace _03._Database_Connections;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

        IDbConnection dbConnection = new SqlConnection(connectionString);
        // With this you create the connection and it needs the System.Data namespace and Microsoft.Data.SqlClient; for the object

        string sqlCommand = "SELECT GETDATE()";

        DateTime rightNow = dbConnection.QuerySingle<DateTime>(sqlCommand); // Dapper namespace needed for Query

        Console.WriteLine(rightNow);
    }
}
