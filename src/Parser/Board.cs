class Board
{
    private int[,] board;
    
    public Board()
    {
        board = new int[SudokuConstants.BoardSize , SudokuConstants.BoardSize];
    }
    public int GetCellInSubGrid(int subGridIndex, int cellIndex)
    {
        int sub = SudokuConstants.SubGridSize;

        return board[(subGridIndex / sub) * sub + (cellIndex / sub),
                     (subGridIndex % sub) * sub + (cellIndex % sub)];
    }
    public void SetCellInSubGrid(int subGridIndex, int cellIndex, int value)
    {
        int sub = SudokuConstants.SubGridSize;

        board[(subGridIndex / sub) * sub + (cellIndex / sub),
              (subGridIndex % sub) * sub + (cellIndex % sub)] = value;
    }
    public int GetCellByRowAndCol(int row, int col)
    {
        return board[row,col];
    }
    public void SetCellByRowAndCol(int row, int col, int value)
    {
        board[row,col] = value;
    }
}