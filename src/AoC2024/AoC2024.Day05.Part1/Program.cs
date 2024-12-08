namespace AoC2024.Day05.Part1;

class Program
{
    static void Main()
    {
        var (rules, updates) = ParseInput();

        var sumOfAllMiddlePages = 0;

        foreach (var update in updates)
        {
            if (IsCorrectlyOrdered(update, rules))
            {
                var middlePage = GetMiddlePage(update.PagesToPrint);
                sumOfAllMiddlePages += middlePage;
            }
        }
        
        Console.WriteLine($"The sum of all correctly ordered middle pages is {sumOfAllMiddlePages}.");
    }

    private static int GetMiddlePage(List<int> pages)
    {
        return pages[(pages.Count - 1)/2];
    }

    private static bool IsCorrectlyOrdered(Update update, List<OrderingRule> rules)
    {
        for (var i = 0; i < update.PagesToPrint.Count - 1; i++)
        {
            var currentPage = update.PagesToPrint[i];
            for (var j = i+1; j < update.PagesToPrint.Count; j++)
            {
                var nextPage = update.PagesToPrint[j];
                if (rules.Any(rule => rule.SecondPage == currentPage && rule.FirstPage == nextPage))
                    return false;
            }
        }

        return true;
    }

    private static (List<OrderingRule> Rules, List<Update> Updates) ParseInput()
    {
        var input = File.ReadAllLines("input.txt");

        var rules = new List<OrderingRule>();

        var index = 0;
        while (!string.IsNullOrWhiteSpace(input[index]))
        {
            var splitInput = input[index].Split('|');
            rules.Add(new OrderingRule(int.Parse(splitInput[0]), int.Parse(splitInput[1])));
            index++;
        }
        
        var updates = new List<Update>();
        for (int i = index + 1; i < input.Length; i++)
        {
            var pages = input[i].Split(',')
                .Select(int.Parse)
                .ToList();
            updates.Add(new Update(pages));
        }
        
        return (rules, updates);
    }
}

record OrderingRule(int FirstPage, int SecondPage);

record Update(List<int> PagesToPrint);