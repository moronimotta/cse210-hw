using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcome();
        string name = PromptUserName();
        int number = PromptUserNumber();
        int squared_number = SquareNumber(number);
        DisplayResult(name, squared_number);
    }


    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    static string PromptUserName()
    {
        Console.WriteLine("Please enter your name:");
        string name = Console.ReadLine();
        return name;
    }

    static int PromptUserNumber()
    {
        Console.WriteLine("Please enter your favorite number:");
        int number = Convert.ToInt32(Console.ReadLine());
        return number;
    }

    static int SquareNumber(int number)
    {
        return number * number;
    }

    static void DisplayResult(string name, int number)
    {
        Console.WriteLine($"Hello {name}, your favorite number squared is {number}");
    }

}