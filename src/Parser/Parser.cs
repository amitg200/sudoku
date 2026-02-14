/// <summary>
/// Responsible for converting a validated input string
/// into a <see cref="Board"/> object.
/// </summary>
public class Parser
{
    /// <summary>
    /// Parses the specified Sudoku input string and constructs
    /// a corresponding <see cref="Board"/> instance.
    /// </summary>
    /// <param name="input">
    /// A validated string representation of the Sudoku board.
    /// </param>
    /// <returns>
    /// A <see cref="Board"/> populated with values derived from the input.
    /// </returns>
    public Board Parse(string input)
    {
        var newBoard = new Board();
        int size = SudokuConstants.BoardSize;
        int row = 0;
        int col = 0;

        foreach (char c in input)
        {
            newBoard.SetCellByRowAndCol(
                row,
                col,
                SudokuConstants.InputSymbolMap[c]);

            col++;

            if (col == size)
            {
                col = 0;
                row++;
            }
        }

        return newBoard;
    }
}