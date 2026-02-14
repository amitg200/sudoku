/// <summary>
/// Defines a contract for a Sudoku solving algorithm.
/// </summary>
public interface ISudokuSolver
{
    /// <summary>
    /// Solves the given Sudoku board.
    /// </summary>
    /// <param name="board">
    /// The board to solve. The board is modified in-place.
    /// </param>
    /// <returns>
    /// The solved Sudoku board.
    /// </returns>
    Board Solve(Board board);
}