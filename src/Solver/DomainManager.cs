public class DomainManager
{
    private List<int>[,] _domains;
    public readonly struct DomainChange
    {
        readonly public int Row;
        readonly public int Col;
        readonly public int Value;
        public DomainChange(int row, int col, int value)
        {
            Row = row;
            Col = col;
            Value = value;
        }
    }
    public Stack<DomainChange> CreateChangeStack()
    {
        return new Stack<DomainChange>();
    }   
 
    public void InitializeDomains(Board board)
    {
        _domains = new List<int>[SudokuConstants.BoardSize,SudokuConstants.BoardSize];

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

    public bool DomainUpdate(Board board, int row,int col, Stack<DomainChange> domainChanges) //return false if there is a list that the value removed is the last one
    {
        int value = board.GetCellByRowAndCol(row, col);

        for (int i = 0; i < SudokuConstants.BoardSize; i++)
        {
            if (col != i && _domains[row, i] != null && _domains[row, i].Contains(value))
            {
                _domains[row, i].Remove(value);
                SaveDomainChange(domainChanges, row, i, value);             
                if(_domains[row, i].Count == 0)
                {
                    return false;
                }
            }
            if (row != i && _domains[i, col] != null && _domains[i, col].Contains(value))
            {
                _domains[i, col].Remove(value);
                SaveDomainChange(domainChanges, i,col , value);   
                if(_domains[i, col].Count == 0)
                {
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
                if (!(currRow == row && currCol == col) &&
                    _domains[currRow, currCol] != null &&
                    _domains[currRow, currCol].Contains(value))
                {
                    _domains[currRow, currCol].Remove(value);
                    SaveDomainChange(domainChanges, currRow, currCol, value);
                    if(_domains[currRow, currCol].Count == 0)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
    public void SaveDomainChange(Stack<DomainChange> domainChanges, int row, int col, int value)
    {
        var newChange = new DomainChange(row, col, value);
        domainChanges.Push(newChange);
    }
    public void UndoDomainUpdate(Stack<DomainChange> domainChanges)
    {
        while (domainChanges.Count != 0)
        {
            DomainChange domainChange = domainChanges.Pop();
            _domains[domainChange.Row,domainChange.Col].Add(domainChange.Value);
        }
    }

    public List<int>[,] GetDomains()
    {
        return _domains;
    }
    public List<int> GetDomain(int row, int col)
    {
        return _domains[row, col];
    }

    public void ClearDomain(int row, int col)
    {
        _domains[row, col] = null;
    }

    public void SetDomain(int row, int col, List<int> domain)
    {
        _domains[row, col] = domain;
    }
}