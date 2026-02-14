/// <summary>
/// Console-based implementation of the <see cref="IOutput"/> interface.
/// Responsible for displaying a formatted Sudoku board in the console.
/// </summary>
public class OutputConsole : IOutput
{
    /// <summary>
    /// Prints the formatted Sudoku board to the console.
    /// Empty cells are displayed using the configured empty cell symbol.
    /// The board is visually separated into sub-grids.
    /// </summary>
    /// <param name="output">
    /// A string representing the Sudoku board in row-major order.
    /// </param>
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
        Console.WriteLine();
    }
}