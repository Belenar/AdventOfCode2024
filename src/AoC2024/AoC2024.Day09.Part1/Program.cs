namespace AoC2024.Day09.Part1;

class Program
{
    public static void Main()
    {
        var fileMap = ParseInput();

        var calculator = new ChecksumCalculator(fileMap);
        var checksum = calculator.CalculateChecksum();

        Console.WriteLine($"The total checkSum is {checksum}.");
    }
    
    private static int[] ParseInput()
    {
        var input = File.ReadAllLines("input.txt");
        return input[0]
            .ToCharArray()
            .Select(c => int.Parse(c.ToString()))
            .ToArray();
    }
}

class ChecksumCalculator(int[] fileMap)
{
    private int _diskPosition;
    private int _frontPointer;
    private int _backPointer;
    private int _backFileId;
    private long _totalChecksum;
    
    public long CalculateChecksum()
    {
        _backPointer = fileMap.Length - 1;
        _backFileId = _backPointer / 2;

        while (_frontPointer <= _backPointer)
        {
            HandleFileInPlace();
            _frontPointer++;
            HandleGap();
            _frontPointer++;
        }

        return _totalChecksum;
    }

    private void HandleGap()
    {
        for (var i = 0; i < fileMap[_frontPointer]; i++)
        {
            AddBlockFromTheBack();
            _diskPosition++;
        }
    }

    private void AddBlockFromTheBack()
    {
        if (fileMap[_backPointer] == 0)
        {
            _backPointer -= 2;
            _backFileId = _backPointer / 2;
        }

        if (_backPointer < _frontPointer)
            return;

        _totalChecksum += _backFileId * _diskPosition;
        fileMap[_backPointer]--;
    }

    private void HandleFileInPlace()
    {
        var fileId = _frontPointer / 2;
        for (int i = 0; i < fileMap[_frontPointer]; i++)
        {
            _totalChecksum += fileId * _diskPosition;
            _diskPosition++;
        }
    }
}