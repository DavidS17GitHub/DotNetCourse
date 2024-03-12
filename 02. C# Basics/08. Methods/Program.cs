namespace _08._Methods;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(GetSum());
    }

    static private int GetSum() 
        // All methods called within a statics method (i.e `Main`) must be `static` as well
        // If, instead of declaring the array within the scope of the method, and instead we passed it as an argument
        // `static int GetSum(int[] intsToCompress)`
        // We would have created a Dynamic Method

        {
            int totalValue = 0;

            int[] intsToCompress = new int[] { 10, 15, 20, 25, 30, 12, 34 };

            foreach (int intForCompression in intsToCompress)
            {
                totalValue += intForCompression;
            }

            return totalValue; // The method MUST always return a value if its not `void`
        }
}
