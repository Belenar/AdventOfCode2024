using System.Text.RegularExpressions;

namespace AoC2024.Day03.Part1;

class Program
{
    static void Main()
    {
        var input = ParseInput();
        var totalResult = 0;

        var validInstructionRegex = new Regex(@"mul\((?'first'(\d){1,3})\,(?'second'(\d){1,3})\)");
        
        var matches = validInstructionRegex.Matches(input);

        for (int i = 0; i < matches.Count; i++)
        {
            var match = matches[i];
            var first = int.Parse(match.Groups["first"].Value);
            var second = int.Parse(match.Groups["second"].Value);
            var result = first * second;
            totalResult += result;
        }
        
        Console.WriteLine($"The sum of all multiplications is {totalResult}.");
    }

    private static string ParseInput()
    {
        var input = File.ReadAllText("input.txt");
        
        return input;
    }
}