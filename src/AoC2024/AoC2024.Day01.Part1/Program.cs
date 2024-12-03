namespace AoC2024.Day01.Part1;

class Program
{
    static void Main()
    {
        var input = ParseInput();
        
        var leftSorted = input.Left.OrderBy(x => x).ToArray();
        var rightSorted = input.Right.OrderBy(x => x).ToArray();

        var totalDistance = 0;

        for (int i = 0; i < leftSorted.Length; i++)
        {
            var distance = Math.Abs(leftSorted[i] - rightSorted[i]);
            totalDistance += distance;
        }
        
        Console.WriteLine($"The total distance is {totalDistance}");
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