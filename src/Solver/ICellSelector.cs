interface ICellSelector
{
    bool TrySelectCell(Board board, List<int>[,] domains, out int row, out int col);
}