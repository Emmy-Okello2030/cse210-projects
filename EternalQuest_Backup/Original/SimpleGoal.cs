public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points, string category) 
        : base(name, description, points, category) { }

    public override void RecordProgress()
    {
        _isComplete = true;
    }

    public override string GetProgressString()
    {
        return $"{GetCategorySymbol()} {GetCheckbox()} {_name} ({_description})";
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{_name},{_description},{_points},{_isComplete},{_category}";
    }
}