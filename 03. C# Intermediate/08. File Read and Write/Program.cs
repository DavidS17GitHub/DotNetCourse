using _08._File_Read_and_Write.Data;
using _08._File_Read_and_Write.Models;
using Microsoft.Extensions.Configuration;

namespace _08._File_Read_and_Write;

class Program
{
    static void Main(string[] args)
    {
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
        + "')\n";

        File.WriteAllText("log.txt", sql);

        using StreamWriter openFile = new("log.txt", append: true);

        openFile.WriteLine(sql);

        openFile.Close(); // You need to release the access to a file in order to read it afterwards

        string filesText = File.ReadAllText("log.txt");

        Console.WriteLine(filesText);
    }
}