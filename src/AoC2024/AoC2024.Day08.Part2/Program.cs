namespace AoC2024.Day08.Part2;

class Program
{
    static void Main()
    {
        var antennaMap = ParseInput();

        List<Position> antiNodePositions = [];

        foreach (var frequency in antennaMap.Keys)
        {
            antiNodePositions.AddRange(GetAntiNodes(antennaMap[frequency]));
        }

        Console.WriteLine($"The total number of antinode positions {antiNodePositions.Distinct().Count()}.");
    }

    private static IEnumerable<Position> GetAntiNodes(List<Position> antennae)
    {
        var firstAntenna = antennae[0];
        
        var otherAntennae = antennae.Skip(1).ToList();

        foreach (var otherAntenna in otherAntennae)
        {
            foreach (var antiNode in firstAntenna.GetAntiNodes(otherAntenna))
            {
                yield return antiNode;
            }
        }

        if(otherAntennae.Count < 2)
            yield break;
        
        foreach (var antiNode in GetAntiNodes(otherAntennae))
        {
            yield return antiNode;
        }
    }

    private static Dictionary<char, List<Position>> ParseInput()
    {
        var input = File.ReadAllLines("input.txt");
        var positions = new Dictionary<char, List<Position>>();
        
        for (var y = 0; y < input.Length; y++)
        {
            var line = input[y].ToCharArray();
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '.') continue;
                
                var frequency = line[x];
                
                if(!positions.ContainsKey(frequency))
                    positions[frequency] = [new(x, y)];
                else
                    positions[frequency].Add(new(x, y));
            }
        }
        return positions;
    }
}

record Position(int X, int Y)
{
    public IEnumerable<Position> GetAntiNodes(Position otherPosition)
    {
        var xDiff = otherPosition.X - X;
        var yDiff = otherPosition.Y - Y;

        for (var x = 0; x < 50; x++)
        {
            if(x == X)
                yield return new Position(X, Y);
            else if (x == otherPosition.X)
                yield return new Position(otherPosition.X, otherPosition.Y);
            else if (yDiff * (X - x) % xDiff  == 0)
            {
                var y = Y - yDiff * (X - x) / xDiff;

                if (y is >= 0 and < 50)
                    yield return new Position(x, y);
            }
        }
    }
}