namespace _01._Models;

public class Computer
{
    // private string? _motherboard; 
    // // This is a field, a best practice is that it has to be private, the `_` and underscore is a naming convention
    // public string Motherboard {get{return _motherboard;} set{_motherboard = value;}}
    // // This is known as a property of the Class and not a field
    // // The `get` and `set` are special methods to retrieve the private fields
    public string Motherboard { get; set; }

    public int CPUCores { get; set; }
    // However, C# has made it easy for the devs and with declaring a property of a class, it will automatially create the private field.
    public bool HasWifi { get; set; }
    public bool HasLTE { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public string VideoCard { get; set; }
    // string properties might give errors if you do not declare the type as a nullable string with `?`

    /* You can avoid the nullable string in the class by declaring a constructor instead for the class*/
    public Computer() // constructors must be public
    {
        if (VideoCard == null)
        {
            VideoCard = "";
        }
        if (Motherboard == null)
        {
            Motherboard = "";
        }
    }
}
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
