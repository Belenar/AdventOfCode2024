namespace AoC2024.Day06.Part2;

class Program
{
    static void Main()
    {
        var (map, startPosition) = ParseInput();
        
        var guard = startPosition;
        
        List<GridPosition> possibleObstaclePositions = [];
        while (!guard.IsLeavingMap(map))
        {
            guard = guard.TakeStep(map);
            if (!possibleObstaclePositions.Contains(guard.Position))
            {
                possibleObstaclePositions.Add(guard.Position);
            }
        }

        possibleObstaclePositions.Remove(startPosition.Position);

        var numberOfValidObstaclePositions = 0;

        Parallel.ForEach(possibleObstaclePositions, obstaclePosition =>
        {
            if (startPosition.RunsInLoop(map, obstaclePosition))
            {
                numberOfValidObstaclePositions++;
            }
        });
        
        Console.WriteLine($"There are {numberOfValidObstaclePositions} obstacle positions that would cause a loop.");
    }

    private static (char[][] Map, GuardPosition Guard) ParseInput()
    {
        var input = File.ReadAllLines("input.txt");

        var map = new char[input.Length][];
        GuardPosition guard = new(new(0, 0), GuardDirection.Up);

        for (var i = 0; i < input.Length; i++)
        {
            map[i] = input[i].ToCharArray();

            var guardPosition = input[i].IndexOf('^');
            if (guardPosition > 0)
            {
                guard = new (new(guardPosition, i), GuardDirection.Up);
            }
        }
        
        return (map, guard);
    }
}

internal record GridPosition(int X, int Y);

internal record GuardPosition(GridPosition Position, GuardDirection Direction)
{
    private bool IsBlocked(char[][] map, GridPosition? obstaclePosition)
    {
        var forwardPosition = GetForwardPosition();
        
        if(obstaclePosition != null && forwardPosition == obstaclePosition)
            return true;
        
        return map[forwardPosition.Y][forwardPosition.X] == '#';
    }
    
    public bool IsLeavingMap(char[][] map)
    {
        return Direction switch
        {
            GuardDirection.Up => Position.Y == 0,
            GuardDirection.Right => Position.X == map[0].Length - 1,
            GuardDirection.Down => Position.Y == map.Length - 1,
            GuardDirection.Left => Position.X == 0,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private GuardPosition TurnRight()
    {
        var newDirection = (GuardDirection)(((int)Direction + 1) % 4);
        
        return this with { Direction = newDirection };
    }

    private GuardPosition MoveForward()
    {
        return this with { Position = GetForwardPosition() };
    }
    
    public GridPosition GetForwardPosition()
    {
        return Direction switch
        {
            GuardDirection.Up => Position with { Y = Position.Y - 1 },
            GuardDirection.Right => Position with { X = Position.X + 1 },
            GuardDirection.Down => Position with { Y = Position.Y + 1 },
            GuardDirection.Left => Position with { X = Position.X - 1 },
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public GuardPosition TakeStep(char[][] map, GridPosition? obstaclePosition = null)
    {
        if (IsBlocked(map, obstaclePosition))
            return TurnRight();

        return MoveForward();
    }

    public bool RunsInLoop(char[][] map, GridPosition obstaclePosition)
    {
        var currentGuard = this;
        
        List<GuardPosition> guardPath = [this];
        
        do
        {
            currentGuard = currentGuard.TakeStep(map, obstaclePosition);
            
            if (guardPath.Any(pos => currentGuard.Position.Equals(pos.Position) &&
                currentGuard.Direction == pos.Direction))
                return true;
            
            guardPath.Add(currentGuard);
        }
        while (!currentGuard.IsLeavingMap(map)) ;
        
        return false;
    }
}

internal enum GuardDirection
{
    Up, 
    Right, 
    Down, 
    Left
}