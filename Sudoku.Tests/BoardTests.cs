namespace Sudoku.Tests;

public class BoardTests
{
    private readonly Board _board = new Board();

    [Theory]
    [InlineData(0, 0, 5)]
    [InlineData(8, 8, 9)]
    [InlineData(4, 3, 7)]
    public void SetCellByRowAndCol_SetsCorrectValue(int row, int col, int value)
    {
        _board.SetCellByRowAndCol(row, col, value);

        Assert.Equal(value, _board.GetCellByRowAndCol(row, col));
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]   
    [InlineData(4, 8, 5, 5)]   
    [InlineData(8, 0, 6, 6)]  
    public void SetCellInSubGrid_MapsToCorrectPosition(
        int subGridIndex,
        int cellIndex,
        int expectedRow,
        int expectedCol)
    {
        _board.SetCellInSubGrid(subGridIndex, cellIndex, 9);

        Assert.Equal(9, _board.GetCellByRowAndCol(expectedRow, expectedCol));
    }

    [Theory]
    [InlineData(0, 0, 0, 0)]
    [InlineData(5, 5, 4, 8)]
    [InlineData(6, 6, 8, 0)]
    public void GetCellInSubGrid_ReturnsCorrectValue(
        int row,
        int col,
        int subGridIndex,
        int cellIndex)
    {
        _board.SetCellByRowAndCol(row, col, 7);

        Assert.Equal(7, _board.GetCellInSubGrid(subGridIndex, cellIndex));
    }
}