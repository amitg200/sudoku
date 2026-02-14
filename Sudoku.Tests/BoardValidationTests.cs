public class BoardValidationTests
{
    private readonly BoardValidation _validator = new BoardValidation();

    [Fact]
    public void Validate_ValidBoard_DoesNotThrow()
    {
        var board = new Board();

        board.SetCellByRowAndCol(0, 0, 1);
        board.SetCellByRowAndCol(0, 1, 2);
        board.SetCellByRowAndCol(1, 0, 3);

        var exception = Record.Exception(() => _validator.Validate(board));

        Assert.Null(exception);
    }

    [Fact]
    public void Validate_RowDuplicate_ThrowsException()
    {
        var board = new Board();

        board.SetCellByRowAndCol(0, 0, 5);
        board.SetCellByRowAndCol(0, 3, 5);

        Assert.Throws<ArgumentException>(() => _validator.Validate(board));
    }

    [Fact]
    public void Validate_SubGridDuplicate_ThrowsException()
    {
        var board = new Board();

        board.SetCellByRowAndCol(0, 0, 9);
        board.SetCellByRowAndCol(1, 1, 9);

        Assert.Throws<ArgumentException>(() => _validator.Validate(board));
    }

    [Fact]
    public void IsBoardValid_InvalidBoard_ReturnsFalse()
    {
        var board = new Board();

        board.SetCellByRowAndCol(0, 0, 4);
        board.SetCellByRowAndCol(0, 5, 4);

        Assert.False(_validator.IsBoardValid(board));
    }
    
    [Fact]
    public void IsCellPlacementValid_ValidPlacement_ReturnsTrue()
    {
        var board = new Board();

        board.SetCellByRowAndCol(0, 0, 3);

        Assert.True(_validator.IsCellPlacementValid(board, 0, 0));
    }

    [Fact]
    public void IsCellPlacementValid_InvalidPlacement_ReturnsFalse()
    {
        var board = new Board();

        board.SetCellByRowAndCol(0, 0, 8);
        board.SetCellByRowAndCol(0, 3, 8);

        Assert.False(_validator.IsCellPlacementValid(board, 0, 3));
    }
}