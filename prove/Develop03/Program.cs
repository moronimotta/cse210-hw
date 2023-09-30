using System;
using System.Collections.Generic;
using System.IO;

class Scripture
{
    private string _reference;
    private string _text;
    private string _hint;
    private List<Word> _words;
    private List<int> _wordIndices;

    public Scripture(string reference, string text, string hint)
    {
        _reference = reference;
        _text = text;
        _hint = hint;
        _words = new List<Word>();
        _wordIndices = new List<int>();
        string[] words = text.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            _words.Add(new Word(words[i]));
            _wordIndices.Add(i);
        }
    }

    public string Reference
    {
        get { return _reference; }
    }

    public string Text
    {
        get { return _text; }
    }

    public string Hint
    {
        get { return _hint; }
    }

    public List<Word> Words
    {
        get { return _words; }
    }

    public void HideRandomWord()
    {
        Random random = new Random();
        int index = random.Next(_wordIndices.Count);
        int wordIndex = _wordIndices[index];
        _words[wordIndex].Hide();
        _wordIndices.RemoveAt(index);
    }

    public bool AllWordsHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.Hidden)
            {
                return false;
            }
        }
        return true;
    }
}

class Word
{
    private string _text;
    private bool _hidden;

    public Word(string text)
    {
        _text = text;
        _hidden = false;
    }

    public string Text
    {
        get { return _text; }
    }

    public bool Hidden
    {
        get { return _hidden; }
    }

    public void Hide()
    {
        _hidden = true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> scriptures = LoadScriptures("scriptures.txt");
        Console.WriteLine("Welcome to the Scripture Memorization Program!");
        while (true)
        {
            Console.WriteLine("Please select a scripture to memorize:");
            for (int i = 0; i < scriptures.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + scriptures[i].Reference);
            }
            Console.WriteLine("Enter the number of the scripture or type 'quit' to exit:");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
            {
                Console.WriteLine("Thanks for praying! I mean... playing!");
                return;
            }
            int index;
            if (int.TryParse(input, out index) && index >= 1 && index <= scriptures.Count)
            {
                MemorizeScripture(scriptures[index - 1]);
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
        
    }

    static List<Scripture> LoadScriptures(string filename)
    {
        List<Scripture> scriptures = new List<Scripture>();
        try
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                string reference = null;
                string text = null;
                string hint = null;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("Scripture: "))
                    {
                        if (reference != null && text != null && hint != null)
                        {
                            scriptures.Add(new Scripture(reference, text, hint));
                        }
                        reference = line.Substring("Scripture: ".Length);
                    }
                    else if (line.StartsWith("Content: "))
                    {
                        text = line.Substring("Content: ".Length);
                    }
                    else if (line.StartsWith("Hint: "))
                    {
                        hint = line.Substring("Hint: ".Length);
                    }
                }
                if (reference != null && text != null && hint != null)
                {
                    scriptures.Add(new Scripture(reference, text, hint));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading scriptures: " + ex.Message);
        }
        return scriptures;
    }

    static void MemorizeScripture(Scripture scripture)
    {
        Console.Clear();
        Console.WriteLine("Press Enter to begin memorizing the following scripture:");
        Console.WriteLine(scripture.Reference + " " + scripture.Text);
      
        Console.ReadLine();
        Console.Clear();
        while (!scripture.AllWordsHidden())
        {
            Console.WriteLine("Press Enter to continue or type 'quit' to finish:");
            Console.WriteLine(scripture.Reference + " " + GetHiddenText(scripture));
            Console.WriteLine(" ");
            Console.WriteLine("Hint: " + scripture.Hint);
            scripture.HideRandomWord();
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
            {
                return;
            }
            Console.Clear();
            
        }
    }

    static string GetHiddenText(Scripture scripture)
    {
        string hiddenText = "";
        foreach (Word word in scripture.Words)
        {
            if (word.Hidden)
            {
                hiddenText += "____ ";
            }
            else
            {
                hiddenText += word.Text + " ";
            }
        }
        return hiddenText;
    }
}