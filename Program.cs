using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

enum DistanceMetric
{
    Euclidean,
    Manhattan,
    Chebyshev
}

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
                Features = parts.Take(4).Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray(),
                Label = int.Parse(parts[4])
            }).ToList();

        Normalize(samples);

        Console.WriteLine("Wybierz metrykę (0 = Euklidesowa, 1 = Manhattan, 2 = Chebyshev): ");
        int choice = int.Parse(Console.ReadLine());
        DistanceMetric metric = (DistanceMetric)choice;

        int correct = 0;
        int k = 3;

        for (int i = 0; i < samples.Count; i++)
        {
            var test = samples[i];
            var train = samples.Where((s, idx) => idx != i).ToList();

            var predicted = Classify(test, train, k, metric);
            if (predicted == test.Label)
                correct++;
        }

        Console.WriteLine($"Dokładność: {100.0 * correct / samples.Count:F2}%");
    }

    static void Normalize(List<IrisSample> samples)
    {
        int featureCount = samples[0].Features.Length;
        for (int i = 0; i < featureCount; i++)
        {
            double min = samples.Min(s => s.Features[i]);
            double max = samples.Max(s => s.Features[i]);

            foreach (var s in samples)
            {
                s.Features[i] = (s.Features[i] - min) / (max - min);
            }
        }
    }

    static int Classify(IrisSample test, List<IrisSample> train, int k, DistanceMetric metric)
    {
        return train.First().Label;
    }
}
