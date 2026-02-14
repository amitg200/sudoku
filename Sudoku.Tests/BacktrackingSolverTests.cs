using System.Diagnostics;

public class BacktrackingSolverTests
{
    [Fact]
    public void Solve_AlreadySolved_ReturnsValidBoard()
    {
        var parser = new Parser();
        var solver = new BacktrackingSolver();
        var validator = new BoardValidation();

        string puzzle =
            "534678912672195348198342567859761423426853791713924856961537284287419635345286179";

        Board board = parser.Parse(puzzle);
        Board solved = solver.Solve(board);

        Assert.True(validator.IsBoardValid(solved));
    }

    [Fact]
    public void Solve_AllPuzzles_UnderOneSecond()
    {
        var parser = new Parser();
        var solver = new BacktrackingSolver();
        var validator = new BoardValidation();

        string filePath = Path.Combine(
            AppContext.BaseDirectory,
            "sudokus.txt");

        var puzzles = File.ReadAllLines(filePath)
                          .Where(line => !string.IsNullOrWhiteSpace(line));

        var slowPuzzles = new List<string>();

        foreach (var puzzle in puzzles)
        {
            Board board = parser.Parse(puzzle);

            var stopwatch = Stopwatch.StartNew();
            Board solved = solver.Solve(board);
            stopwatch.Stop();

            if (!validator.IsBoardValid(solved))
            {
                slowPuzzles.Add($"Invalid solution on {puzzle}");
                continue;
            }

            if (stopwatch.ElapsedMilliseconds >= 1000)
            {
                slowPuzzles.Add(
                    $"Took {stopwatch.ElapsedMilliseconds} ms on {puzzle}");
            }
        }

        Assert.True(
            slowPuzzles.Count == 0,
            "Failing puzzles:\n" + string.Join("\n", slowPuzzles));
    }
}