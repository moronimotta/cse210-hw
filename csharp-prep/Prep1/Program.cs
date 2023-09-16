using System;

class Program
{
    static void Main(string[] args)
    {
        // prompt the user for their name
        Console.WriteLine("What is your first name?");
        // read the user's name
        string fst_name = Console.ReadLine();

        Console.WriteLine("What is your last name?");
        string lst_name = Console.ReadLine();

        // print the user's name
        Console.WriteLine($"Your name is {lst_name}, {fst_name} {lst_name}");
    }
}