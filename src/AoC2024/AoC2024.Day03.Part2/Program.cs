using System.Text.RegularExpressions;

namespace AoC2024.Day03.Part2;

class Program
{
    static void Main()
    {
        var input = ParseInput();
        var totalResult = 0;

        var instructionRegex = new Regex(@"(?'mul'mul\((?'first'(\d){1,3})\,(?'second'(\d){1,3})\))|(?'enable'do\(\))|(?'disable'don\'t\(\))");
        var enabled = true;
        
        var matches = instructionRegex.Matches(input);

        for (int i = 0; i < matches.Count; i++)
        {
            var match = matches[i];
            if (match.Groups["enable"].Success)
            {
                enabled = true;
            } else if (match.Groups["disable"].Success)
            {
                enabled = false;
            } else if (enabled)
            {
                var first = int.Parse(match.Groups["first"].Value);
                var second = int.Parse(match.Groups["second"].Value);
                var result = first * second;
                totalResult += result;
            }
        }
        
        Console.WriteLine($"The sum of all multiplications is {totalResult}.");
    }

    private static string ParseInput()
    {
        var input = File.ReadAllText("input.txt");
        
        return input;
    }
}