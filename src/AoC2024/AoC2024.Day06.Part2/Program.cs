namespace AoC2024.Day06.Part2;

class Program
{
    static void Main()
    {
        var (map, guard) = ParseInput();

        List<ObstaclePosition> obstaclePositions = [];

        while (!IsLeavingMap(guard, map))
        {
            var (newGuard, obstaclePosition) = MoveGuard(guard, map);
            if(obstaclePosition != null && !obstaclePositions.Contains(obstaclePosition))
                obstaclePositions.Add(obstaclePosition);
            guard = newGuard;
        }
        
        Console.WriteLine($"There are {obstaclePositions.Count} obstacle positions that would cause a loop.");
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
    
    
    private static (GuardPosition NewPosition, ObstaclePosition? ObstaclePosition) MoveGuard(GuardPosition guard, char[][] map)
    {
        if (guard.IsBlocked(map))
            return (guard.TurnRight(map), null);

        var obstacle = guard.GetObstacleIfLooped(guard, map);
        return (guard.MoveForward(map), obstacle);
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

internal record ObstaclePosition(int X, int Y);

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

    public GuardPosition TurnRight(char[][] map)
    {
        char[] overWritePositions = ['.', 'X', '<', '>', '^', 'v'];
        switch (Direction)
        {
            case GuardDirection.Up:
                if (overWritePositions.Contains(map[Y][X]))
                    map[Y][X] = '^';
                break;
            case GuardDirection.Right:
                if (overWritePositions.Contains(map[Y][X]))
                    map[Y][X] = '>';
                break;
            case GuardDirection.Down:
                if (overWritePositions.Contains(map[Y][X]))
                    map[Y][X] = 'v';
                break;
            case GuardDirection.Left:
                if (overWritePositions.Contains(map[Y][X]))
                    map[Y][X] = '<';
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        var newDirection = (GuardDirection)(((int)Direction + 1) % 4);
        
        return this with { Direction = newDirection };
    }

    public GuardPosition MoveForward(char[][] map)
    {
        if (map[Y][X] == '.')
            map[Y][X] = 'X';
        
        return Direction switch
        {
            GuardDirection.Up => this with { Y = Y - 1 },
            GuardDirection.Right => this with { X = X + 1 },
            GuardDirection.Down => this with { Y = Y + 1 },
            GuardDirection.Left => this with { X = X - 1 },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public ObstaclePosition? GetObstacleIfLooped(GuardPosition guard, char[][] map)
    {
        char[] turnPositions = ['<', '>', '^', 'v'];
        
        switch (Direction)
        {
            case GuardDirection.Up:
                if (map[Y - 1][X] != '.')
                    return null;
                for (var x = X; x < map[0].Length; x++)
                {
                    if(map[Y][x] == '#')
                        if(turnPositions.Contains(map[Y][x - 1]))
                            return new ObstaclePosition(Y-1, X);
                        else
                            return null;
                }
                break;
            case GuardDirection.Right:
                if (map[Y][X + 1] != '.')
                    return null;
                for (var y = Y; y < map.Length; y++)
                {
                    if(map[y][X] == '#')
                        if(turnPositions.Contains(map[y-1][X]))
                            return new ObstaclePosition(Y, X+1);
                        else
                            return null;
                }
                break;
            case GuardDirection.Down:
                if (map[Y + 1][X] != '.')
                    return null;
                for (var x = X; x >= 0 ; x--)
                {
                    if(map[Y][x] == '#')
                        if(turnPositions.Contains(map[Y][x + 1]))
                            return new ObstaclePosition(Y+1, X);
                        else
                            return null;
                }
                break;
            case GuardDirection.Left:
                if (map[Y][X - 1] != '.')
                    return null;
                for (var y = Y; y >= 0; y--)
                {
                    if(map[y][X] == '#')
                        if(turnPositions.Contains(map[y+1][X]))
                            return new ObstaclePosition(Y, X-1);
                        else
                            return null;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return null;
    }
}

internal enum GuardDirection
{
    Up, 
    Right, 
    Down, 
    Left
}