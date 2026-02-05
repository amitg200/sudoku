static class SudokuConstants
{
    public const int BoardSize = 9;
    public const int SubGridSize = 3;   
    public const char EmptyCellOutputSymbol = 'x';
    public static readonly Dictionary<char, int> InputSymbolMap = 
    new Dictionary<char, int>
    {
        { '0', 0 },
        { '1', 1 },
        { '2', 2 },
        { '3', 3 },
        { '4', 4 },
        { '5', 5 },
        { '6', 6 },
        { '7', 7 },
        { '8', 8 },
        { '9', 9 }
    };
}