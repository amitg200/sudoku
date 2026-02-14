/// <summary>
/// Validates raw Sudoku input before parsing.
/// Ensures correct length and allowed symbols.
/// </summary>
public class InputValidation
{
    /// <summary>
    /// Validates the given Sudoku input string.
    /// Throws <see cref="ArgumentException"/> if validation fails.
    /// </summary>
    /// <param name="input">Raw Sudoku input string.</param>
    public void Validate(string input)
    {
        int expectedLength = SudokuConstants.BoardSize * SudokuConstants.BoardSize;

        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input is null or empty.");

        if (input.Length != expectedLength)
            throw new ArgumentException(
                $"Invalid length. Expected {expectedLength} but got {input.Length}.");

        foreach (char c in input)
        {
            if (!SudokuConstants.InputSymbolMap.ContainsKey(c))
                throw new ArgumentException($"Invalid character '{c}'.");
        }
    }
}