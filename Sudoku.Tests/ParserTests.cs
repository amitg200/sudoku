namespace Sudoku.Tests;

public class ParserTests
{
    private readonly Parser _parser = new Parser();
    [Theory]
    [InlineData(
        "100000027000304015500170683430962001900007256006810000040600030012043500058001000")]
    [InlineData(
        "800000070006010053040600000000080400003000700020005038000000800004050061900002000")]
    public void Parse_ValidInput_MapsValuesCorrectly(string input)
    {
        Board board = _parser.Parse(input);

        for (int i = 0; i < SudokuConstants.BoardSize * SudokuConstants.BoardSize; i++)
        {
            int row = i / SudokuConstants.BoardSize;
            int col = i % SudokuConstants.BoardSize;

            int expectedValue = SudokuConstants.InputSymbolMap[input[i]];
            int actualValue = board.GetCellByRowAndCol(row, col);

            Assert.Equal(expectedValue, actualValue);
        }
    }
}