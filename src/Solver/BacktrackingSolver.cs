/// <summary>
/// Backtracking-based Sudoku solver implementation.
/// Uses MRV (Minimum Remaining Values) heuristic
/// and forward checking with domain propagation.
/// </summary>
public class BacktrackingSolver : ISudokuSolver
{
    private readonly ICellSelector _cellSelector = new MrvCellSelector();
    private readonly DomainManager _domainManager = new DomainManager();
    private readonly BoardValidation _validator = new BoardValidation();
    private readonly Stack<DomainManager.DomainChange> _changeStack = new();

    /// <summary>
    /// Solves the given Sudoku board using backtracking.
    /// </summary>
    /// <param name="board">
    /// The Sudoku board to solve.
    /// </param>
    /// <returns>
    /// A solved Sudoku board.
    /// </returns>
    /// <exception cref="Exception">
    /// Thrown if the board is initially invalid
    /// or if no solution exists.
    /// </exception>
    public Board Solve(Board board)
    {
        _domainManager.InitializeDomains(board);

        for (int r = 0; r < SudokuConstants.BoardSize; r++)
        {
            for (int c = 0; c < SudokuConstants.BoardSize; c++)
            {
                int val = board.GetCellByRowAndCol(r, c);
                if (val != 0)
                {
                    if (!_domainManager.DomainUpdate(
                        board, r, c,
                        _domainManager.CreateChangeStack()))
                    {
                        throw new InvalidOperationException("Initial Sudoku board is invalid.");
                    }
                }
            }
        }

        if (!Backtracking(board))
            throw new InvalidOperationException("Sudoku has no solution.");

        if (!_validator.IsBoardValid(board))
            throw new InvalidOperationException("Solver produced invalid board.");

        return board;
    }

    /// <summary>
    /// Recursive backtracking search.
    /// Selects a cell using MRV and tries
    /// all possible domain values.
    /// </summary>
    /// <param name="board">
    /// The current board state.
    /// </param>
    /// <returns>
    /// True if a solution was found; otherwise false.
    /// </returns>
    private bool Backtracking(Board board)
    {
        int row;
        int col;

        if (!_cellSelector.TrySelectCell(
                board,
                _domainManager.GetDomains(),
                out row,
                out col))
        {
            return true;
        }

        List<int> currentCellDomain =
            _domainManager.GetDomain(row, col);

        for (int i = 0; i < currentCellDomain.Count; i++)
        {
            int snapshot = _changeStack.Count;

            board.SetCellByRowAndCol(
                row, col,
                currentCellDomain[i]);

            _domainManager.ClearDomain(row, col);

            if (!_domainManager.DomainUpdate(
                    board, row, col,
                    _changeStack))
            {
                board.SetCellByRowAndCol(row, col, 0);
                _domainManager.UndoDomainUpdate(
                    _changeStack, snapshot);
                _domainManager.SetDomain(
                    row, col,
                    currentCellDomain);
                continue;
            }

            if (Backtracking(board))
                return true;

            board.SetCellByRowAndCol(row, col, 0);
            _domainManager.UndoDomainUpdate(
                _changeStack, snapshot);
            _domainManager.SetDomain(
                row, col,
                currentCellDomain);
        }

        return false;
    }
}