public class BoardFormatterTests
{
    private readonly BoardFormatter boardFormatter = new BoardFormatter();
    private readonly Parser _parser = new Parser();

    [Theory]
    [InlineData(
        "100000027000304015500170683430962001900007256006810000040600030012043500058001000")]
    [InlineData(
        "800000070006010053040600000000080400003000700020005038000000800004050061900002000")]
    public void Foramat_CorrectFormat(string input)
    {
        Board board = _parser.Parse(input);
        string output = boardFormatter.Format(board);
        Assert.Equal(input, output);
    }
}