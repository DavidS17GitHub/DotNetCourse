using System.Text.Json;
using _09._JSON.Data;
using _09._JSON.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace _09._JSON;

class Program
{
    static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        DataContextDapper dapper = new DataContextDapper(config);
        
        // string sql = @"INSERT INTO [TutorialAppSchema].[Computer] (
        //     Motherboard,
        //     CPUCores,
        //     HasWifi,
        //     HasLTE,
        //     ReleaseDate,
        //     Price,
        //     VideoCard
        // ) VALUES ('" + myComputer.Motherboard 
        //         + "','" + myComputer.CPUCores
        //         + "','" + myComputer.HasWifi
        //         + "','" + myComputer.HasLTE
        //         + "','" + myComputer.ReleaseDate
        //         + "','" + myComputer.Price
        //         + "','" + myComputer.VideoCard
        // + "')\n";

        // File.WriteAllText("log.txt", sql);

        // using StreamWriter openFile = new("log.txt", append: true);

        // openFile.WriteLine(sql);

        // openFile.Close(); // You need to release the access to a file in order to read it afterwards

        string computersJson = File.ReadAllText("Computers.json");

        // Console.WriteLine(computersJson);

        // JsonSerializerOptions options = new JsonSerializerOptions()
        // {
        //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        // };
        // //The following snippet of code will not work because the JSON object has camelCase, while the object properties are written in PascalCase

        // IEnumerable<Computer>? computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, /*Add the `options` here*/ options);

        // if (computers != null)
        // {
        //     foreach(Computer computer in computers)
        //     {
        //         Console.WriteLine(computer.Motherboard);
        //     }
        // }

        IEnumerable<Computer>? computers = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

        if (computers != null)
        {
            foreach(Computer computer in computers)
            {
                // Console.WriteLine(computer.Motherboard);
                string sql = @"INSERT INTO [TutorialAppSchema].[Computer] (
                    Motherboard,
                    CPUCores,
                    HasWifi,
                    HasLTE,
                    ReleaseDate,
                    Price,
                    VideoCard
                ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
                        + "','" + computer.CPUCores
                        + "','" + computer.HasWifi
                        + "','" + computer.HasLTE
                        + "','" + computer.ReleaseDate
                        + "','" + computer.Price
                        + "','" + EscapeSingleQuote(computer.VideoCard)
                + "')\n";

                dapper.ExecuteSql(sql);
            }
        }

        // Using Newtonsoft

        JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver() // We do this to avoid returning a Json file with PascalCase
        };

        string computersCopyNewtonsoft = JsonConvert.SerializeObject(computers, settings);

        File.WriteAllText("computersCopyNewtonsoft.txt", computersCopyNewtonsoft);

        // Using System

        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase // We do this to avoid returning a Json file with PascalCase
        };

        string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computers, options);

        File.WriteAllText("computersCopySystem.txt", computersCopySystem);
    }

    // We need to create a method to clean the data incoming from the Users

    static string EscapeSingleQuote(string input)
    {
        string output = input.Replace("'", "''");

        return output;
    }
}