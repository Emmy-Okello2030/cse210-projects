using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create 3 videos
        var video1 = new Video
        {
            _title = "C# Tutorial for Beginners",
            _author = "ProgrammingMaster",
            _lengthInSeconds = 600
        };

        var video2 = new Video
        {
            _title = "Learn Python in 10 Minutes",
            _author = "CodeNinja",
            _lengthInSeconds = 720
        };

        var video3 = new Video
        {
            _title = "ASP.NET Core Crash Course",
            _author = "DotNetExpert",
            _lengthInSeconds = 900
        };

        // Add comments to video1 (correct syntax)
        video1.AddComment(new Comment { _commenterName = "User1", _commentText = "Great tutorial!" });
        video1.AddComment(new Comment { _commenterName = "User2", _commentText = "Very helpful!" });
        video1.AddComment(new Comment { _commenterName = "User3", _commentText = "I learned a lot" });

        // Add comments to video2
        video2.AddComment(new Comment { _commenterName = "PythonFan", _commentText = "Short and sweet" });
        video2.AddComment(new Comment { _commenterName = "Newbie", _commentText = "Perfect for beginners" });
        video2.AddComment(new Comment { _commenterName = "Dev123", _commentText = "Can you make more?" });

        // Add comments to video3
        video3.AddComment(new Comment { _commenterName = "WebDev", _commentText = "Awesome content" });
        video3.AddComment(new Comment { _commenterName = "Student", _commentText = "Exactly what I needed" });
        video3.AddComment(new Comment { _commenterName = "Coder", _commentText = "Very clear explanations" });

        // Store videos in a list
        var videos = new List<Video> { video1, video2, video3 };

        // Display video information
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video._title}");
            Console.WriteLine($"Author: {video._author}");
            Console.WriteLine($"Length: {video._lengthInSeconds} seconds");
            Console.WriteLine($"Comments ({video.GetNumberOfComments()}):");
            
            foreach (var comment in video.GetComments())
            {
                Console.WriteLine($"- {comment._commenterName}: {comment._commentText}");
            }
            Console.WriteLine();
        }
    }
}