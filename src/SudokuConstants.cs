/// <summary>
/// Contains global constant values and shared configuration
/// used throughout the Sudoku application.
/// </summary>
public static class SudokuConstants
{
    /// <summary>
    /// The size of the Sudoku board (number of rows and columns).
    /// Standard Sudoku is 9x9.
    /// </summary>
    public const int BoardSize = 9;

    /// <summary>
    /// The size of a single subgrid within the Sudoku board.
    /// Standard Sudoku uses 3x3 subgrids.
    /// </summary>
    public const int SubGridSize = 3;

    /// <summary>
    /// The character used to represent an empty cell when printing the board.
    /// </summary>
    public const char EmptyCellOutputSymbol = 'x';

    /// <summary>
    /// Maps input characters to their corresponding integer values
    /// used internally in the board representation.
    /// </summary>
    public static readonly Dictionary<char, int> InputSymbolMap = new()
        {
            { '0', 0 }, // Can be changed to { '.', 0 } if input format changes.
            { '1', 1 },
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 }
        };

    /// <summary>
    /// An array containing all possible numeric values
    /// that can appear on the Sudoku board.
    /// </summary>
    public static readonly int[] AllValues = InputSymbolMap.Values.ToArray();
}