using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What is your grade percentage?");
        int grade_percent = Convert.ToInt32(Console.ReadLine());

        string sign = CheckSign(grade_percent);

        string grade;

        if (grade_percent >= 90)
        {
            grade = "A";
        }
        else if (grade_percent >= 80)
        {
            grade = "B";
        }
        else if (grade_percent >= 70)
        {
            grade = "C";
        }
        else if (grade_percent >= 60)
        {
            grade = "D";
        }
        else
        {
            grade = "F";
        }

        if (grade_percent >= 70)
        {
            Console.WriteLine($"You passed with a {grade}{sign}");
        }
        else
        {
            Console.WriteLine($"You failed with a {grade}{sign}");
        }
    }
    static string CheckSign(int grade_percent)
    {
        string sign = "";

        if(grade_percent >=97 || grade_percent <= 59)
        {
            return sign;
        }

        if (grade_percent % 10 >= 7)
        {
            sign = "+";
        }
        else if (grade_percent % 10 < 3)
        {
            sign = "-";
        }

        return sign;
    }
}