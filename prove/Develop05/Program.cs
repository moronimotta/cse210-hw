using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();

        while (true)
        {   
            goalManager.LevelUp();
            Console.WriteLine($"Current Score: {goalManager._score}");
            Console.WriteLine("Please select an activity:");
            Console.WriteLine("1. Create a New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();
            Console.WriteLine();
            switch (input)
            {
                case "1":
                    goalManager.CreateGoal();
                    break;
                case "2":
                    goalManager.ListGoalsDetails();
                    break;
                case "3":
                    goalManager.SaveGoals();
                    break;
                case "4":
                    goalManager.LoadGoals();
                    break;
                case "5":
                    goalManager.RecordEvent();
                    break;
                case "6":
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

class GoalManager
{
    public List<Goal> _goals;
    public int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }
public void LevelUp()
{
    int score = GetScore();
    string[] ranks = { "Bronze", "Silver", "Gold", "Platinum", "Diamond", "Master" };
    ConsoleColor[] colors = { ConsoleColor.DarkGray, ConsoleColor.White, ConsoleColor.Yellow, ConsoleColor.DarkYellow, ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Red };
    int rankIndex = Math.Min(score / 100, ranks.Length - 1);
    int colorIndex = Math.Min(score / 100, colors.Length - 1);
    string rank = ranks[rankIndex];
    ConsoleColor color = colors[colorIndex];
    Console.ForegroundColor = color;
    Console.WriteLine($"Current Level: {rank}");
    Console.ResetColor();
}


    public int GetScore()
    {
        int score = 0;
        foreach (Goal goal in _goals)
        {
            if (goal.IsComplete())
            {
                score += goal._points;
            }
        }
        return score;
    }

    public void ListGoalsDetails()
{
    Console.WriteLine($"Number of goals: {_goals.Count}");
    if (_goals.Count == 0)
    {
        Console.WriteLine("You have no goals.");
    }
    else {
        Console.WriteLine("Here are your goals:");
        foreach (Goal goal in _goals)
        {
            Console.WriteLine(goal.GetDetailsString());
        }
    } 
}

    public void CreateGoal()
    {
        Console.WriteLine("Select the type of goal:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter your choice: ");
        string input = Console.ReadLine();
        
        Console.Write("Enter the name of the goal: ");
        string name = Console.ReadLine();
        Console.Write("Enter the description of the goal: ");
        string description = Console.ReadLine();
        Console.Write("Enter the point value of the goal: ");
        int points = int.Parse(Console.ReadLine());

        switch (input)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, description, points));
                break;
            case "2":
                _goals.Add(new EternalGoal(name, description, points));
                break;
            case "3":
                Console.Write("Enter the target number of times to complete the goal: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter the bonus point value for completing the goal: ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                break;
            default:
                Console.WriteLine("Invalid input. Please try again.");
                break;
        }
        
    }

    public void RecordEvent()
    {
        Console.WriteLine("Select the goal to record an event for:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i]._shortName}");
        }
        Console.Write("Enter your choice: ");
        int choice = int.Parse(Console.ReadLine()) - 1;
        Goal goal = _goals[choice];
        int pointsEarned = goal.RecordEvent();
        _score += pointsEarned;
        Console.WriteLine($"Event recorded for {goal._shortName}. You earned {pointsEarned} points!");
    }

    public void SaveGoals()
{
    Console.Write("Enter the name of the file to save the goals to: ");
    string fileName = Console.ReadLine();
    using (StreamWriter writer = new StreamWriter(fileName))
    {
        writer.WriteLine(_score);

        foreach (Goal goal in _goals)
        {
            string goalString = goal.GetStringRepresentation();
            writer.WriteLine(goalString);
        }
    }
    Console.WriteLine($"Goals saved to {fileName}.");
}

    public void LoadGoals()
{
    Console.Write("Enter the name of the file to load the goals from: ");
    string fileName = Console.ReadLine();
    using (StreamReader reader = new StreamReader(fileName))
    {
        string line;
        line = reader.ReadLine();
        _score = int.Parse(line);

        while ((line = reader.ReadLine()) != null)
        {
            string[] parts = line.Split(',');
            string type = parts[0];
            string name = parts[1];
            string description = parts[2];
            int points = int.Parse(parts[3]);
            bool isComplete = false;
            if (type != "eternal"){
            
            isComplete = bool.Parse(parts[4]);
            }
            switch (type)
            {
                case "simple":
                    _goals.Add(new SimpleGoal(name, description, points, isComplete));
                    Console.WriteLine($"Loaded simple goal: {name}");
                    break;
                case "eternal":
                    _goals.Add(new EternalGoal(name, description, points));
                    Console.WriteLine($"Loaded eternal goal: {name}");
                    break;
                case "checklist":
                    int target = int.Parse(parts[5]);
                    int bonus = int.Parse(parts[6]);
                    int timesCompleted = int.Parse(parts[7]);
                    _goals.Add(new ChecklistGoal(name, description, points, target, bonus, timesCompleted, isComplete));
                    Console.WriteLine($"Loaded checklist goal: {name}");
                    break;
                default:
                    Console.WriteLine($"Invalid goal type: {type}");
                    break;
            }
        }
    }
    Console.WriteLine($"Goals loaded from {fileName}.");
}
}

