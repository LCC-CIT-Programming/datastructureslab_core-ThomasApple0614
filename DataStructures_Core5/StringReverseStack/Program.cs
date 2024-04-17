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

    }
}
