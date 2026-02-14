/// <summary>
/// Validates Sudoku board structure and constraints.
/// Ensures rows, columns, and subgrids contain no duplicate values.
/// </summary>
public class BoardValidation
{
    /// <summary>
    /// Validates the entire board.
    /// Throws <see cref="ArgumentException"/> if the board is invalid.
    /// </summary>
    /// <param name="board">The Sudoku board to validate.</param>
    public void Validate(Board board)
    {
        if (!IsBoardValid(board))
            throw new ArgumentException("Invalid Sudoku board.");
    }

    /// <summary>
    /// Checks whether the entire board satisfies Sudoku rules.
    /// </summary>
    /// <param name="board">The Sudoku board to validate.</param>
    /// <returns>True if valid; otherwise false.</returns>
    public bool IsBoardValid(Board board)
    {
        int size = SudokuConstants.BoardSize;

        for (int i = 0; i < size; i++)
        {
            if (!IsRowValid(board, i)) return false;
            if (!IsColumnValid(board, i)) return false;
            if (!IsSubGridValid(board, i)) return false;
        }

        return true;
    }

    /// <summary>
    /// Checks whether a specific cell placement maintains Sudoku constraints.
    /// </summary>
    /// <param name="board">The Sudoku board.</param>
    /// <param name="row">Row index.</param>
    /// <param name="col">Column index.</param>
    /// <returns>True if placement is valid; otherwise false.</returns>
    public bool IsCellPlacementValid(Board board, int row, int col)
    {
        return
            IsRowValid(board, row) &&
            IsColumnValid(board, col) &&
            IsSubGridValid(board, GetSubGridIndex(row, col));
    }

    /// <summary>
    /// Validates that a row contains no duplicate values.
    /// </summary>
    private bool IsRowValid(Board board, int row)
    {
        var seen = new HashSet<int>();

        for (int col = 0; col < SudokuConstants.BoardSize; col++)
        {
            int value = board.GetCellByRowAndCol(row, col);
            if (value == 0) continue;
            if (!seen.Add(value)) return false;
        }

        return true;
    }

    /// <summary>
    /// Validates that a column contains no duplicate values.
    /// </summary>
    private bool IsColumnValid(Board board, int col)
    {
        var seen = new HashSet<int>();

        for (int row = 0; row < SudokuConstants.BoardSize; row++)
        {
            int value = board.GetCellByRowAndCol(row, col);
            if (value == 0) continue;
            if (!seen.Add(value)) return false;
        }

        return true;
    }

    /// <summary>
    /// Validates that a subgrid contains no duplicate values.
    /// </summary>
    private bool IsSubGridValid(Board board, int subGridIndex)
    {
        var seen = new HashSet<int>();

        for (int cellIndex = 0; cellIndex < SudokuConstants.BoardSize; cellIndex++)
        {
            int value = board.GetCellInSubGrid(subGridIndex, cellIndex);
            if (value == 0) continue;
            if (!seen.Add(value)) return false;
        }

        return true;
    }

    /// <summary>
    /// Calculates subgrid index based on row and column.
    /// </summary>
    private int GetSubGridIndex(int row, int col)
    {
        int sub = SudokuConstants.SubGridSize;
        return (row / sub) * sub + (col / sub);
    }
}