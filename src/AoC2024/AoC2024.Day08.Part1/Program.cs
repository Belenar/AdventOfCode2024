namespace AoC2024.Day08.Part1;

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
    private bool IsOnMap()
    {
        return X is >= 0 and < 50 && Y is >= 0 and < 50;
    }

    public IEnumerable<Position> GetAntiNodes(Position otherPosition)
    {
        var xDiff = otherPosition.X - X;
        var yDiff = otherPosition.Y - Y;
        
        Position antiNode1 = new(otherPosition.X + xDiff, otherPosition.Y + yDiff);
        if(antiNode1.IsOnMap()) yield return antiNode1;
        
        Position antiNode2 = new(X - xDiff, Y - yDiff);
        if (antiNode2.IsOnMap()) yield return antiNode2;
    }
}