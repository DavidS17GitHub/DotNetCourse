namespace _06._Entity_Framework.Models
{
    public class Computer
    {
        public int ComputerId { get; set; } // Now we have a computer id property that matches the computerid in the Db table
        public string Motherboard { get; set; }

        public int CPUCores { get; set; }
        public bool HasWifi { get; set; }
        public bool HasLTE { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string VideoCard { get; set; }
        public Computer()
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
}