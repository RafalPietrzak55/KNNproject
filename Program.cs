using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

class IrisSample
{
    public double[] Features;
    public int Label;
}

class Program
{
    static void Main()
    {
        var samples = File.ReadAllLines("iris.txt")
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Split('\t'))
            .Select(parts => new IrisSample
            {
                Features = parts.Take(4)
                                .Select(s => double.Parse(s, CultureInfo.InvariantCulture))
                                .ToArray(),
                Label = int.Parse(parts[4])
            }).ToList();

        Console.WriteLine($"Wczytano {samples.Count} próbek.");
    }
}
