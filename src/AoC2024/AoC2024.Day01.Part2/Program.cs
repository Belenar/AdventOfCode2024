namespace AoC2024.Day01.Part2;

class Program
{
    static void Main()
    {
        var input = ParseInput();

        var similarityScore = 0;

        foreach (var left in input.Left)
        {
            var rightCount = input.Right.Count(c => c == left);
            similarityScore += (left * rightCount);
        }
        
        Console.WriteLine($"The total similarity score is {similarityScore}");
    }

    private static CoordinateLists ParseInput()
    {
        var result = new CoordinateLists();
        
        var lines = File.ReadAllLines("input.txt");
        foreach (var line in lines)
        {
            var values = line.Split("   ");
            result.Left.Add(int.Parse(values[0]));
            result.Right.Add(int.Parse(values[1]));
        }
        
        return result;
    }
}

class CoordinateLists
{
    public List<int> Left { get; } = new();
    public List<int> Right { get; } = new();
}