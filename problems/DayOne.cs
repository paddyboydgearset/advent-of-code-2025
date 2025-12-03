using advent_of_code_2025.Loader;

namespace advent_of_code_2025.problems;

public class DayOne
{
    public void SolvePartOne()
    {
        var position = 50;
        var counter = 0;
        
        foreach (var line in InputLoader.IterateEmbeddedResourceLines(@"day-one.txt"))
        {
            var direction = line[0] == 'R' ? 1 : -1;
            var amount = int.Parse(line[1..]);

            var newPos = (position + (amount * direction)) % 100;
            position = newPos < 0 ? 100 + newPos : newPos;
            Console.WriteLine(position);
            if (position == 0) counter++;
        }
        
        Console.WriteLine($"Password: {counter}");
    }
    
    public void SolvePartTwo()
    {
        var position = 50;
        var counter = 0;
        
        foreach (var line in InputLoader.IterateEmbeddedResourceLines(@"day-one.txt"))
        {
            var direction = line[0] == 'R' ? 1 : -1;
            var amount = int.Parse(line[1..]);

            var originalPos = position;
            var movement = amount * direction;
            var unadjustedPosition = position + (movement  % 100);
            var adjustedPosition = unadjustedPosition % 100;
            var crossed = originalPos != 0 && adjustedPosition != 0 && unadjustedPosition is > 100 or < 0;
            
            var extraRotations = ((amount) / 100);

            position = adjustedPosition < 0 ? 100 + adjustedPosition : adjustedPosition;
            
            if (position == 0) counter++;
            
            if (crossed) counter++;
            
            if (extraRotations > 0)
            {
                counter += extraRotations;
            }
        }
        
        Console.WriteLine($"Password: {counter}");
    }
}