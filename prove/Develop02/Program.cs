using System;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();

        Console.WriteLine("Welcome to the Journal Program!");
        Console.WriteLine("Please select an option:");
        Console.WriteLine("1. Write");
        Console.WriteLine("2. Display");
        Console.WriteLine("3. Load");
        Console.WriteLine("4. Save");
        Console.WriteLine("5. Exit");

        int option = Convert.ToInt32(Console.ReadLine());

        HandleOption(option, journal);

        while (option != 6)
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Remove Entry");
            Console.WriteLine("6. Exit");

            option = Convert.ToInt32(Console.ReadLine());
            HandleOption(option, journal);
        }


    }

    static void HandleOption(int option, Journal journal)
    {
        switch (option)
        {
            case 1:

                string sendPrompt = journal.SendPrompt();

                Console.WriteLine(sendPrompt);

                string content = Console.ReadLine();
                Entry entry = new Entry();
                entry._content = content;
                entry._date = DateTime.Now.Date.ToString("dd/MM/yyyy");
                journal.AddEntry(entry, sendPrompt);
                return;
            case 2:
                journal.DisplayEntries();
                return;
            case 3:
                journal.LoadEntries();
                return;
            case 4:
                journal.SaveEntries();
                return;
            case 5:
                journal.RemoveEntry();
                return;
            case 6:
                Console.WriteLine("Goodbye!");
                break;
            default:
                Console.WriteLine("Invalid option");
                return;
        }
    }
}


class Journal
{
    public Entry[] _entries { get; set; }
    public int[] _usedPrompts { get; set; }
    public string[] _prompts = new string[] { "Tell me something funny about your day?", "What did you learn today?", "What did you accomplish today?" };

    public Journal()
    {
        _entries = new Entry[0];
    }

    public string SendPrompt()
    {
        Random random = new Random();
        int randomNumber = random.Next(0, _prompts.Length);

        if (_usedPrompts == null)
        {
            _usedPrompts = Array.Empty<int>();
        }
        if (_usedPrompts.Length >= _prompts.Length)
        {
            return "How was your day?";
        }

        while (Array.Exists(_usedPrompts, element => element == randomNumber))
        {
            randomNumber = random.Next(0, _prompts.Length);
        }

        int[] usedPrompts = _usedPrompts;
        Array.Resize(ref usedPrompts, usedPrompts.Length + 1);
        usedPrompts[usedPrompts.Length - 1] = randomNumber;
        _usedPrompts = usedPrompts;

        return _prompts[randomNumber];

    }

    public void RemovePrompt(string prompt)
    {
        int index = Array.FindIndex(_prompts, element => element == prompt);

        int[] usedPrompts = _usedPrompts;
        Array.Resize(ref usedPrompts, usedPrompts.Length - 1);
        _usedPrompts = usedPrompts;

    }

    public void AddEntry(Entry entry, string prompt)
    {
        entry.InsertId();

        entry.InsertPrompt(prompt);

        List<Entry> entryList = _entries.ToList();

        entryList.Add(entry);

        _entries = entryList.ToArray();
    }
    public void RemoveEntry()
    {
        bool checkEntries = DisplayEntries();
        Console.WriteLine("Please enter the ID of the current new journal's entry you would like to remove:");
        if (!checkEntries)
        {
            return;
        }
        int id = Convert.ToInt32(Console.ReadLine());
        int index = Array.FindIndex(_entries, entry => entry._id == id);
        var list = _entries.ToList();
        list.RemoveAt(index);

    }
    public bool DisplayEntries()
    {
        if (_entries.Length == 0)
        {
            Console.WriteLine("There are no entries to display.");
            return false;
        }
        Console.WriteLine("Here are your entries:");
        foreach (Entry entry in _entries)
        {
            Console.WriteLine();
            Console.WriteLine("====================================");
            Console.WriteLine($"ID: {entry._id}");
            Console.WriteLine($"Date: {entry._date} - Prompt: {entry._prompt}");
            Console.WriteLine($"{entry._content}");
            Console.WriteLine("====================================");
        }
        return true;
    }
    public void SaveEntries()
    {
        if (_entries.Length == 0)
        {
            Console.WriteLine("There are no entries to save.");
            return;
        }
        Console.WriteLine("Please enter a file name:");
        string fileName = Console.ReadLine();
        using StreamWriter sw = new StreamWriter(fileName);

        foreach (Entry entry in _entries)
        {
            sw.WriteLine($"ID: {entry._id}");
            sw.WriteLine($"Date: {entry._date} - Prompt: {entry._prompt}");
            sw.WriteLine($"{entry._content}");
            sw.WriteLine();
        }
        sw.Close();
    }
    public void LoadEntries()
    {
        Console.WriteLine("Please enter a file name:");
        string fileName = Console.ReadLine();

        if (!File.Exists(fileName))
        {
            Console.WriteLine("File does not exist.");
            return;
        }
        using StreamReader sr = new StreamReader(fileName);

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            Console.WriteLine(line);
        }
    }
}

class Entry
{
    public int _id { get; set; }
    public string _content { get; set; }
    public string _date { get; set; }
    public string _prompt { get; set; }
    public void InsertId()
    {
        _id = GenerateId();
    }
    public void InsertPrompt(string prompt)
    {
        _prompt = prompt;
    }
    public static int GenerateId()
    {
        Random random = new Random();

        int randomNumber = random.Next(1, 1000);

        return randomNumber;
    }
}