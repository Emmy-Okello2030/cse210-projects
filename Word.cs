namespace ScriptureMemorizer;

public class Word
{
    private string _text;
    private bool _isHidden;

    // Constructor
    public Word(string text)
    {
        _text = text;
        _isHidden = false; // Words are initially visible
    }

    // Property to check if the word is hidden
    public bool IsHidden
    {
        get { return _isHidden; }
    }

    // Method to hide the word
    public void Hide()
    {
        _isHidden = true;
    }

    // Method to get the display text (hidden or original)
    public string GetDisplayText()
    {
        return _isHidden ? new string('_', _text.Length) : _text;
    }
}
