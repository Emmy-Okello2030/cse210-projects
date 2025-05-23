using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}

/*
CREATIVE ENHANCEMENTS IMPLEMENTED:
1. Level System - User levels up based on total points earned
2. Streak Tracking - Tracks consecutive days with activity and awards bonus
3. Goal Categories - Goals organized into Spiritual, Physical, Social, Intellectual categories
4. Visual Progress Bars - Shows progress toward goals with ASCII art bars
5. Motivational Quotes - Displays random encouragement when recording progress
6. Color Coding - Different goal types shown in different colors
7. Milestone Celebrations - Special messages for significant achievements
*/