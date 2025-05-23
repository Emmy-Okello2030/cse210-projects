using System;

public abstract class Activity
{
    private DateTime _date;
    private int _minutes;

    public Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public DateTime Date => _date;
    public int Minutes => _minutes;

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    public virtual string GetSummary()
    {
        return string.Format("{0} {1} ({2} min)- Distance {3:F1} miles, Speed {4:F1} mph, Pace: {5:F1} min per mile",
            _date.ToString("dd MMM yyyy"),
            GetType().Name,
            _minutes,
            GetDistance(),
            GetSpeed(),
            GetPace());
    }
}