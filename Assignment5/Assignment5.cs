/*
    Program to create 2 concurrent CPU threads that compete with a user to solve 4 rounds of multiplication problems. 
    After completion of each round results are compared for speed and accuracy and displayed.
 */

using static System.Console;
#nullable disable


class Program
{
    //Main prompts user for "P" to play or "E" to exit game
    public static void Main()
    {
        WriteLine("Welcome to Math Whiz!");
        WriteLine("---------------------\n");

        while(true)
        {
            WriteLine("Enter P to play, or E to exit:");
            string input = ReadLine();

            if (input == "P" || input == "p")
            {
                PlayGame();
                return;
            }
            else if (input == "E" || input == "e")
            {
                WriteLine("Exiting Program...\nThank you for playing Math Whiz!\n");
                return;
            }
            else
            {
                WriteLine("Please enter a valid character.\n");
            }
        }

    }

    //PlayGame method runs 4 rounds of random multiplication problems, tracks completion time for each thread and compares results. 
    public static void PlayGame()
    {
        //create random variable
        Random rnd = new();

        //create completion time variables for each thread
        DateTime task1CompletionTime = DateTime.MinValue;
        DateTime task2CompletionTime = DateTime.MinValue;
        DateTime userCompletionTime = DateTime.MinValue;

        for (int i = 1; i < 5; i++)
        {
            //create random multiplication variables each iteration
            int x = rnd.Next(1, 11);
            int y = rnd.Next(1, 11);
            WriteLine($"\nRound {i}:");
            WriteLine($"What is the product of {x} and {y}?");

            //create CPU threads that run applicable methods
            Task <int>cpu1 = Task.Run(() => Multiply("1", x, y, ref task1CompletionTime));
            Task <int>cpu2 = Task.Run(() => Multiply("2", x, y, ref task2CompletionTime));

            //get and store user input
            int userInput = UserMultiply(ref userCompletionTime);

            //wait for cpu threads to complete before displaying user answer
            Task.WaitAll(cpu1, cpu2);
            WriteLine($"Your answer was  {userInput} @ {userCompletionTime:mm:ss:fff}");

            //create list of completion times and find minimum time
            List<DateTime> times = new() { task1CompletionTime, task2CompletionTime, userCompletionTime };
            times.Sort();
            DateTime fastest = times[0];

            //create difference variable for display
            TimeSpan difference = TimeSpan.MaxValue;      

            //correct user answer
            if (userInput == cpu1.Result)
            {
                //user faster than both tasks
                if (fastest == userCompletionTime)
                {
                    difference = times[1] - fastest;
                    WriteLine($"Correct, and you were the quickest by {(int)difference.TotalMilliseconds} milliseconds!\n");
                }
                //user slower than one or both tasks
                else
                {
                    difference = userCompletionTime - fastest;
                    WriteLine($"Correct, but you were too slow by {(int)difference.TotalMilliseconds} milliseconds!\n");
                }
            }
            //incorrect user answer
            else
            {
                //user faster than both tasks
                if (fastest == userCompletionTime)
                {
                    difference = times[1] - fastest;
                    WriteLine($"Incorrect, but you were the quickest by {(int)difference.TotalMilliseconds} milliseconds!\n");
                }
                //user slower than one or both tasks
                else
                {
                    difference = userCompletionTime - fastest;
                    WriteLine($"Incorrect, and you were too slow by {(int)difference.TotalMilliseconds} milliseconds!\n");
                }
            }
        }
        WriteLine("Thank you for playing Math Whiz!");
    }

    /*
     * Multiply takes two random variables and multiplies them, returns int answer and updates completion time.
     * results are printed in the method to assure the thread reports results as soon as it is finished.
     */
    public static int Multiply(string threadNumber, int x, int y, ref DateTime TaskCompletionTime)
    {
        //random delay for CPU thread between 1800-3000ms
        Task.Delay(new Random().Next(1800, 3001)).Wait();
        int result = x * y;
        TaskCompletionTime = DateTime.Now;
        WriteLine($"Thread {threadNumber} answers {result} @ {TaskCompletionTime:mm:ss:fff}");
        return result;
    }

    /*
     * UserMultiply reads keys from user until "Enter" is pressed. 
     * keys stored as string then converted to int.
     * returns int answer and updates completion time.
     */
    public static int UserMultiply(ref DateTime endUser)
    {
        string userInputString = "";
        while (true)
        {
            ConsoleKeyInfo key = ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
                break;

            if (char.IsDigit(key.KeyChar))
            {
                userInputString += key.KeyChar;
            }
        }
        endUser = DateTime.Now;

        bool success = int.TryParse(userInputString, out int UserInput);
        if (success)
        {
            return UserInput;
        }
        else
        {
            WriteLine("User answer did not successfully convert to a number.");
            return UserInput;
        }
    }
}