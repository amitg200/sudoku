/// <summary>
/// Maintains and updates domains for each empty cell during solving.
/// Supports forward-checking by removing invalid values from neighbor domains
/// and restoring them when backtracking.
/// </summary>
public class DomainManager
{
    private List<int>[,] _domains;

    /// <summary>
    /// Represents a single domain change for a specific cell and value.
    /// Used to undo domain updates during backtracking.
    /// </summary>
    public readonly struct DomainChange
    {
        /// <summary>
        /// Row index of the cell.
        /// </summary>
        public readonly int Row;

        /// <summary>
        /// Column index of the cell.
        /// </summary>
        public readonly int Col;

        /// <summary>
        /// The value that was removed from the domain.
        /// </summary>
        public readonly int Value;

        /// <summary>
        /// Creates a new domain change record.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="col">Column index.</param>
        /// <param name="value">Removed value.</param>
        public DomainChange(int row, int col, int value)
        {
            Row = row;
            Col = col;
            Value = value;
        }
    }

    /// <summary>
    /// Creates a new stack used for tracking domain changes.
    /// </summary>
    /// <returns>A stack for domain changes.</returns>
    public Stack<DomainChange> CreateChangeStack()
    {
        return new Stack<DomainChange>();
    }

    /// <summary>
    /// Initializes domains for every cell based on the current board state.
    /// Filled cells have a null domain.
    /// </summary>
    /// <param name="board">The current Sudoku board.</param>
    public void InitializeDomains(Board board)
    {
        _domains = new List<int>[SudokuConstants.BoardSize, SudokuConstants.BoardSize];

        for (int row = 0; row < SudokuConstants.BoardSize; row++)
        {
            for (int col = 0; col < SudokuConstants.BoardSize; col++)
            {
                if (board.GetCellByRowAndCol(row, col) == 0)
                {
                    _domains[row, col] = BuildDomainForCell(board, row, col);
                }
                else
                {
                    _domains[row, col] = null;
                }
            }
        }
    }

    /// <summary>
    /// Updates neighboring domains after placing a value in a cell.
    /// Removes the placed value from domains in the same row, column, and sub-grid.
    /// Returns false if any neighbor domain becomes empty.
    /// </summary>
    /// <param name="board">Current board state.</param>
    /// <param name="row">Row of the placed value.</param>
    /// <param name="col">Column of the placed value.</param>
    /// <param name="domainChanges">Stack used to record domain changes for undo.</param>
    /// <returns>True if update succeeds; false if a domain becomes empty.</returns>
    public bool DomainUpdate(Board board, int row, int col, Stack<DomainChange> domainChanges)
    {
        int value = board.GetCellByRowAndCol(row, col);

        for (int i = 0; i < SudokuConstants.BoardSize; i++)
        {
            if (i != col && _domains[row, i] != null)
            {
                if (_domains[row, i].Remove(value))
                {
                    SaveDomainChange(domainChanges, row, i, value);
                    if (_domains[row, i].Count == 0)
                        return false;
                }
            }

            if (i != row && _domains[i, col] != null)
            {
                if (_domains[i, col].Remove(value))
                {
                    SaveDomainChange(domainChanges, i, col, value);
                    if (_domains[i, col].Count == 0)
                        return false;
                }
            }
        }

        int sub = SudokuConstants.SubGridSize;
        int startRow = (row / sub) * sub;
        int startCol = (col / sub) * sub;

        for (int r = 0; r < sub; r++)
        {
            for (int c = 0; c < sub; c++)
            {
                int currRow = startRow + r;
                int currCol = startCol + c;

                if ((currRow != row || currCol != col) && _domains[currRow, currCol] != null)
                {
                    if (_domains[currRow, currCol].Remove(value))
                    {
                        SaveDomainChange(domainChanges, currRow, currCol, value);
                        if (_domains[currRow, currCol].Count == 0)
                            return false;
                    }
                }
            }
        }

        return true;
    }

    /// <summary>
    /// Records a domain change to allow undoing domain updates.
    /// </summary>
    /// <param name="domainChanges">Stack used to record changes.</param>
    /// <param name="row">Row of the changed cell.</param>
    /// <param name="col">Column of the changed cell.</param>
    /// <param name="value">Value removed from the domain.</param>
    public void SaveDomainChange(Stack<DomainChange> domainChanges, int row, int col, int value)
    {
        domainChanges.Push(new DomainChange(row, col, value));
    }

    /// <summary>
    /// Undoes domain updates until the stack returns to a given snapshot size.
    /// </summary>
    /// <param name="domainChanges">Stack containing recorded changes.</param>
    /// <param name="snapshot">The stack size snapshot to restore to.</param>
    public void UndoDomainUpdate(Stack<DomainChange> domainChanges, int snapshot)
    {
        while (domainChanges.Count > snapshot)
        {
            DomainChange domainChange = domainChanges.Pop();
            _domains[domainChange.Row, domainChange.Col].Add(domainChange.Value);
        }
    }

    /// <summary>
    /// Returns the domain grid.
    /// </summary>
    /// <returns>A 2D array of domains.</returns>
    public List<int>[,] GetDomains()
    {
        return _domains;
    }

    /// <summary>
    /// Returns the domain list for a specific cell.
    /// </summary>
    /// <param name="row">Row index.</param>
    /// <param name="col">Column index.</param>
    /// <returns>The domain list for the cell.</returns>
    public List<int> GetDomain(int row, int col)
    {
        return _domains[row, col];
    }

    /// <summary>
    /// Clears the domain for a cell (used when the cell becomes assigned).
    /// </summary>
    /// <param name="row">Row index.</param>
    /// <param name="col">Column index.</param>
    public void ClearDomain(int row, int col)
    {
        _domains[row, col] = null;
    }

    /// <summary>
    /// Sets the domain for a cell.
    /// </summary>
    /// <param name="row">Row index.</param>
    /// <param name="col">Column index.</param>
    /// <param name="domain">Domain values for the cell.</param>
    public void SetDomain(int row, int col, List<int> domain)
    {
        _domains[row, col] = domain;
    }

    private List<int> BuildDomainForCell(Board board, int row, int col)
    {
        var domain = new List<int>();

        foreach (int value in SudokuConstants.AllValues)
        {
            if (value == 0) continue;

            if (IsAllowed(board, row, col, value))
            {
                domain.Add(value);
            }
        }

        return domain;
    }

    private bool IsAllowed(Board board, int row, int col, int value)
    {
        for (int i = 0; i < SudokuConstants.BoardSize; i++)
        {
            if (board.GetCellByRowAndCol(row, i) == value) return false;
            if (board.GetCellByRowAndCol(i, col) == value) return false;
        }

        int sub = SudokuConstants.SubGridSize;
        int subGridIndex = (row / sub) * sub + (col / sub);

        for (int cellIndex = 0; cellIndex < sub * sub; cellIndex++)
        {
            if (board.GetCellInSubGrid(subGridIndex, cellIndex) == value)
                return false;
        }

        return true;
    }
}