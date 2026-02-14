/// <summary>
/// Defines a contract for output operations in the Sudoku application.
/// Implementations may provide output through console, GUI, file, or other mediums.
/// </summary>
public interface IOutput
{
    /// <summary>
    /// Outputs the specified string to the underlying output medium.
    /// </summary>
    /// <param name="output">
    /// The formatted Sudoku board or message to display.
    /// </param>
    void Print(string output);
}