class Goal
{
    public string _shortName;
    public string _description;
    public int _points;
    public string Type { get; }

    public Goal(string name, string description, int points, string type)
    {
        _shortName = name;
        _description = description;
        _points = points;
        Type = type;
    }

    public virtual int RecordEvent()
    {
        return _points;
    }

    public virtual bool IsComplete()
    {
        return false;
    }

    public virtual string GetDetailsString()
    {
        if (IsComplete())
        {
            return $"[X] {_shortName} ({_description})";
        }
        else
        {
            return $"[ ] {_shortName} ({_description})";
        }
    }

    public virtual string GetStringRepresentation()
    {
        return $"{Type},{_shortName},{_description},{_points}";
    }
}

class ChecklistGoal : Goal
{
    private int _target;
    private int _bonus;
    private int _timesCompleted;

    private bool _isComplete;

    public ChecklistGoal(string name, string description, int points, int target, int bonus = 0,int  timesCompleted = 0, bool completed = false) : base(name, description, points, "checklist")
    {
        _target = target;
        _bonus = bonus;
        _timesCompleted = timesCompleted;
         _isComplete = completed;
    }

    public override int RecordEvent()
    {
        _timesCompleted++;
        if (_timesCompleted == _target)
        {
            _isComplete = true;
            return _bonus; 
        }
        else
        {
            return _points;
        }
    }

    public override bool IsComplete()
    {
        return _timesCompleted == _target;
    }

    public override string GetDetailsString()
    {
        if (_timesCompleted == _target)
        {
            return $"[X] {_shortName} ({_description}) --- Completed {_timesCompleted}/{_target} times";
        }
        else
        {
            return $"[ ] {_shortName} ({_description}) --- Completed {_timesCompleted}/{_target} times";
        }
    }

    public override string GetStringRepresentation()
    {
        return $"{base.GetStringRepresentation()},{_isComplete},{_target},{_bonus},{_timesCompleted}";
    }
}

class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points, bool completed = false) : base(name, description, points, "simple")
    {
        _isComplete = completed;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _points;
        }
        return 0; 
    }
    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetDetailsString()
    {
        if (_isComplete)
        {
            return $"[X] {_shortName} ({_description})";
        }
        else
        {
            return $"[ ] {_shortName} ({_description})";
        }

    }

    public override string GetStringRepresentation()
    {
        return $"{base.GetStringRepresentation()},{_isComplete}";
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) : base(name, description, points, "eternal")
    {
    }

    public override string GetDetailsString()
    {
        return $"[ ] {_shortName} ({_description})";
    }

    public override string GetStringRepresentation()
    {
        return $"{base.GetStringRepresentation()}";
    }

    public override int RecordEvent()
    {
        return _points;
    }
}