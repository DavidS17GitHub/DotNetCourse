namespace _11._Tasks;

class Program
{
    static async Task Main(string[] args) 
    // The main method should be asynchronous in order for it to wait for the answer of our async programs
    {
        Task firstTask = new Task(() => {
            Thread.Sleep(5000);
            Console.WriteLine("Task 1");
        });
        firstTask.Start();

        Console.WriteLine("After the task was created"); // Up to this point the program would still not wait for the task to finish
        await firstTask; // We need an await statement for the system to wait, however the system will hang if we don't Start the task
    }
}
