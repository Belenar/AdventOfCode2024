namespace AoC2024.Day06.Part1;

class Program
{
    static void Main()
    {
        var (map, guard) = ParseInput();

        while (!IsLeavingMap(guard, map))
        {
            guard = MoveGuard(guard, map);
            map[guard.Y][guard.X] = 'X';
        }

        var numberOfVisitedPositions = map
            .SelectMany(row => row)
            .Count(position => position == 'X');
        
        Console.WriteLine($"The guard visited {numberOfVisitedPositions} positions.");
    }

    private static bool IsLeavingMap(GuardPosition guard, char[][] map)
    {
        return guard.Direction switch
        {
            GuardDirection.Up => guard.Y == 0,
            GuardDirection.Right => guard.X == map[0].Length - 1,
            GuardDirection.Down => guard.Y == map.Length - 1,
            GuardDirection.Left => guard.X == 0,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    
    private static GuardPosition MoveGuard(GuardPosition guard, char[][] map)
    {
        if (guard.IsBlocked(map))
            return guard.TurnRight();
        else
            return guard.MoveForward();
    }

    private static (char[][] Map, GuardPosition Guard) ParseInput()
    {
        var input = File.ReadAllLines("input.txt");

        var map = new char[input.Length][];
        GuardPosition guard = new(0, 0, GuardDirection.Up);

        for (var i = 0; i < input.Length; i++)
        {
            map[i] = input[i].ToCharArray();

            var guardPosition = input[i].IndexOf('^');
            if (guardPosition > 0)
            {
                guard = new GuardPosition(guardPosition, i, GuardDirection.Up);
                map[i][guardPosition] = 'X';
            }
        }
        
        return (map, guard);
    }
}

internal record GuardPosition(int X, int Y, GuardDirection Direction)
{
    public bool IsBlocked(char[][] map)
    {
        return Direction switch
        {
            GuardDirection.Up => map[Y - 1][X] == '#',
            GuardDirection.Right => map[Y][X + 1] == '#',
            GuardDirection.Down => map[Y + 1][X] == '#',
            GuardDirection.Left => map[Y][X - 1] == '#',
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public GuardPosition TurnRight()
    {
        var newDirection = (GuardDirection)(((int)Direction + 1) % 4);
        
        return this with { Direction = newDirection };
    }

    public GuardPosition MoveForward()
    {
        return Direction switch
        {
            GuardDirection.Up => this with { Y = Y - 1 },
            GuardDirection.Right => this with { X = X + 1 },
            GuardDirection.Down => this with { Y = Y + 1 },
            GuardDirection.Left => this with { X = X - 1 },
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

internal enum GuardDirection
{
    Up, 
    Right, 
    Down, 
    Left
}