class InputValidation
{
    public void Validate(string input)
    {
        int expectedLength = SudokuConstants.BoardSize * SudokuConstants.BoardSize;

        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input is null or empty.");
        }

        if (input.Length != expectedLength)
        {
            throw new ArgumentException($"Invalid length, expected:{expectedLength} and got {input.Length}.");
        }

        foreach (char c in input)
        {
            if(!SudokuConstants.InputSymbolMap.ContainsKey(c))
            {
                throw new ArgumentException($"Invalid char '{c}'.");
            }
        }
    }
}