using System.Reflection.Metadata.Ecma335;
using advent_of_code_2025.Loader;

namespace advent_of_code_2025.problems;

public class DayFive
{
    const char Roll = '@';

    private record Ingredients(IReadOnlyCollection<long[]> FreshRanges, IReadOnlyCollection<long> IngredientIds);
    
    private Ingredients LoadInput()
    {
        var freshRanges = new List<long[]>();
        var ingredientIds = new List<long>();
        bool finishedRanges = false;
        
        foreach (var line in InputLoader.IterateEmbeddedResourceLines("day-five-sample.txt"))
        {
            if (line == "")
            {
                finishedRanges = true;
                continue;
            }

            if (!finishedRanges)
            {
                var newRange = line.Split("-").Select(long.Parse).ToArray();
                if (!freshRanges.Any(f => f[0] == newRange[0] && f[1] == newRange[1]))
                {
                    freshRanges.Add(newRange);
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
            return ingredients.FreshRanges.Any(f => i >= f[0] && i <= f[1]);
        }).Count();
        
        Console.WriteLine($"Fresh: {fresh}");
    }
    
    public void SolvePartTwo()
    {
        var ingredients = LoadInput();

        var sortedRanges = ingredients.FreshRanges.OrderBy(f => f[0]).ToArray();
        var totallyOverlappingRanges = new List<int>();
        
        long totalFreshPossible = sortedRanges.Select((range, index) =>
        {
            Console.WriteLine($"Range: {range[0]} {range[1]}");
            
            if (totallyOverlappingRanges.Contains(index))
            {
                Console.WriteLine("Overlaps previous");
                return 0;
            }
            
            var start = range[0];
            var end = range[1];

            int[] overlapping = [];
            
            if (index < sortedRanges.Count() - 1)
            {
                overlapping = sortedRanges
                    .Skip(index + 1)
                    .Where(r => r[1] <= end)
                    .Select((r, nextInd) => nextInd + index + 1)
                    .ToArray();
                
                totallyOverlappingRanges.AddRange(overlapping);
            }

            var startNextNotOverlapping = sortedRanges
                .Skip(index + 1 + overlapping.Length)
                .Select(r => r[0])
                .FirstOrDefault();
            
            Console.WriteLine($"Start next not overlap: {startNextNotOverlapping}");

            var actualEnd = startNextNotOverlapping == 0 || end < startNextNotOverlapping 
                ? end : startNextNotOverlapping - 1;
            
            Console.WriteLine($"Actual end: {actualEnd}");
            Console.WriteLine($"Add this: {actualEnd - start + 1}");

            return actualEnd - start + 1;
        }).Sum();
        
        Console.WriteLine($"Fresh possible: {totalFreshPossible}");
    }

    private IReadOnlyCollection<int[]> GetLiftableCoords(char[][] grid)
    {
        return grid
                .SelectMany((r, row) =>
                    r.Select((c, col) => new { row, col, c }))
                .Where(x => x.c == Roll && CanForklift(grid, x.row, x.col))
                .Select(x => new[] { x.row, x.col })
                .ToList();
    }

    private bool CanForklift(char[][] grid, int row, int col)
    {
        var above = row - 1;
        var below = row + 1;
        var left = col - 1;
        var right = col + 1;

        IEnumerable<int[]> checkTargets =
        [
            [above, left], [above, col], [above, right],
            [row, left],  [row, right],
            [below, left], [below, col], [below, right]
        ];

        var surroundingCount = 0;
                    
        foreach (var checkTarget in checkTargets)
        {
            var x = checkTarget[0];
            var y = checkTarget[1];

            if (x < 0 ||
                x >= grid.Length ||
                y < 0 ||
                y >= grid[x].Length)
            {
                continue;
            }

            surroundingCount += grid[x][y] == Roll ? 1 : 0;
        }

        return (surroundingCount < 4);
    }
}