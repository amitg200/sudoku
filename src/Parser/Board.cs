/// <summary>
/// Represents the internal Sudoku board structure.
/// Provides access methods by row/column and by sub-grid indexing.
/// </summary>
public class Board
{
    private int[,] _board;

    /// <summary>
    /// Initializes a new empty Sudoku board
    /// with the configured board size.
    /// </summary>
    public Board()
    {
        _board = new int[
            SudokuConstants.BoardSize,
            SudokuConstants.BoardSize];
    }

    /// <summary>
    /// Retrieves the value of a cell within a specific sub-grid.
    /// </summary>
    /// <param name="subGridIndex">
    /// The index of the sub-grid (0 to BoardSize - 1).
    /// </param>
    /// <param name="cellIndex">
    /// The index of the cell inside the sub-grid.
    /// </param>
    /// <returns>
    /// The value stored in the specified sub-grid cell.
    /// </returns>
    public int GetCellInSubGrid(int subGridIndex, int cellIndex)
    {
        int sub = SudokuConstants.SubGridSize;

        int startRow = (subGridIndex / sub) * sub;
        int startCol = (subGridIndex % sub) * sub;

        int rowOffset = cellIndex / sub;
        int colOffset = cellIndex % sub;

        return _board[startRow + rowOffset, startCol + colOffset];
    }

    /// <summary>
    /// Sets the value of a cell within a specific sub-grid.
    /// </summary>
    /// <param name="subGridIndex">
    /// The index of the sub-grid (0 to BoardSize - 1).
    /// </param>
    /// <param name="cellIndex">
    /// The index of the cell inside the sub-grid.
    /// </param>
    /// <param name="value">
    /// The value to assign to the cell.
    /// </param>
    public void SetCellInSubGrid(int subGridIndex, int cellIndex, int value)
    {
        int sub = SudokuConstants.SubGridSize;

        int startRow = (subGridIndex / sub) * sub;
        int startCol = (subGridIndex % sub) * sub;

        int rowOffset = cellIndex / sub;
        int colOffset = cellIndex % sub;

        _board[startRow + rowOffset, startCol + colOffset] = value;
    }

    /// <summary>
    /// Retrieves the value of a cell by its row and column coordinates.
    /// </summary>
    /// <param name="row">The row index of the cell.</param>
    /// <param name="col">The column index of the cell.</param>
    /// <returns>The value stored in the specified cell.</returns>
    public int GetCellByRowAndCol(int row, int col)
    {
        return _board[row, col];
    }

    /// <summary>
    /// Sets the value of a cell by its row and column coordinates.
    /// </summary>
    /// <param name="row">The row index of the cell.</param>
    /// <param name="col">The column index of the cell.</param>
    /// <param name="value">The value to assign to the cell.</param>
    public void SetCellByRowAndCol(int row, int col, int value)
    {
        _board[row, col] = value;
    }
}