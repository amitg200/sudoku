/// <summary>
/// Defines a strategy for selecting the next cell
/// to assign during Sudoku solving.
/// </summary>
public interface ICellSelector
{
    /// <summary>
    /// Attempts to select the next cell to process.
    /// </summary>
    /// <param name="board">The current Sudoku board.</param>
    /// <param name="domains">
    /// The current domain matrix representing possible values for each cell.
    /// </param>
    /// <param name="row">
    /// When this method returns true, contains the selected row index.
    /// </param>
    /// <param name="col">
    /// When this method returns true, contains the selected column index.
    /// </param>
    /// <returns>
    /// True if a cell was selected; otherwise false
    /// (meaning no empty cells remain).
    /// </returns>
    bool TrySelectCell(Board board, List<int>[,] domains, out int row, out int col);
}