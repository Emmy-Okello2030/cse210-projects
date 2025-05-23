public class EternalGoal : Goal
{
    private int _timesCompleted;

    public EternalGoal(string name, string description, int points, string category) 
        : base(name, description, points, category) 
    {
        _timesCompleted = 0;
    }

    public override void RecordProgress()
    {
        _timesCompleted++;
    }

    public override string GetProgressString()
    {
        return $"{GetCategorySymbol()} {GetCheckbox()} {_name} ({_description}) - Completed {_timesCompleted} times";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{_name},{_description},{_points},{_timesCompleted},{_category}";
    }
}