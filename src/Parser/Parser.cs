public class Parser
{
    public Board Parse(string input)
    {
        var newBoard = new Board();
        int size = SudokuConstants.BoardSize;
        int row = 0;
        int col = 0;

        foreach (char c in input)
        {
            newBoard.SetCellByRowAndCol(row,col,SudokuConstants.InputSymbolMap[c]);
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