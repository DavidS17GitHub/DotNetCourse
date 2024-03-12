using System.Data;
using _04._Dapper.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace _04._Dapper;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";

        IDbConnection dbConnection = new SqlConnection(connectionString);
        
        string sqlCommand = "SELECT GETDATE()";

        DateTime rightNow = dbConnection.QuerySingle<DateTime>(sqlCommand);

        Console.WriteLine(rightNow);

        Computer myComputer = new Computer() 
        // You'wll get an error if the Constructor is not set as public
        // 'Computer.Computer()' is inaccessible due to its protection level
        {
            Motherboard = "Z690", //commas needed to separate
            CPUCores = 4,
            HasWifi = true,
            HasLTE = false,
            ReleaseDate = DateTime.Now,
            Price = 943.87m,
            VideoCard = "RTX 2060"

        };

        string sql = @"INSERT INTO [TutorialAppSchema].[Computer] (
            Motherboard,
            CPUCores,
            HasWifi,
            HasLTE,
            ReleaseDate,
            Price,
            VideoCard
        ) VALUES ('" + myComputer.Motherboard 
                + "','" + myComputer.CPUCores
                + "','" + myComputer.HasWifi
                + "','" + myComputer.HasLTE
                + "','" + myComputer.ReleaseDate
                + "','" + myComputer.Price
                + "','" + myComputer.VideoCard
        + "')";

        /*
        INSERT INTO [TutorialAppSchema].[Computer] (
            Motherboard,
            CPUCores,
            HasWifi,
            HasLTE,
            ReleaseDate,
            Price,
            VideoCard
        ) VALUES ('Z690','4','True','False','3/11/2024 5:45:21 PM','943.87','RTX 2060')
        */

        Console.WriteLine(sql);

        int result = dbConnection.Execute(sql); 
        // This returns the number of rows affected by the statement, and that's why we set it up as an `int`

        Console.WriteLine(result);

        string sqlSelect = @"
        SELECT
            [Computer].[ComputerId]
            ,[Computer].[Motherboard]
            ,[Computer].[CPUCores]
            ,[Computer].[HasWifi]
            ,[Computer].[HasLTE]
            ,[Computer].[ReleaseDate]
            ,[Computer].[Price]
            ,[Computer].[VideoCard]
        FROM [DotNetCourseDatabase].[TutorialAppSchema].[Computer]";

        IEnumerable<Computer> computers = dbConnection.Query<Computer>(sqlSelect);
        // It's really important that this is an IEnumerable because that is the return type that we are expecting
        // from the `Query` call, `Query` returns an Ienumerable of the <T> (Type).
        // If we were to return a list, then a explicit conversion is necessary:
        // List<Computer> computers = dbConnection.Query<Computer>(sqlSelect).ToList();

        Console.WriteLine("'Motherboard','CPUCores','HasWifi','HasLTE','ReleaseDate','Price','VideoCard'");

        foreach(Computer singleComputer in computers)
        {
            Console.WriteLine("'" + singleComputer.Motherboard 
                + "','" + singleComputer.CPUCores
                + "','" + singleComputer.HasWifi
                + "','" + singleComputer.HasLTE
                + "','" + singleComputer.ReleaseDate
                + "','" + singleComputer.Price
                + "','" + singleComputer.VideoCard
        + "'");
        }
    }
}
