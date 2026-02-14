using System.Diagnostics;

/// <summary>
/// Entry point of the Sudoku application.
/// Responsible for orchestrating input, validation,
/// solving, formatting, and output.
/// </summary>
public class Program
{
    /// <summary>
    /// Main execution loop of the application.
    /// Continuously reads Sudoku input, validates,
    /// solves, and prints the result.
    /// The application exits when the user types "quit".
    /// </summary>
    static void Main()
    {
        while (true)
        {
            IInput input = new InputConsole();
            string data = input.ReadInput();
            if (data.ToLower() == "quit") break;

            var stopwatch = Stopwatch.StartNew();// start timer
            string outputString = "";
            try
            {
                var inputValidation = new InputValidation();
                inputValidation.Validate(data);

                var parser = new Parser();
                Board boardData = parser.Parse(data);

                var boardValidation = new BoardValidation();
                boardValidation.Validate(boardData); 

                ISudokuSolver solver = new BacktrackingSolver();
                Board solvedBoard = solver.Solve(boardData);

                var boardFormatter = new BoardFormatter();
                outputString = boardFormatter.Format(solvedBoard);

                IOutput output = new OutputConsole();
                output.Print(data);
                output.Print(outputString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }

            stopwatch.Stop();
            Console.WriteLine($"Time it took to solve: {stopwatch.Elapsed}\nSolved board:{outputString}"); 
        }
    }
}