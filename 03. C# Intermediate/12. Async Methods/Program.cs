namespace _12._Async_Methods;

class Program
{
    static async Task Main(string[] args) 
    // The main method should be asynchronous in order for it to wait for the answer of our async programs
    {
        Task firstTask = new Task(() => {
            Thread.Sleep(2500);
            Console.WriteLine("Task 1");
        });
        firstTask.Start();

        Task secondTask = ConsoleAfterDelayAsync("Text 2", 5000);

        ConsoleAfterDelay("Delay", 3750);

        Task thirdTask = ConsoleAfterDelayAsync("Text 3", 1250);

        await secondTask;
        Console.WriteLine("After the task was created"); // Up to this point the program would still not wait for the task to finish
        await firstTask; // We need an await statement for the system to wait, however the system will hang if we don't Start the task
        await thirdTask;
    }

    static void ConsoleAfterDelay(string text, int delayTime)
    {
        Thread.Sleep(delayTime);
        Console.WriteLine(text);
    }
    static async Task ConsoleAfterDelayAsync(string text, int delayTime)
    {
        await Task.Delay(delayTime);
        Console.WriteLine(text);
    }
}
