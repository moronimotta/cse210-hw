using System;

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = new int[100];
        int sum = 0;
        int count = 0;
        int largest = 0;
        int smallest = 200;
        int smallest_negative = 0;
        while (true)
        {
            Console.WriteLine("Enter a number (0 to quit)");
            int number = Convert.ToInt32(Console.ReadLine());
            if (number == 0)
            {
                break;
            }
            numbers[count] = number;
            sum += number;
            count++;
        }

        for (int i = 0; i < count; i++)
        {
            if (numbers[i] > largest)
            {
                largest = numbers[i];
            }
            if (numbers[i] < smallest && numbers[i] > 0)
            {
                smallest = numbers[i];
            }
            if (numbers[i] < 0 && numbers[i] < smallest_negative)
            {
                smallest_negative = numbers[i];
            }
        }

        for (int i = 0; i < count; i++)
        {
            for (int j = i + 1; j < count; j++)
            {
                if (numbers[i] > numbers[j])
                {
                    int temp = numbers[i];
                    numbers[i] = numbers[j];
                    numbers[j] = temp;
                }
            }
        }

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {(double)sum / count}");
        Console.WriteLine($"The largest number is: {largest}");
        Console.WriteLine($"The smallest positive number is: {smallest}");
        Console.WriteLine($"The smallest negative number is: {smallest_negative}");
        Console.WriteLine("The sorted list is:");
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(numbers[i]);
        }

    }
}