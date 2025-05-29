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
                Features = parts.Take(4).Select(s => double.Parse(s)).ToArray(),
                Label = int.Parse(parts[4])
            }).ToList();



        int correct = 0;
        int k = 3;

        for (int i = 0; i < samples.Count; i++)
        {
            var test = samples[i];
            var train = samples;

            var predicted = Classify(test, train, k);
            if (predicted == test.Label)
                correct++;
        }

        Console.WriteLine($"Dokładność: {100.0 * correct / samples.Count:F2}%");
    }

    static void Normalize(List<IrisSample> samples) { }

    static int Classify(IrisSample test, List<IrisSample> train, int k)
    {
        return train.First().Label;
    }


}
