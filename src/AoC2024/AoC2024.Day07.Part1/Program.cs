namespace AoC2024.Day07.Part1;

class Program
{
    static void Main()
    {
        var equations = ParseInput();

        long calibrationResult = 0;

        foreach (var equation in equations)
        {
            if (equation.CanBeTrue())
            {
                calibrationResult += equation.Result;
            }
        }

        Console.WriteLine($"The total calibration result is {calibrationResult}.");
    }
    
    private static IEnumerable<Equation> ParseInput()
    {
        var input = File.ReadAllLines("input.txt");
        
        for (var i = 0; i < input.Length; i++)
        {
            var line = input[i].Split(": ");
            var result = long.Parse(line[0]);
            var operands = line[1].Split(' ').Select(long.Parse).ToArray();
            yield return new (result, operands);
        }
    }
}

record Equation(long Result, long[] Operands)
{
    public bool CanBeTrue()
    {
        var currentResult = Operands[0];
        
        var remainingOperands = Operands.Skip(1).ToArray();

        return HasValidCombination(currentResult, remainingOperands);
    }

    private bool HasValidCombination(long currentResult, long[] operands)
    {
        if (currentResult > Result) return false;
        
        var nextOperand = operands[0];
        var remainingOperands = operands.Length > 1 
            ? operands.Skip(1).ToArray() 
            : [];

        foreach (var nextResult in ApplyOperations(currentResult, nextOperand))
        {
            if (remainingOperands.Length == 0)
            {
                if (nextResult == Result) return true;
            }
            else
            {
                if (HasValidCombination(nextResult, remainingOperands)) return true;
            }
        }
        return false;
    }

    private IEnumerable<long> ApplyOperations(long currentResult, long nextOperand)
    {
        yield return currentResult + nextOperand;
        yield return currentResult * nextOperand;
    }
}