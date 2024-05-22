/*
    A program that uses basic manual methods to create a stack and perform
    desired stack operations on the stack 
*/
using System;
using static System.Console;

class Assignment2
{
    public static void Main()
    {
        //declare "stack" and initialize using StackOperations method
        int[] rawStack = StackOperations();

        //create new int array by passing current stack to TrimStack method
        int[] trimmedStack = TrimStack(rawStack);

        //print final stack after processing and trimming is done
        WriteLine($"Stack after processing: {string.Join(", ", trimmedStack)}\n\n\n\n");

        //pass finalStack to StackSearch method to read and perform specified queries
        StackSearch(trimmedStack);

        //print finalStack unsorted
        WriteLine($"\n\n\nUnsorted array: {string.Join(" ", trimmedStack)}\n");

        //sort and print finalStack
        Array.Sort(trimmedStack);
        WriteLine($"Sorted array: {string.Join(" ", trimmedStack)}");
    }

    //StackOperations method builds and returns an int array based off of read stack commands
    static int[] StackOperations()
    {
        //create "stack" of potential size 100 using int array
        int[] stack = new int[100];

        //set first element to -1 to indicate an empty stack
        stack[0] = -1;

        //start top at 1 to pass index 0 which is empty stack condition(-1)
        int top = 1;
        string operationRequest;

        //do while loop runs until (stack end) command is read
        do
        {
            //store MacReadLine element in string for processing
            operationRequest = MacReadLine();

            //split request into substrings based on space between request and value
            string[] operationParts = operationRequest.Split(" ");

            //push section checks for push followed by an int and then parses for int > 0 then stores on stack
            if (operationParts[0].Contains("push") && operationParts.Length > 1)
            {
                int.TryParse(operationParts[1], out int pushedElement);

                if (pushedElement > 0)
                {
                    stack[top] = pushedElement;
                    WriteLine($"Pushed {stack[top]}\n");
                    top++;
                }
            }

            //pop section checks for pop and pops value at top of stack if stack is not empty
            else if (operationParts[0].Contains("pop") && stack[top - 1] != -1)
            {
                WriteLine($"Popped {stack[top - 1]}\n");
                stack[top - 1] = 0;
                top--;
            }

            //continue ignores invalid command formats and proceeds with processing
            else continue;
        }
        while (operationRequest != "(stack end)");

        //return stack after processing is done
        return stack;
    }

    /*trim stack method reads given "stack" from top to bottom and "pops" elements
    from top and copies to trimmedStack int arr[] if popped variables are
    valid entries (> 0), stops at empty stack (-1) and returns new trimmedStack*/
    static int[] TrimStack(int[] stack)
    {
        int[] trimmedStack = new int[5];
        int index = 0;

        for (int top = stack.Length-1; stack[top] != -1; top--)
        {
            if (stack[top] > 0)
            {
                trimmedStack[index] = stack[top];
                stack[top] = 0;
                index++;
            }
        }

        //return trimmed stack
        return trimmedStack;
    }

    //StackSearch method takes in an int array and performs searches for desired elements
    static void StackSearch(int[] stack)
    {
        string findRequest;
        int index;

        //do while loop runs until (find end) command is read
        do
        {
            //store MacReadLine element in string for processing
            findRequest = MacReadLine();

            //split request into substrings based on space between request and value
            string[] findParts = findRequest.Split(" ");
            index = 0;
            bool found = false;

            //check for correct request format and search for desired element
            if (findParts[0].Contains("find") && findParts.Length > 1)
            {
                int.TryParse(findParts[1], out int findElement);

                foreach (int i in stack)
                {
                    if (i == findElement)
                    {
                        WriteLine($"Searching list for item {findElement}, found it in array posiition {index}.\n");
                        found = true;
                    }
                    index++;
                }

                //unsuccessful search condition
                if (found == false && findElement != 0)
                {
                    WriteLine($"Searching list for item {findElement}, could not find it.\n");
                }
            }

            //continue ignores invalid command formats and proceeds with processing
            else continue;
        }
        while (findRequest != "(find end)");
    }

    //Mac workaround code acting as txt file
    static string MacReadLine()
    {
        string[] returnStrings = {"push 3", "push 7", "pop", "push 9", "push", "pop", "pop", "pop", "push push", "push 17",
                              "push 8", "push 9", "push 11", "push 43", "push 3", "push 88", "pop", "push 12", "pop",
                              "pop", "(stack end)", "find 9", "find 11", "find find", "find 17", "(find end)"};

        return returnStrings[StaticValue.stringNumber++];
    }

    public static class StaticValue
    {
        public static int stringNumber = 0;
    }
}