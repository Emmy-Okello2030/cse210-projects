public abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;
    protected bool _isComplete;
    protected string _category;

    public Goal(string name, string description, int points, string category)
    {
        _name = name;
        _description = description;
        _points = points;
        _category = category;
        _isComplete = false;
    }

    public abstract void RecordProgress();
    public abstract string GetProgressString();
    public abstract string GetStringRepresentation();

    public bool IsComplete()
    {
        return _isComplete;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetDescription()
    {
        return _description;
    }

    public int GetPoints()
    {
        return _points;
    }

    public string GetCategory()
    {
        return _category;
    }

    protected string GetCheckbox()
    {
        return _isComplete ? "[X]" : "[ ]";
    }

    protected string GetCategorySymbol()
    {
        return _category switch
        {
            "Spiritual" => "✝",
            "Physical" => "🏋️",
            "Social" => "👥",
            "Intellectual" => "📚",
            _ => "★"
        };
    }
}