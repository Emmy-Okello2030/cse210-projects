namespace ScriptureMemorizer;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        if (reference == null)
        {
            throw new ArgumentNullException(nameof(reference), "Reference cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Text cannot be null or empty.", nameof(text));
        }

        _reference = reference;
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public string GetDisplayText()
    {
        string displayText = $"{_reference.GetFullReference()} - " +
                             string.Join(" ", _words.Select(word => word.GetDisplayText()));

        // Debugging output
        Console.WriteLine("Current Scripture Display:");
        Console.WriteLine(displayText);
        return displayText;
    }

    public void HideRandomWords(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentException("Count must be greater than zero.", nameof(count));
        }

        Random random = new Random();
        int hiddenCount = 0;

        for (int i = 0; i < count; i++)
        {
            var wordToHide = _words[random.Next(_words.Count)];

            // Hide only if the word is not already hidden
            if (!wordToHide.IsHidden)
            {
                wordToHide.Hide();
                hiddenCount++;
            }
        }

        // Debugging output
        Console.WriteLine($"{hiddenCount} words hidden in this step.");
    }

    public bool AllWordsHidden()
    {
        return _words.All(word => word.IsHidden);
    }
}
