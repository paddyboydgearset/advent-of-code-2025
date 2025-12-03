using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using advent_of_code_2025.Loader;

namespace advent_of_code_2025.problems;

public class DayTwo
{
    private IReadOnlyCollection<Tuple<long, long>> GetIdRanges()
    {
        var ranges = InputLoader.IterateEmbeddedResourceLines(@"day-two.txt")
            .SelectMany(l => l.Split(','));

        return ranges.Select(r =>
        {
            var limits = r.Split("-").Select(Int64.Parse).ToArray();
            return new Tuple<long, long>(limits[0], limits[1]);

        }).ToArray();
    }
    public void SolvePartOne()
    {
        var regex = new Regex(@"(?!^\d$)^(\d+)\1$");
        
        SumMatches(regex);
    }
    
    public void SolvePartTwo()
    {
        var regex = new Regex(@"(?!^\d$)^(\d+)\1+$");
        
        SumMatches(regex);
    }

    private void SumMatches(Regex regex)
    {
        long invalidSum = 0;
        
        foreach (var idRange in GetIdRanges())
        {
            for (var id = idRange.Item1; id <= idRange.Item2; id++)
            {
                if (regex.IsMatch(id.ToString()))
                {
                    Console.WriteLine(id);
                    invalidSum += id;
                }
            }
        }

        Console.WriteLine($"Sum invalids: {invalidSum}");
    }
}