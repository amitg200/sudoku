using System.Text;

/// <summary>
/// Provides functionality to convert a <see cref="Board"/>
/// into its string representation.
/// </summary>
public class BoardFormatter
{
    /// <summary>
    /// Converts the specified Sudoku board into a single string.
    /// The board is serialized row by row without separators.
    /// </summary>
    /// <param name="board">
    /// The Sudoku board to format.
    /// </param>
    /// <returns>
    /// A string representing the board in row-major order.
    /// </returns>
    public string Format(Board board)
    {
        int size = SudokuConstants.BoardSize;
        var builder = new StringBuilder(size * size);

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                builder.Append(board.GetCellByRowAndCol(row, col));
            }
        }

        return builder.ToString();
    }
}