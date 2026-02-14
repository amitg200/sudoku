/// <summary>
/// Selects the next cell to assign using the
/// Minimum Remaining Values (MRV) heuristic.
/// </summary>
class MrvCellSelector : ICellSelector
{
    /// <summary>
    /// Attempts to select the next empty cell with the smallest domain size.
    /// </summary>
    /// <param name="board">
    /// The current Sudoku board.
    /// </param>
    /// <param name="domains">
    /// A 2D array representing the domain of possible values for each cell.
    /// </param>
    /// <param name="nextRow">
    /// Output parameter that receives the selected row index.
    /// </param>
    /// <param name="nextCol">
    /// Output parameter that receives the selected column index.
    /// </param>
    /// <returns>
    /// True if an empty cell was found; otherwise false if the board is complete.
    /// </returns>
    public bool TrySelectCell(Board board, List<int>[,] domains, out int nextRow, out int nextCol)
    {
        nextRow = -1;
        nextCol = -1;
        int minOptions = SudokuConstants.AllValues.Length + 1;

        for (int row = 0; row < SudokuConstants.BoardSize; row++)
        {
            for (int col = 0; col < SudokuConstants.BoardSize; col++)
            {
                if (board.GetCellByRowAndCol(row, col) == 0 && domains[row, col] != null)
                {
                    if (domains[row, col].Count < minOptions)
                    {
                        minOptions = domains[row, col].Count;
                        nextRow = row;
                        nextCol = col;

                        if (minOptions == 1)
                            return true;
                    }
                }
            }
        }

        return minOptions < SudokuConstants.AllValues.Length + 1;
    }
}