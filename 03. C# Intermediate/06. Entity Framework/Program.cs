using _06._Entity_Framework.Data;
using _06._Entity_Framework.Models;

namespace _06._Entity_Framework;

class Program
{
    static void Main(string[] args)
    {
        DataContextDapper dapper = new DataContextDapper();
        DataContextEF entityFramework = new DataContextEF();
        
        string sqlCommand = "SELECT GETDATE()";

        DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);

        Console.WriteLine(rightNow);

        Computer myComputer = new Computer() 
        {
            Motherboard = "Z690",
            CPUCores = 4,
            HasWifi = true,
            HasLTE = false,
            ReleaseDate = DateTime.Now,
            Price = 943.87m,
            VideoCard = "RTX 2060"

        };

        entityFramework.Add(myComputer);
        entityFramework.SaveChanges();

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

        // int result = dapper.ExecuteSqlWithRowCount(sql); // In case we need the return type as an int and the number of rows affected
        bool result = dapper.ExecuteSql(sql);

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

        IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

        Console.WriteLine("'Computer Id','Motherboard','CPUCores','HasWifi','HasLTE','ReleaseDate','Price','VideoCard'");

        foreach(Computer singleComputer in computers)
        {
            Console.WriteLine("'" + singleComputer.ComputerId 
                + "','" + singleComputer.Motherboard
                + "','" + singleComputer.CPUCores
                + "','" + singleComputer.HasWifi
                + "','" + singleComputer.HasLTE
                + "','" + singleComputer.ReleaseDate
                + "','" + singleComputer.Price
                + "','" + singleComputer.VideoCard
        + "'");
        }

        // Using Entity Framework

        IEnumerable<Computer>? computersEf = entityFramework.Computer?.ToList<Computer>();

        Console.WriteLine("'Computer Id','Motherboard','CPUCores','HasWifi','HasLTE','ReleaseDate','Price','VideoCard'");

        if (computersEf != null)
        {
            foreach(Computer singleComputer in computersEf)
            {
                Console.WriteLine("'" + singleComputer.ComputerId 
                    + "','" + singleComputer.Motherboard
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
}
