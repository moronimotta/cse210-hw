using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Clear();
            Random rand = new Random();
            int magic_number = rand.Next(1, 11);

            int guess = 0;

            Console.WriteLine("What is the magic number? Beetwen 1 and 100");
            int user_number = Convert.ToInt32(Console.ReadLine());

            while (user_number != magic_number)
            {
                if (user_number < magic_number)
                {
                    Console.WriteLine("Higher");
                    guess++;
                }
                else if (user_number > magic_number)
                {
                    Console.WriteLine("Lower");
                    guess++;
                }
                Console.WriteLine("What is your guess?");
                user_number = Convert.ToInt32(Console.ReadLine());
            }
            guess++;
            Clear();
            Console.WriteLine($"You guessed it! Attempts: {guess}");
            Console.WriteLine("Do you want to play again? (yes/no)");
            string play_again = Console.ReadLine();
            if (play_again == "no")
            {
                break;
            }
        }


    }

    static void Clear()
    {
        Console.Clear();
    }
}
