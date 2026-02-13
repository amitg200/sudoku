class OutputConsole : IOutput
{
    public void Print(string output)
    {
        int size = SudokuConstants.BoardSize;
        int sub = SudokuConstants.SubGridSize;

        int col = 0;
        int row = 0;

        foreach (char c in output)
        {
            if (c == '0')
                Console.Write(SudokuConstants.EmptyCellOutputSymbol);
            else
                Console.Write(c);

            col++;

            if (col < size && col % sub == 0)
                Console.Write('|');

            if (col == size)
            {
                Console.WriteLine();
                col = 0;
                row++;

                if (row < size && row % sub == 0)
                    Console.WriteLine(new string('-', size + sub - 1));
            }
        }
    }
}
