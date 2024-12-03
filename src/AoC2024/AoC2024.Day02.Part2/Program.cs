namespace AoC2024.Day02.Part2;

class Program
{
    static void Main()
    {
        var input = ParseInput();

        var safeReports = 0;
        foreach (var report in input)
        {
            if(IsSafe(report.Levels))
                safeReports++;
            else
            {
                for (int i = 0; i < report.Levels.Length; i++)
                {
                    var newLevels = report.Levels
                        .Where((_, j) => j != i)
                        .ToArray();
                    if (IsSafe(newLevels))
                    {
                        safeReports++;
                        break;
                    }
                }
            }
        }
        
        Console.WriteLine($"The number of safe reports is {safeReports}.");
    }

    private static bool IsSafe(int[] levels)
    {
        var increasing = levels[1] > levels[0];

        for (int i = 1; i < levels.Length; i++)
        {
            var diff = levels[i] - levels[i - 1];
            if (Math.Abs(diff) > 3 || Math.Abs(diff) < 1)
                return false;
            if ((diff > 0) != increasing)
                return false;
        }
        return true;
    }

    private static List<Report> ParseInput()
    {
        List<Report> result = new();
        
        var lines = File.ReadAllLines("input.txt");
        foreach (var line in lines)
        {
            var levels = line
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
            result.Add(new Report { Levels = levels});
        }
        
        return result;
    }
}

class Report
{
    public required int[] Levels { get; init; }
}