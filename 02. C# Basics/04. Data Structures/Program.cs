namespace _04._Data_Structures;

class Program
{
    static void Main(string[] args)
    {
// ARRAYS:

        // First way of declaring an Array
        string[] myGroceryArray = new string[2];

        myGroceryArray[0] = "Guacamole";

        Console.WriteLine(myGroceryArray[0]);
        Console.WriteLine(myGroceryArray[1]); // Null
        // Console.WriteLine(myGroceryArray[2]); //System.IndexOutOfRangeException: Index was outside the bounds of the array.

        myGroceryArray[1] = "Ice Cream";

        Console.WriteLine(myGroceryArray[0]);
        Console.WriteLine(myGroceryArray[1]);

        // Second way of declaring an Array

        string[] mySecondGroceryArray = { "Apples", "Eggs" };

        Console.WriteLine(mySecondGroceryArray[0]);
        Console.WriteLine(mySecondGroceryArray[1]);

        /*
        mySecondGroceryArray[2] = "Bananas"; // Unhandled exception. System.IndexOutOfRangeException: Index was outside the bounds of the array.

        Console.WriteLine(mySecondGroceryArray[2]);

        */

// LISTS:

        // Declararion

        List<string> myGroceryList = new List<String>() { "Milk", "Cheese" }; 
        // The parenthesis are necessary since we are calling the constrcutor of the class to create a new instance of the list
        // You could use the alias (bool) or the class name (Boolean) `new List<string>();`

        
        Console.WriteLine(myGroceryList[0]);
        Console.WriteLine(myGroceryList[1]);

        //Console.WriteLine(myGroceryList[2]); 
        // Unhandled exception. System.ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection.

        myGroceryList.Add("Oranges");

        Console.WriteLine(myGroceryList[2]); // Oranges

// IENUMERABLE:

        // We want to use this to loop through Arrays or Collection and it is faster than other loop constructs

        IEnumerable<string> myGroceryIEnumerable = myGroceryList;
        // We cannot manipulate the values inside of it, so we need to already have an Array or Collection

        // Console.WriteLine(myGroceryIEnumerable[0]); // Cannot apply indexing with [] to an expression of type IEnumerable<string>

        Console.WriteLine(myGroceryIEnumerable.First()); // Milk

// 2 DIMENSIONAL ARRAYS:

        string[,] myTwoDimensionalArray = new string[,] {
            { "Apples", "Eggs" },
            { "Milk", "Cheese" },
        };

        // Access it at a double index

        Console.WriteLine(myTwoDimensionalArray[0,0]); // Apples
        Console.WriteLine(myTwoDimensionalArray[0,1]); // Eggs
        Console.WriteLine(myTwoDimensionalArray[1,0]); // Milk
        Console.WriteLine(myTwoDimensionalArray[1,1]); // Cheese

// DICTIONARIES:

        Dictionary<string, string> myGroceryDictionary = new Dictionary<string, string>(){
            // Department(key) -> Product(value)
            { "Dairy", "Cheese" }
        };

        Console.WriteLine(myGroceryDictionary["Dairy"]); // Cheese

        // We can also create an array of elements inside a dictionary

        Dictionary<string, string[]> mySecondGroceryDictionary = new Dictionary<string, string[]>(){
            // Department(key) -> Products(value[array])
            { "Dairy", new string[]{"Cheese", "Milk", "Yogurt"} } // A new array must be declared with new array[]
        };

        Console.WriteLine(mySecondGroceryDictionary["Dairy"]); // System.String[] (the object)
        Console.WriteLine(mySecondGroceryDictionary["Dairy"][0]); // Cheese
        Console.WriteLine(mySecondGroceryDictionary["Dairy"][1]); // Milk
        Console.WriteLine(mySecondGroceryDictionary["Dairy"][2]); // Yogurt
    }
}