public class BoardValidation
{
    public void Validate(Board board)
    {
        if (!IsBoardValid(board))
        {
            throw new ArgumentException("Invalid Sudoku board.");
        }
    }

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

    public bool IsCellPlacementValid(Board board, int row, int col)
    {
        return
            IsRowValid(board, row) &&
            IsColumnValid(board, col) &&
            IsSubGridValid(board, GetSubGridIndex(row, col));
    }

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

    private int GetSubGridIndex(int row, int col)
    {
        int sub = SudokuConstants.SubGridSize;
        return (row / sub) * sub + (col / sub);
    }
}