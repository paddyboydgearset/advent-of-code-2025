using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using advent_of_code_2025.Loader;

namespace advent_of_code_2025.dayone;

public class DayThree
{
    private IEnumerable<long[]> GetLines()
    {
        foreach (var line in InputLoader.IterateEmbeddedResourceLines(@"day-three.txt"))
        {
            yield return line.Select(c => Int64.Parse(c.ToString())).ToArray();
        }
    }
    
    public void SolvePartOne()
    {
        long maxAmount = GetLines().Sum(l => FindMax(l, 2));
        
        Console.WriteLine($"Max joltage: {maxAmount}");
    }
    
    public void SolvePartTwo()
    {
        long maxAmount = GetLines().Sum(l => FindMax(l, 12));
        
        Console.WriteLine($"Max joltage: {maxAmount}");
    }
  
    private long FindMax(long[] digits, int finalDigitCount)
    {
        List<long> excludeFromMax = [];

        if (finalDigitCount == 1) return digits.Max();

        while (excludeFromMax.Count < 9)
        {
            var maxDigit = digits.Except(excludeFromMax).Max();
            var firstIndex = Array.IndexOf(digits, maxDigit);
            if (firstIndex <= digits.Length - (finalDigitCount))
            {
                var maxNext = FindMax(digits.Skip(firstIndex + 1).ToArray(), finalDigitCount - 1);
                return Int64.Parse($"{maxDigit}{maxNext}");
            }

            excludeFromMax.Add(maxDigit);
        }

        return 0;
    }
}