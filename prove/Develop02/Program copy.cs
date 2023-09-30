using System;

class Calculator
{
    public int _num1;
    public int _num2;

    public int Add(int num1, int num2)
    {
        return num1 + num2;
    }

    public int Subtract(int num1, int num2)
    {
        return num1 - num2;
    }
}

class Program
{
    static void Main(string[] args)
    {
        
        Calculator calculator = new Calculator();

        calculator._num1 = 5;
        calculator._num2 = 10;

        int result = calculator.Add(calculator._num1, calculator._num2);


    }
}