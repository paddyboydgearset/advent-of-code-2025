using System.Reflection.Metadata.Ecma335;
using advent_of_code_2025.Loader;

namespace advent_of_code_2025.problems;

public class DayFive
{

    private record LongRange(long Start, long End)
    {
        public long Length = End - Start + 1;

        public bool Overlaps(LongRange range)
        {
            return (range.Start >= Start && range.Start <= End)
                || (range.End >= Start && range.End <= End)
                || range.Start < Start && range.End > End;
        }

        public LongRange Combined(IReadOnlyCollection<LongRange> ranges)
        {
            return new LongRange(ranges.Select(r => r.Start).Concat([Start]).Min(), 
                ranges.Select(r => r.End).Concat([End]).Max());
        }
    };
    
    private record Ingredients(IReadOnlyCollection<LongRange> FreshRanges, IReadOnlyCollection<long> IngredientIds);
    
    private Ingredients LoadInput()
    {
        var freshRanges = new List<LongRange>();
        var ingredientIds = new List<long>();
        bool finishedRanges = false;
        
        foreach (var line in InputLoader.IterateEmbeddedResourceLines("day-five.txt"))
        {
            if (line == "")
            {
                finishedRanges = true;
                continue;
            }

            if (!finishedRanges)
            {
                var lineRange = line.Split("-").Select(long.Parse).ToArray();
                var range = new LongRange(lineRange[0], lineRange[1]);

                var overlaps = freshRanges.Where(f => f.Overlaps(range)).ToArray();

                if (overlaps.Any())
                {
                    freshRanges = freshRanges.Except(overlaps).ToList();

                    freshRanges.Add(range.Combined(overlaps));
                }
                else
                {
                    freshRanges.Add(range);
                }
            }
            else
            {
                ingredientIds.Add(long.Parse(line));
            }
        }

        return new Ingredients(freshRanges, ingredientIds);
    }
    
    public void SolvePartOne()
    {
        var ingredients = LoadInput();

        var fresh = ingredients.IngredientIds.Where(i =>
        {
            return ingredients.FreshRanges.Any(f => i >= f.Start && i <= f.End);
        }).Count();
        
        Console.WriteLine($"Fresh: {fresh}");
    }
    
    public void SolvePartTwo()
    {
        var ingredients = LoadInput();
        
        var totalFreshPossible = ingredients.FreshRanges.Select(f => f.Length).Sum();
        
        Console.WriteLine($"Fresh possible: {totalFreshPossible}");
    }
}