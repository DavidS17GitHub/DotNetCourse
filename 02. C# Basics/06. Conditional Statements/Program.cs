namespace _06._Conditional_Statements;

class Program
{
    static void Main(string[] args)
    {
// CONDITIONAL
        int myInt = 5;
        int mySecondInt = 10;

        if (myInt > mySecondInt) // False
        {
            myInt += 10;
            Console.WriteLine(myInt); // 5
        }
        else if (myInt < mySecondInt) // True
        {
            myInt += 10;
        }

        Console.WriteLine(myInt); // 15

// SWITCH STATEMENT

// Comparison against constant values

        string myCow = "cow";

        switch (myCow)
        {
            case "cows":
                Console.WriteLine("Lowercase");
                break; // It will tell that here is the end of the logic and not move into other switch cases

            // case cow: // You cannot repeat cases

            case "Cow":
                Console.WriteLine("Capitalized");
                break;

            default: // Default case, do not use the word `case` before `default`
                Console.WriteLine("Deafult Run");
                break;
        }


    }
}
