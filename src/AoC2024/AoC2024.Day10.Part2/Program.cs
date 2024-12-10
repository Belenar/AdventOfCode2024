using System.Collections;

namespace AoC2024.Day10.Part2;

class Program
{
    public static void Main()
    {
        var topographicMap = ParseInput();

        var trailheads = FindTrailHeads(topographicMap);

        var totalTrailCount = 0;
        
        foreach (var trailhead in trailheads)
        {
            var trailCount = CalculateTrailCount(trailhead, topographicMap);
            totalTrailCount += trailCount;
        }
        
        Console.WriteLine($"The total trailhead score is {totalTrailCount}.");
    }

    private static int CalculateTrailCount(Position trailhead, int[][] topographicMap)
    {
        List<Position> currentPositions = [trailhead];
        
        for (int i = 1; i < 10; i++)
        {
            var possiblePositions = currentPositions
                .SelectMany(p => p.GetAdjacentPositions())
                .ToList();

            List<Position> newPositions = [];

            foreach (var possiblePosition in possiblePositions)
            {
                if(topographicMap[possiblePosition.Y][possiblePosition.X] == i)
                    newPositions.Add(possiblePosition);
            }
            
            currentPositions = newPositions;
        }
        
        return currentPositions.Count;
    }

    private static IEnumerable<Position> FindTrailHeads(int[][] topographicMap)
    {
        for (int y = 0; y < topographicMap.Length; y++)
        {
            for (int x = 0; x < topographicMap[y].Length; x++)
            {
                if(topographicMap[y][x] == 0)
                    yield return new Position(x, y);
            }
        }
    }

    record Position(int X, int Y)
    {
        public IEnumerable<Position> GetAdjacentPositions()
        {
            if (X > 0) yield return this with { X = X - 1 };
            if (Y > 0) yield return this with { Y = Y - 1 };
            if (X < 41) yield return this with { X = X + 1 };
            if (Y < 41) yield return this with { Y = Y + 1 };
        }
    };
    
    private static int[][] ParseInput()
    {
        var input = File.ReadAllLines("input.txt");
        return input
            .Select(line => line
                .ToCharArray()
                .Select(c => int.Parse(c.ToString()))
                .ToArray())
            .ToArray();
    }
}