namespace _05._Operators_and_Conditionals;

class Program
{
    static void Main(string[] args)
    {
// NUMERIC OPERATORS:
        int myInt = 5;
        int mySecondInt = 10;

        Console.WriteLine(myInt); // 5

        myInt++;

        Console.WriteLine(myInt); // 6

        myInt += 7;

        Console.WriteLine(myInt); // 13

        myInt -= 8;

        Console.WriteLine(myInt); // 5

        Console.WriteLine(myInt * mySecondInt); // 50

        Console.WriteLine(mySecondInt / myInt); // 2

        Console.WriteLine(myInt + mySecondInt); // 15

        Console.WriteLine(myInt - mySecondInt); // -5

        // Order of operations in C# works under the PEMDAS (Parenthesis, Exponents, Multiplication, Division, Addition, Substraction) order

        Console.WriteLine(5 + 5 * 10); // 55

        Console.WriteLine((5 + 5) * 10); // 100

        Console.WriteLine(Math.Pow(5, 4)); // Exponents. Output: 625

        Console.WriteLine(Math.Sqrt(25)); // Square Root. Output: 5

// STRING OPERATORS:

        string myString = "Test";

        Console.WriteLine(myString); // Test

        myString += ". Second part"; // += Appends

        Console.WriteLine(myString); // Test. Second part

        myString = myString + ". Third part"; // + Appends

        Console.WriteLine(myString); // Test. Second part. Third part

        myString = myString + ". \"Forth\" part"; // \ Escaping charater

        Console.WriteLine(myString); // Test. Second part. Third part. "Forth" part"

        string[] myStringArray = myString.Split(" "); // Split Operator

// COMPARISONG OPERATORS

        myInt = 5;
        mySecondInt = 10;

        // Equals
        Console.WriteLine(myInt == mySecondInt); // False
        Console.WriteLine(myInt == mySecondInt / 2); // True

        // Not equals
        Console.WriteLine(myInt != mySecondInt); // True
        Console.WriteLine(myInt != mySecondInt / 2); // False

        // Greater, greater or iqual, Less than, less than or equal
        Console.WriteLine(myInt > mySecondInt); // False
        Console.WriteLine(myInt >= mySecondInt / 2); // True
        Console.WriteLine(myInt < mySecondInt); // True
        Console.WriteLine(myInt <= mySecondInt / 2); // True
    }
}
