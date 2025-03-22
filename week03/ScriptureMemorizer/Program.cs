using ScriptureMemorizer;

public class Program
{
    public static void Main(string[] args)
    {
        // Step 1: Create a scripture reference
        Reference reference = new Reference("Proverbs", 3, 5, 6);

        // Step 2: Create the scripture using the reference and text
        Scripture scripture = new Scripture(reference, "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways acknowledge Him, and He shall direct your paths.");

        // Step 3: Main program loop
        while (true)
        {
            // Clear the console and display the scripture
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());

            // Prompt the user for input
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break; // Exit the program
            }

            // Hide random words in the scripture
            scripture.HideRandomWords(3);

            // Check if all words are hidden
            if (scripture.AllWordsHidden())
            {
                Console.Clear();
                Console.WriteLine("Congratulations! You have successfully memorized the scripture:");
                Console.WriteLine(scripture.GetDisplayText());
                break;
            }
        }

        Console.WriteLine("\nProgram ended. Thank you for using the Scripture Memorizer!");
    }
}
