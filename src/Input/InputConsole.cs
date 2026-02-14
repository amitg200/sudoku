/// <summary>
/// Console-based implementation of the <see cref="IInput"/> interface.
/// Responsible for reading Sudoku input from the standard console.
/// </summary>
public class InputConsole : IInput
{
    /// <summary>
    /// Prompts the user to enter a Sudoku board string via the console.
    /// The method continues prompting until a non-empty input is provided.
    /// </summary>
    /// <returns>
    /// A non-null, non-empty string representing the Sudoku board.
    /// </returns>
    public string ReadInput()
    {
        string? input;
        do
        {
            Console.WriteLine("Hello, please type in the sudoku:");
            input = Console.ReadLine();
        }
        while (string.IsNullOrEmpty(input));

        return input;
    }
}