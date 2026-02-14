namespace Sudoku.Tests;

public class InputValidationTests
{
    private readonly InputValidation _validator = new InputValidation();
    [Theory]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("80000007000601005304060000000008040000300070002000503800000080000405006190000200a")]
    [InlineData("10000^027#00304015500170683430962001900007256006810000040600030012043500058001000")]
    [InlineData("\n 12 2")]
    public void Validate_InvalidInput_ThrowsException(string input)
    {
        Assert.Throws<ArgumentException>(() => _validator.Validate(input));
    }

    [Theory]
    [InlineData("100000027000304015500170683430962001900007256006810000040600030012043500058001000")]
    [InlineData("800000070006010053040600000000080400003000700020005038000000800004050061900002000")]
    public void Validate_ValidInput_DoesNotThrow(string input)
    {
        var exception = Record.Exception(() => _validator.Validate(input));

        Assert.Null(exception);
    }
}