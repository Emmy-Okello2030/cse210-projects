using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;
    private int _level;
    private int _streakDays;
    private DateTime _lastActivityDate;
    private List<string> _motivationalQuotes;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _level = 1;
        _streakDays = 0;
        _lastActivityDate = DateTime.MinValue;
        InitializeMotivationalQuotes();
    }

    private void InitializeMotivationalQuotes()
    {
        _motivationalQuotes = new List<string>
        {
            "Small steps every day lead to big results!",
            "You're making eternal progress!",
            "Heavenly Father is pleased with your efforts!",
            "Consistency is the key to success!",
            "Every effort counts in your eternal quest!",
            "You're becoming your best self!",
            "The Lord sees your righteous desires!"
        };
    }

    public void Start()
    {
        LoadGoals();
        UpdateStreak();
        
        while (true)
        {
            Console.Clear();
            DisplayScore();
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Goal Progress");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. Quit");

            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    DisplayGoals();
                    break;
                case "3":
                    RecordGoalProgress();
                    break;
                case "4":
                    SaveGoals();
                    break;
                case "5":
                    LoadGoals();
                    break;
                case "6":
                    Console.WriteLine("Goodbye! Keep working on your eternal quest!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    private void UpdateStreak()
    {
        DateTime today = DateTime.Today;
        if (_lastActivityDate == DateTime.MinValue)
        {
            _streakDays = 0;
        }
        else if (_lastActivityDate.AddDays(1) == today)
        {
            _streakDays++;
        }
        else if (_lastActivityDate != today)
        {
            _streakDays = 1;
        }
        _lastActivityDate = today;
    }

    private void DisplayScore()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Current Score: {_score} points");
        Console.WriteLine($"Level: {_level} (Next level at {_level * 1000} points)");
        Console.WriteLine($"Current Streak: {_streakDays} days");
        
        // Display progress to next level
        int nextLevelThreshold = _level * 1000;
        int progress = _score - ((_level - 1) * 1000);
        double percentage = (double)progress / 1000 * 100;
        Console.WriteLine($"Progress to Level {_level + 1}: {percentage:F1}%");
        
        Console.ResetColor();
    }

    private void CreateGoal()
    {
        Console.WriteLine("\nThe types of Goals are:");
        Console.WriteLine("1. Simple Goal (one-time)");
        Console.WriteLine("2. Eternal Goal (ongoing)");
        Console.WriteLine("3. Checklist Goal (requires multiple completions)");
        Console.Write("Which type of goal would you like to create? ");
        string goalType = Console.ReadLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();

        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine());

        Console.WriteLine("\nSelect a category:");
        Console.WriteLine("1. Spiritual");
        Console.WriteLine("2. Physical");
        Console.WriteLine("3. Social");
        Console.WriteLine("4. Intellectual");
        Console.Write("Enter your choice: ");
        string categoryChoice = Console.ReadLine();
        string category = categoryChoice switch
        {
            "1" => "Spiritual",
            "2" => "Physical",
            "3" => "Social",
            "4" => "Intellectual",
            _ => "Other"
        };

        Goal goal = null;

        switch (goalType)
        {
            case "1":
                goal = new SimpleGoal(name, description, points, category);
                break;
            case "2":
                goal = new EternalGoal(name, description, points, category);
                break;
            case "3":
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                int targetCount = int.Parse(Console.ReadLine());
                Console.Write("What is the bonus for accomplishing it that many times? ");
                int bonusPoints = int.Parse(Console.ReadLine());
                goal = new ChecklistGoal(name, description, points, targetCount, bonusPoints, category);
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                return;
        }

        _goals.Add(goal);
        Console.WriteLine("Goal created successfully!");
    }

    private void DisplayGoals()
    {
        Console.WriteLine("\nYour Goals:");
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals have been created yet.");
            return;
        }

        // Group goals by category
        var groupedGoals = new Dictionary<string, List<Goal>>();
        foreach (var goal in _goals)
        {
            if (!groupedGoals.ContainsKey(goal.GetCategory()))
            {
                groupedGoals[goal.GetCategory()] = new List<Goal>();
            }
            groupedGoals[goal.GetCategory()].Add(goal);
        }

        foreach (var category in groupedGoals)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{category.Key} Goals:");
            Console.ResetColor();

            for (int i = 0; i < category.Value.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {category.Value[i].GetProgressString()}");
            }
        }
    }

    private void RecordGoalProgress()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals available to record.");
            return;
        }

        DisplayGoals();
        Console.Write("\nWhich goal did you accomplish? (Enter the number): ");
        if (int.TryParse(Console.ReadLine(), out int goalNumber) && goalNumber > 0 && goalNumber <= _goals.Count)
        {
            Goal selectedGoal = _goals[goalNumber - 1];
            selectedGoal.RecordProgress();

            int pointsEarned = selectedGoal.GetPoints();
            string message = $"\nCongratulations! You earned {pointsEarned} points!";

            // Check for bonus points (for checklist goals)
            if (selectedGoal is ChecklistGoal checklistGoal && checklistGoal.IsComplete())
            {
                int bonusPoints = checklistGoal.GetBonusPoints();
                pointsEarned += bonusPoints;
                message += $" Plus a bonus of {bonusPoints} points for completing the goal!";
            }

            // Add streak bonus if applicable
            if (_streakDays >= 3)
            {
                int streakBonus = (int)(pointsEarned * (_streakDays * 0.05)); // 5% per day of streak
                pointsEarned += streakBonus;
                message += $" Plus a {streakBonus} point streak bonus ({_streakDays} days)!";
            }

            _score += pointsEarned;
            Console.WriteLine(message);

            // Display random motivational quote
            Random rnd = new Random();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n{_motivationalQuotes[rnd.Next(_motivationalQuotes.Count)]}");
            Console.ResetColor();

            // Check for level up
            CheckLevelUp();

            // Update last activity date for streak tracking
            _lastActivityDate = DateTime.Today;
        }
        else
        {
            Console.WriteLine("Invalid goal selection.");
        }
    }

    private void CheckLevelUp()
    {
        int newLevel = _score / 1000 + 1;
        if (newLevel > _level)
        {
            _level = newLevel;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n★ ★ ★ LEVEL UP! You've reached Level {_level}! ★ ★ ★");
            Console.ResetColor();
        }
    }

    private void SaveGoals()
    {
        Console.Write("Enter the filename to save goals: ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);
            outputFile.WriteLine(_level);
            outputFile.WriteLine(_streakDays);
            outputFile.WriteLine(_lastActivityDate.ToString("yyyy-MM-dd"));

            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved successfully!");
    }

    private void LoadGoals()
    {
        Console.Write("Enter the filename to load goals: ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);
            
            // First line is the score
            if (lines.Length > 0 && int.TryParse(lines[0], out int score))
            {
                _score = score;
            }

            // Second line is the level
            if (lines.Length > 1 && int.TryParse(lines[1], out int level))
            {
                _level = level;
            }

            // Third line is streak days
            if (lines.Length > 2 && int.TryParse(lines[2], out int streakDays))
            {
                _streakDays = streakDays;
            }

            // Fourth line is last activity date
            if (lines.Length > 3 && DateTime.TryParse(lines[3], out DateTime lastDate))
            {
                _lastActivityDate = lastDate;
            }

            _goals.Clear();
            for (int i = 4; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(':');
                if (parts.Length == 2)
                {
                    string goalType = parts[0];
                    string[] goalData = parts[1].Split(',');

                    switch (goalType)
                    {
                        case "SimpleGoal":
                            if (goalData.Length >= 5)
                            {
                                bool isComplete = bool.Parse(goalData[3]);
                                SimpleGoal simpleGoal = new SimpleGoal(goalData[0], goalData[1], int.Parse(goalData[2]), goalData[4]);
                                if (isComplete) simpleGoal.RecordProgress();
                                _goals.Add(simpleGoal);
                            }
                            break;
                        case "EternalGoal":
                            if (goalData.Length >= 5)
                            {
                                EternalGoal eternalGoal = new EternalGoal(goalData[0], goalData[1], int.Parse(goalData[2]), goalData[4]);
                                if (int.TryParse(goalData[3], out int timesCompleted))
                                {
                                    for (int j = 0; j < timesCompleted; j++) eternalGoal.RecordProgress();
                                }
                                _goals.Add(eternalGoal);
                            }
                            break;
                        case "ChecklistGoal":
                            if (goalData.Length >= 7)
                            {
                                ChecklistGoal checklistGoal = new ChecklistGoal(
                                    goalData[0], 
                                    goalData[1], 
                                    int.Parse(goalData[2]), 
                                    int.Parse(goalData[4]), 
                                    int.Parse(goalData[5]), 
                                    goalData[6]);

                                if (int.TryParse(goalData[3], out int currentCount))
                                {
                                    for (int j = 0; j < currentCount; j++) checklistGoal.RecordProgress();
                                }
                                _goals.Add(checklistGoal);
                            }
                            break;
                    }
                }
            }

            Console.WriteLine("Goals loaded successfully!");
            UpdateStreak();
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}