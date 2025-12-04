using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using advent_of_code_2025.Loader;

namespace advent_of_code_2025.problems;

public class DayFour
{
    const char Roll = '@';
    
    private char[][] GetGrid()
    {
        return InputLoader.LoadFileTo2DCharArray("day-four.txt");
    }
    
    public void SolvePartOne()
    {
        var grid = GetGrid();
        
        Console.WriteLine($"Liftable rolls: {GetLiftableCoords(grid).Count()}");
    }
    
    public void SolvePartTwo()
    {
        var grid = GetGrid();
        long totalLiftable = 0;
        int liftable = 0;

        do
        {
            var liftableCoords = GetLiftableCoords(grid);

            liftable = liftableCoords.Count;

            foreach (var moved in liftableCoords)
            {
                grid[moved[0]][moved[1]] = 'x';
            }

            totalLiftable += liftable;

        } while (liftable > 0);
        
        Console.WriteLine($"Liftable rolls: {totalLiftable}");
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