public class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints, string category) 
        : base(name, description, points, category)
    {
        _targetCount = targetCount;
        _currentCount = 0;
        _bonusPoints = bonusPoints;
    }

    public override void RecordProgress()
    {
        _currentCount++;
        if (_currentCount >= _targetCount)
        {
            _isComplete = true;
        }
    }

    public override string GetProgressString()
    {
        string progressBar = GetProgressBar();
        return $"{GetCategorySymbol()} {GetCheckbox()} {_name} ({_description}) - Completed {_currentCount}/{_targetCount}\n{progressBar}";
    }

    private string GetProgressBar()
    {
        int width = 20;
        int filled = (int)Math.Round((double)_currentCount / _targetCount * width);
        filled = Math.Min(filled, width);
        return $"[{new string('█', filled)}{new string(' ', width - filled)}]";
    }

    public int GetBonusPoints()
    {
        return _isComplete ? _bonusPoints : 0;
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{_name},{_description},{_points},{_currentCount},{_targetCount},{_bonusPoints},{_category}";
    }
}