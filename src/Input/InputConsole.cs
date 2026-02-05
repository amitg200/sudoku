class InputConsole
{
    public string ReadInput()
    {
        String input;
        do
        {
            Console.WriteLine("Hello, please type in the sudoku:");
            input = Console.ReadLine();
        }while(string.IsNullOrEmpty(input));
        return input;
    }
}