/* 
    A program that reads in a text file (simulated for Mac) and performs 
    various string interpolation techniques to display a desired output
*/
using static System.Console;

class Assignment1
{
    public static void Main(string[] args)
    {
        //declare appropriate strings and call macReadLine() for instantiation
        string firstName = MacReadLine();
        string lastName = MacReadLine();
        string birthCountry = MacReadLine();
        string friendsCountry = MacReadLine();
        string friendsCapital = MacReadLine();

        /*
            try parse for all appropriate string to number conversions, results
            will be checked when vars are called in interpolation
        */
        decimal.TryParse(MacReadLine(), out decimal gdp);

        string friendName = MacReadLine();

        byte.TryParse(MacReadLine(), out byte friendAge);

        int.TryParse(MacReadLine(), out int quarter1);

        int.TryParse(MacReadLine(), out int quarter2);

        int.TryParse(MacReadLine(), out int quarter3);

        int.TryParse(MacReadLine(), out int quarter4);

        byte.TryParse(MacReadLine(), out byte secondFriendAge);

        string firstNum = MacReadLine(); //store as string first so it can be called
        string secondNum = MacReadLine(); //store as string first so it can be called

        int.TryParse(firstNum, out int firstNumCheck);
        int.TryParse(secondNum, out int secondNumCheck);

        long worldPop = 7888000000;

        //Part 1 console output formatted accordingly, broken into paragraphs for readability
        //para 1
        WriteLine($"Hi, my name is {firstName} {lastName}, and I was born in " +
                  $"{birthCountry}. It is polite to write my name like this: " +
                  $"{firstName.ToUpper()} {lastName.ToUpper()}. My name is huge; " +
                  $"it is {firstName.Length + lastName.Length} characters long!\n");
        //para 2
        WriteLine($"Many of my friends were born in the country of {friendsCountry}. " +
                  $"Its capital is {friendsCapital}, but people call it: " +
                  $"{friendsCapital.Remove(0, 4)}.\n");
        //para 3
        WriteLine($"My country is very wealthy, with a GDP of {(gdp != 0 ? gdp : "No, didn't convert"):C}!" +
                  $" We have 100000 citizens, and each generates {gdp / 100000:C} of the GDP output. " +
                  $"Each of us produces 30,000 widgets a year, for a total of {(long)100000 * 30000} " +
                  $"widgets per year! Many of the world's population of {worldPop} people will buy them!\n");
        //para 4
        WriteLine($"Here is my friend, her age, and how many widgets she produced per quarter last year:\n");
        //para 5
        WriteLine($"Name: {friendName} Age: {(friendAge != 0 ? friendAge : "no, didn't convert")} " +
                  $"Q1: {(quarter1 != 0 ? quarter1 : "No, didn't convert")} " +
                  $"Q2: {(quarter2 != 0 ? quarter2 : "No, didn't convert")} " +
                  $"Q3: {(quarter3 != 0 ? quarter3 : "No, didn't convert")} " +
                  $"Q4: {(quarter4 != 0 ? quarter4 : "No, didn't convert")} " +
                  $"Total: {(long)(quarter1 + quarter2 + quarter3 + quarter4)}\n");
        //para 6
        WriteLine($"Here is another friend, and he is " +
                  $"{(secondFriendAge != 0 ? secondFriendAge : "No, didn't convert")} years old." +
                  $" They are a total of {friendAge + secondFriendAge} years old.\n");
        //para 7
        WriteLine($"Is this string {firstNum} a number? {(firstNumCheck != 0 ? firstNumCheck : "No, didn't convert")}\n" +
                  $"Is this string {secondNum} a number? {(secondNumCheck != 0 ? secondNumCheck : "No, didn't convert")}\n");

        //Part 2
        //create 3 tuples of favorite actors and output each
        (string name, int age, string movie) actor1 = ("Christian Bale", 50, "Out Of The Furnace");
        (string name, int age, string movie) actor2 = ("Jeff Bridges", 74, "The Big Lebowski");
        (string name, int age, string movie) actor3 = ("Brad Pitt", 60, "Troy");

        WriteLine($"Actor 1: {actor1}\nActor 2: {actor2}\nActor 3: {actor3}\n");

        //create 2 tuples of 2 ints each and output each with sum and product of both
        (int num1, int num2) tuple1 = (29, 6);
        (int num1, int num2) tuple2 = (23, 9);

        WriteLine($"The sum and product of number tuples 1: {tuple1} and 2: {tuple2} " +
                  $"are {tuple1.num1 + tuple1.num2 + tuple2.num1 + tuple2.num2} and " +
                  $"{tuple1.num1 * tuple1.num2 * tuple2.num1 * tuple2.num2}.\n");

        //create string array of 9 dog breeds and print all, then print specified index
        string[] dogs = {"Collie", "Fox Terrier", "Beagle", "Pit Bull", "Sheltie",
                         "Poodle", "Pug", "Miniature Schnauzer", "Dachshund"};
        //bonus
        WriteLine($"My dogs: {string.Join(", ", dogs)}\n");
        WriteLine($"The 3rd through 5th dogs: {string.Join(", ", dogs[3..6])}");
    }

    //Mac workaround code acting as txt file
    static string MacReadLine()
    {
        string[] returnStrings = {"Stephen", "Badeaux", "Belize", "Georgia", "Paraquat",
            "10000000000", "Sally", "23", "8000", "12000", "20000", "40000", "250",
            "999", "Twelve"};

        return returnStrings[staticValue.stringNumber++];
    }

    public static class staticValue
    {
        public static int stringNumber = 0;
    }
}