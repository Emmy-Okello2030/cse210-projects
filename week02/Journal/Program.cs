using System;
using System.Collections.Generic;
using System.IO;

class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }

    public void Display()
    {
        Console.WriteLine($"Date: {Date}\nPrompt: {Prompt}\nResponse: {Response}\n");
    }
}

class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayJournal()
    {
        foreach (var entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in _entries)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
        Console.WriteLine("Journal saved successfully.");
    }

    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            _entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    _entries.Add(new Entry(parts[0], parts[1], parts[2]));
                }
            }
            Console.WriteLine("Journal loaded successfully.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}

class PromptGenerator
{
    private List<string> _prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(_prompts.Count);
        return _prompts[index];
    }
}

class Program
{
    static void Main()
    {
        Journal myJournal = new Journal();
        PromptGenerator promptGen = new PromptGenerator();

        while (true)
        {
            Console.WriteLine("\nJournal Program");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    string prompt = promptGen.GetRandomPrompt();
                    Console.WriteLine($"\n{prompt}");
                    Console.Write("Your response: ");
                    string response = Console.ReadLine();
                    myJournal.AddEntry(new Entry(DateTime.Now.ToShortDateString(), prompt, response));
                    break;
                case "2":
                    myJournal.DisplayJournal();
                    break;
                case "3":
                    Console.Write("Enter filename: ");
                    string saveFile = Console.ReadLine();
                    myJournal.SaveToFile(saveFile);
                    break;
                case "4":
                    Console.Write("Enter filename: ");
                    string loadFile = Console.ReadLine();
                    myJournal.LoadFromFile(loadFile);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }
}
