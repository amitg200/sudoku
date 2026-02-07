class MrvCellSelector : ICellSelector
{
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
                    if (domains[row,col].Count < minOptions)
                    {
                        minOptions = domains[row,col].Count;
                        nextRow = row;
                        nextCol = col;
                        if (minOptions == 1) return true;
                    }
                }
            }
        }
        if (minOptions < SudokuConstants.AllValues.Length + 1)
            return true;
        return false;
    }
}