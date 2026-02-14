/// <summary>
/// Defines a contract for reading input data for the Sudoku application.
/// Implementations can provide input from different sources such as
/// console, file, GUI.
/// </summary>
public interface IInput
{
    /// <summary>
    /// Reads a Sudoku input representation from the underlying source.
    /// </summary>
    /// <returns>
    /// A string representing the Sudoku board in its raw input format.
    /// </returns>
    string ReadInput();
}