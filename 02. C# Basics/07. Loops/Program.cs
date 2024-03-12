namespace _07._Loops;

class Program
{
    static void Main(string[] args)
    {
        int[] intsToCompress = new int[] { 10, 15, 20, 25, 30, 12, 34 };

        // Let's calculate how much does it take in time

        DateTime startTime = DateTime.Now;

        int totalValue = intsToCompress[0] + intsToCompress[1]
                + intsToCompress[2] + intsToCompress[3]
                + intsToCompress[4] + intsToCompress[5]
                + intsToCompress[6];

        Console.WriteLine((DateTime.Now - startTime).TotalSeconds); // 0.0035

        Console.WriteLine(totalValue); // 146

// FOR LOOP

        // We can accomplish this with a For Loop

        totalValue = 0;

        startTime = DateTime.Now;

        for (int i = 0; i < intsToCompress.Length; i++)
        {
            totalValue += intsToCompress[i];
        }

        Console.WriteLine((DateTime.Now - startTime).TotalSeconds); // 0.0000004 Faster

        Console.WriteLine(totalValue); // 146

// FOR EACH LOOP

        // This is the most used construc to iterate over due to its speed

        totalValue = 0;

        startTime = DateTime.Now;

        foreach (int intForCompression in intsToCompress)
        {
            totalValue += intForCompression;
        }

        Console.WriteLine((DateTime.Now - startTime).TotalSeconds); // 0.0000003 Almost twice as fast as the For Loop

        Console.WriteLine(totalValue); // 146

// WHILE LOOP

        int index = 0; // A variable needs to be declared first out of teh scope of the while loop

        totalValue = 0;

        startTime = DateTime.Now;

        while(index < intsToCompress.Length)
        {
            totalValue += intsToCompress[index];
            index++;
        }

        Console.WriteLine((DateTime.Now - startTime).TotalSeconds); 
        
        // 0.0000001 Almost twice as fast as the For Each Loop, but less readable as a for each loop

        Console.WriteLine(totalValue); // 146

// DO WHILE LOOP

        index = 0;

        totalValue = 0;

        startTime = DateTime.Now;

        do // It will run at least once before checking the while condition
        {
            totalValue += intsToCompress[index];
            index++;
        } 
        while(index < intsToCompress.Length);

        Console.WriteLine((DateTime.Now - startTime).TotalSeconds);

        // 0.0000001 Same performance as the while loop, but in reality this one is faster

        Console.WriteLine(totalValue); // 146



        //// Explanation of a while loop ////

        // index = 0; // We will keep the same value from the previous loop

        totalValue = 0;

        do // It will run at least once before checking the while condition
        {
            // totalValue += intsToCompress[index];
            Console.WriteLine(index);
            Console.WriteLine(index < intsToCompress.Length); // Even though the condition is false, the loop will run once and stop when it reaches the while loop
            index++;
        } 
        while(index < intsToCompress.Length);

// SUM METHOD

        totalValue = 0;

        startTime = DateTime.Now;

        totalValue = intsToCompress.Sum();

        Console.WriteLine((DateTime.Now - startTime).TotalSeconds);

        // 0.0046 Not as fast

        Console.WriteLine(totalValue); // 146
    }
}
