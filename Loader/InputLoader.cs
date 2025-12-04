using System.Reflection;
using System.Text;

namespace advent_of_code_2025.Loader;

public static class InputLoader
{
    public static void ReadEmbeddedResourceLines(string resourceName, Action<string> processLine)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        using var stream = assembly.GetManifestResourceStream("advent_of_code_2025.Inputs." + resourceName);
        
        if (stream == null)
            throw new FileNotFoundException($"Resource '{resourceName}' not found.");

        using var reader = new StreamReader(stream);
        
        while (reader.ReadLine() is { } line)
        {
            processLine(line);
        }
    }
    public static string LoadEntireFile(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        using var stream = assembly.GetManifestResourceStream("advent_of_code_2025.Inputs." + resourceName);
        
        if (stream == null)
            throw new FileNotFoundException($"Resource '{resourceName}' not found.");

        using var reader = new StreamReader(stream);
        var sb = new StringBuilder();
        while (reader.ReadLine() is { } line)
        {
            sb.Append(line);
        }

        return sb.ToString();
    }
    
    public static char[][] LoadFileTo2DCharArray(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        using var stream = assembly.GetManifestResourceStream("advent_of_code_2025.inputs." + resourceName);
        
        if (stream == null)
            throw new FileNotFoundException($"Resource '{resourceName}' not found.");

        using var reader = new StreamReader(stream);
        var lines = new List<char[]>();
        while (reader.ReadLine() is { } line)
        {
            lines.Add(line.ToCharArray());
        }

        return lines.ToArray();
    }
    
    public static IEnumerable<string> IterateEmbeddedResourceLines(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        using var stream = assembly.GetManifestResourceStream("advent_of_code_2025.inputs." + resourceName);
        
        if (stream == null)
            throw new FileNotFoundException($"Resource '{resourceName}' not found.");

        using var reader = new StreamReader(stream);
        
        while (reader.ReadLine() is { } line)
        {
            yield return line;
        }
    }
    
    public static string PrintGrid(char[][] grid)
    {
        var sb = new StringBuilder();
        int rowInd = 0;
        
        foreach (var row in grid)
        {
            sb.AppendLine(new string(row));
            rowInd++;
        }
    
        return sb.ToString();
    }
}