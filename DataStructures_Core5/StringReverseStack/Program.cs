using System;
using System.Collections;
using System.Collections.Generic;
class Program
{
    public static void Main(string[] args)
    {
        //Using the Visual Studio Solution provided in the starting files as a starting point, write an application that asks the user to enter a string from the keyboard and prints the string in reverse order.  Use a stack to reverse the string
        Console.WriteLine("Problem 1: ");
        Stack stack = new Stack();
        Console.WriteLine("Please enter a word, phrase, or sentence: ");
        string input = Console.ReadLine();

        for (int i = 0; i < input.Length; i++)
        {
            char n = input[i];
            stack.Push(n);
        }
        Console.WriteLine("Here is what you entered in reverse: ");
        foreach (char n in stack)
        {
            Console.Write(n);
        }
        Console.WriteLine("\n");

        Console.WriteLine("Problem 2:");
        Console.WriteLine();

        //Problem 2: Write a program that asks the user to enter a string from the keyboard and prints the number of each letter

        //Using the Visual Studio Solution provided in the starting files as a starting point, write an application that asks the user to enter a positive integer from the keyboard and prints the binary numbers from 1 to that number.  Use a queue to generate the binary numbers.
        // Queue<string> queue = new Queue<string>();
        Queue<string> queue = new Queue<string>();
        int num = GetPositiveInt(1, 20);
        queue.Enqueue("1");
        for (int i = 0; i <= num; i++)
        {
            string next = queue.Dequeue();
            queue.Enqueue(next + "0");
            queue.Enqueue(next + "1");
            Console.WriteLine(next);
        }
        Console.ReadLine();



        static int GetPositiveInt(int min, int max)
        {
            bool isInt = false;
            int number = min - 1;
            do
            {
                Console.Write("Please enter a positive whole number: ");
                string input = Console.ReadLine();
                isInt = int.TryParse(input, out number);
            } while (!(isInt && number > min && number < max));

            return number;
        }

    }
}
