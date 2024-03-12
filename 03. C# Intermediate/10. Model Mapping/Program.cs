using System.Diagnostics;
using System.Text.Json;
using _10._Model_Mapping.Data;
using _10._Model_Mapping.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace _10._Model_Mapping;

class Program
{
    static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        DataContextDapper dapper = new DataContextDapper(config);

        string computersJson = File.ReadAllText("ComputersSnake.json");

        // We don't use the mapper, but instead we can add an [Attribute] to the Model's Property

        Mapper mapper = new Mapper(new MapperConfiguration((cfg) => {
            cfg.CreateMap<ComputerSnake, Computer>()
                .ForMember(destination => destination.ComputerId, options =>
                    options.MapFrom(source => source.computer_id))
                .ForMember(destination => destination.Motherboard, options =>
                    options.MapFrom(source => source.motherboard))
                .ForMember(destination => destination.CPUCores, options =>
                    options.MapFrom(source => source.cpu_cores))
                .ForMember(destination => destination.HasWifi, options =>
                    options.MapFrom(source => source.has_wifi))
                .ForMember(destination => destination.HasLTE, options =>
                    options.MapFrom(source => source.has_lte))
                .ForMember(destination => destination.ReleaseDate, options =>
                    options.MapFrom(source => source.release_date))
                .ForMember(destination => destination.Price, options =>
                    options.MapFrom(source => source.price))
                .ForMember(destination => destination.VideoCard, options =>
                    options.MapFrom(source => source.video_card /* * 0.8m // We can use Automapper to convert values as well */));
        }));

        IEnumerable<ComputerSnake>? computerSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJson);

        if (computerSystem != null)
        {
            IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computerSystem);
            Console.WriteLine("Auto Mapper Count: " + computerResult.Count());
            // foreach(Computer computer in computerResult)
            // {
            //     Console.WriteLine(computer.Motherboard);
            // }
        }

        IEnumerable<Computer>? computerJsonPropertyMapping = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);

        if (computerJsonPropertyMapping != null)
        {
            Console.WriteLine("JSON Property Count: " + computerJsonPropertyMapping.Count());
            // foreach(Computer computer in computerJsonPropertyMapping)
            // {
            //     Console.WriteLine(computer.Motherboard);
            // }
        }

    }
}
