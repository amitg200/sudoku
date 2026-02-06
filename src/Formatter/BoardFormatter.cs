using System.Text;

class BoardFormatter
{
    public string Format(Board board)
    {
        int size = SudokuConstants.BoardSize;
        var sb = new StringBuilder(size * size);

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                sb.Append(board.GetCellByRowAndCol(row, col));
            }
        }

        return sb.ToString();
    }
}