namespace AoC2024.Day04.Part1;

class Program
{
    static void Main()
    {
        var input = ParseInput();
        var numberOfWords = 0;

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == 'X')
                {
                    var numberOfXmas = FindWords(input, x, y);
                    numberOfWords += numberOfXmas;
                }
            }
        }
        
        Console.WriteLine($"The number of times XMAS appears is {numberOfWords}.");
    }

    private static int FindWords(char[][] input, int x, int y)
    {
        var words = 0;
        // Left
        if(x > 2 && input[y][x-1] == 'M' && input[y][x-2] == 'A' && input[y][x-3] == 'S')
            words++;
        // Top left
        if(x > 2 && y > 2 && input[y-1][x-1] == 'M' && input[y-2][x-2] == 'A' && input[y-3][x-3] == 'S')
            words++;
        // Top
        if(y > 2 && input[y-1][x] == 'M' && input[y-2][x] == 'A' && input[y-3][x] == 'S')
            words++;
        // Top right
        if(x < input[0].Length - 3 && y > 2 && input[y-1][x+1] == 'M' && input[y-2][x+2] == 'A' && input[y-3][x+3] == 'S')
            words++;
        // Right
        if(x < input[0].Length - 3 && input[y][x+1] == 'M' && input[y][x+2] == 'A' && input[y][x+3] == 'S')
            words++;
        // Bottom right
        if(x < input[0].Length - 3 && y < input.Length - 3 && input[y+1][x+1] == 'M' && input[y+2][x+2] == 'A' && input[y+3][x+3] == 'S')
            words++;
        // Bottom
        if(y < input.Length - 3 && input[y+1][x] == 'M' && input[y+2][x] == 'A' && input[y+3][x] == 'S')
            words++;
        // Bottom left
        if(x > 2 && y < input.Length - 3 && input[y+1][x-1] == 'M' && input[y+2][x-2] == 'A' && input[y+3][x-3] == 'S')
            words++;
            
        return words;
    }

    private static char[][] ParseInput()
    {
        var input = File.ReadAllLines("input.txt");

        var result = new char[input.Length][];

        for (int i = 0; i < input.Length; i++)
        {
            result[i] = input[i].ToCharArray();
        }
        
        return result;
    }
}