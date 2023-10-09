using System;
using System.Threading;
class Program
{
    static void Main(string[] args)
    {

        BreathingActivity breathingActivity = new BreathingActivity("Breathing Activity", "This activity will help you relax and focus on your breathing.");
        ReflectingActivity reflectingActivity = new ReflectingActivity("Reflecting Activity", "This activity will help you reflect on your life and your experiences.");
        ListingActivity listingActivity = new ListingActivity("Listing Activity", "This activity will help you list things that you are grateful for.");

        while (true)
        {
            Console.WriteLine("Please select an activity:");
            Console.WriteLine("1. Start Breathing activity");
            Console.WriteLine("2. Start Reflecting activity");
            Console.WriteLine("3. Start Listing activity");
            Console.WriteLine("4. Quit");
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();
            Console.WriteLine();
            switch (input)
            {
                case "1":
                    breathingActivity.Start();
                    break;
                case "2":
                    reflectingActivity.Start();
                    break;
                case "3":
                    listingActivity.Start();
                    break;
                case "4":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
            Console.WriteLine();
        }
    }

}

abstract class Activity
{
    protected string _name;
    protected int _duration;
    protected string _description;
    protected bool _isRunning;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public int ActivityDuration()
    {
        Console.WriteLine("How long would you like to spend on this activity?");
        Console.Write("Enter your choice (in seconds): ");
        int duration = Convert.ToInt32(Console.ReadLine());
        _duration = duration;
        return _duration;
    }

    public void Introduction()
    {
        Console.WriteLine($"Welcome to the {_name}!");
        Console.WriteLine();
        Console.WriteLine(_description);
        Console.WriteLine();
        ActivityDuration();
        Console.WriteLine();
        Console.WriteLine("Press enter to start...");
        Console.ReadLine();
        Console.WriteLine();
    }


    public void Finish()
    {
        Console.WriteLine($"You have completed the {_name} with a duration of {_duration} seconds.");
    }

    public abstract void Start();
}
class BreathingActivity : Activity
{
    public BreathingActivity(string name, string description) : base(name, description)
    {
    }

    public override void Start()
    {
        Introduction();
        DateTime startTime = DateTime.Now;
        DateTime futureTime = startTime.AddSeconds(_duration);

        while (DateTime.Now < futureTime)
        {
            Console.WriteLine("Breathe in...");
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("Breathe out...");
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine();
        }
        Finish();
    }
}

class ReflectingActivity : Activity
{
    private string[] _prompts = new string[] {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] _questions = new string[] {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    private int[] _usedPrompts = new int[4];

    public ReflectingActivity(string name, string description) : base(name, description)
    {
    }

    public override void Start()
    {
        Introduction();
        _isRunning = true;
        DateTime startTime = DateTime.Now;
        DateTime futureTime = startTime.AddSeconds(_duration);
        while (DateTime.Now < futureTime)
        {
            int promptIndex = GetUnusedPromptIndex();
            Console.WriteLine(_prompts[promptIndex]);
            _usedPrompts[promptIndex] = 1;


            if ((DateTime.Now - startTime).TotalSeconds >= _duration)
            {
                break;
            }

            foreach (string question in _questions)
            {
                if ((DateTime.Now - startTime).TotalSeconds >= _duration)
                {
                    break;
                }
                Console.Write($"{question} ");
                SpinnerAnimation();
                string answer = Console.ReadLine();
                System.Threading.Thread.Sleep(1000);
            }
            Console.Clear();
        }

        Console.WriteLine("Activity completed. Please wait for 3 seconds...");
        SpinnerAnimation();
        System.Threading.Thread.Sleep(3000);
        Finish();
    }

    private void SpinnerAnimation()
    {
        char[] spinners = { '|', '/', '-', '\\' };
        int spinnerIndex = 0;
        for (int i = 0; i < 8; i++)
        {
            Console.Write(spinners[spinnerIndex]);
            System.Threading.Thread.Sleep(125);
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            spinnerIndex = (spinnerIndex + 1) % spinners.Length;
        }
    }


    private int GetUnusedPromptIndex()
    {
        Random random = new Random();
        int promptIndex = random.Next(0, _prompts.Length);
        while (_usedPrompts[promptIndex] == 1)
        {
            promptIndex = random.Next(0, _prompts.Length);
        }
        return promptIndex;
    }
}

class ListingActivity : Activity
{
    private string[] _prompts = new string[] {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    private int[] _usedPrompts = new int[5];

    public ListingActivity(string name, string description) : base(name, description)
    {
    }

    public override void Start()
    {
        Introduction();
        _isRunning = true;
        DateTime startTime = DateTime.Now;
        DateTime futureTime = startTime.AddSeconds(_duration);

        while (DateTime.Now < futureTime)
        {
            Console.Clear();
            int promptIndex = GetUnusedPromptIndex();
            Console.WriteLine(_prompts[promptIndex]);
            Console.Write("Your answer: ");
            SpinnerAnimation();
            string answer = Console.ReadLine();
        }

        Console.WriteLine("Activity completed. Please wait for 3 seconds...");
        SpinnerAnimation();
        System.Threading.Thread.Sleep(3000);
        Console.Clear();

        if (AllPromptsUsed())
        {
            Console.WriteLine("You have used all the prompts. Is there anything else interesting you want to add?");
            Console.Write("Your answer: ");
            SpinnerAnimation();
            string answer = Console.ReadLine();
            Console.Clear();
        }

        Finish();
    }

    private void SpinnerAnimation()
    {
        char[] spinners = { '|', '/', '-', '\\' };
        int spinnerIndex = 0;
        for (int i = 0; i < 8; i++)
        {
            Console.Write(spinners[spinnerIndex]);
            System.Threading.Thread.Sleep(125);
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            spinnerIndex = (spinnerIndex + 1) % spinners.Length;
        }
    }


    private int GetUnusedPromptIndex()
    {
        Random random = new Random();
        int promptIndex = random.Next(0, _prompts.Length);
        while (_usedPrompts[promptIndex] == 1)
        {
            promptIndex = random.Next(0, _prompts.Length);
        }
        _usedPrompts[promptIndex] = 1;
        return promptIndex;
    }

    private bool AllPromptsUsed()
    {
        foreach (int usedPrompt in _usedPrompts)
        {
            if (usedPrompt == 0)
            {
                return false;
            }
        }
        return true;
    }
}