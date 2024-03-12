using _02._Namespaces.Models;

namespace _02._Namespaces;

class Program
{
    static void Main(string[] args)
    {
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

        }; // Semicolon at the end of the constructor

        Console.WriteLine(myComputer.Motherboard);
        Console.WriteLine(myComputer.CPUCores);
        Console.WriteLine(myComputer.HasWifi);
        Console.WriteLine(myComputer.HasLTE);
        Console.WriteLine(myComputer.ReleaseDate);
        Console.WriteLine(myComputer.Price);
        Console.WriteLine(myComputer.VideoCard);
    }
}
