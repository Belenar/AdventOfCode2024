namespace AoC2024.Day04.Part2;

class Program
{
    static void Main()
    {
        var input = ParseInput();
        var numberOfXmas = 0;

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == 'A')
                {
                    if(IsXmas(input, x, y))
                        numberOfXmas ++;
                }
            }
        }
        
        Console.WriteLine($"The number X-MAS is {numberOfXmas}.");
    }

    private static bool IsXmas(char[][] input, int x, int y)
    {
        // Check bounds
        if (x == 0 || y == 0 || x == input[0].Length - 1 || y == input.Length - 1) 
            return false;
        
        // Check diagonal 1
        if(!(input[y-1][x-1] == 'M' && input[y+1][x+1] == 'S') && !(input[y-1][x-1] == 'S' && input[y+1][x+1] == 'M'))
            return false;
        
        // Check diagonal 2
        if(!(input[y-1][x+1] == 'M' && input[y+1][x-1] == 'S') && !(input[y-1][x+1] == 'S' && input[y+1][x-1] == 'M'))
            return false;
            
        return true;
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