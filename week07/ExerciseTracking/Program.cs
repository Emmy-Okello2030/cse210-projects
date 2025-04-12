using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2023, 11, 3), 30, 3.0),
            new Cycling(new DateTime(2023, 11, 3), 30, 12.0),
            new Swimming(new DateTime(2023, 11, 4), 30, 40)
        };

        Console.WriteLine("Exercise Tracking Summary:");
        Console.WriteLine("=========================");
        
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
            Console.WriteLine();
        }
    }
}