public class BacktrackingSolver : ISudokuSolver
{
    private readonly ICellSelector _cellSelector = new MrvCellSelector();
    private readonly DomainManager _domainManager = new DomainManager();
    private readonly BoardValidation _validator = new BoardValidation();
    private readonly Stack<DomainManager.DomainChange> _changeStack = new();
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

                    if (!_domainManager.DomainUpdate(board, r, c, _domainManager.CreateChangeStack()))
                    {
                        throw new Exception("Initial Sudoku board is invalid.");
                    }
                }
            }
        }

        if (!Backtracking(board))
            throw new Exception("Sudoku has no solution.");

        if (!_validator.IsBoardValid(board)) throw new Exception("Solver produced invalid board.");// for me to check
        return board;
    }

    private bool Backtracking(Board board)
    {
        int row;
        int col;
        if (!_cellSelector.TrySelectCell(board, _domainManager.GetDomains(),out row, out col))//here i need somains public
        {
            return true;
        }

        List<int> currentCellDomain = _domainManager.GetDomain(row, col);
        for (int i = 0; i < currentCellDomain.Count; i++)
        {
            int snapshot = _changeStack.Count;

            board.SetCellByRowAndCol(row, col,currentCellDomain[i]);

            _domainManager.ClearDomain(row, col);

            if (!_domainManager.DomainUpdate(board, row, col, _changeStack))
            {
                board.SetCellByRowAndCol(row, col, 0);
                _domainManager.UndoDomainUpdate(_changeStack, snapshot);
                _domainManager.SetDomain(row, col, currentCellDomain);
                continue;
            }

            if (Backtracking(board))
                return true;

            board.SetCellByRowAndCol(row, col, 0);
            _domainManager.UndoDomainUpdate(_changeStack, snapshot);
            _domainManager.SetDomain(row, col, currentCellDomain);
        }
        return false;
    }
}