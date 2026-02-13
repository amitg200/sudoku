using System.Diagnostics;
class Program
{
    static void Main()
    {
        while (true)
        {
            IInput inputConsole = new InputConsole();
            string data = inputConsole.ReadInput();

            var stopwatch = Stopwatch.StartNew();// start timer
            string output = "";
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
                output = boardFormatter.Format(solvedBoard);

                IOutput outputConsole = new OutputConsole();
                outputConsole.Print(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }

            stopwatch.Stop();
            Console.WriteLine($"Time it took to solve: {stopwatch.Elapsed}\nSolved board:{output}"); 
        }
    }
}