using System;
using System.IO;
using System.Linq;

namespace Day2_part2
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\inputs\day2.txt"));
            var lines = File.ReadLines(path);

            var sumOfMultiplies = 0;

            foreach (var line in lines)
            {
                var game = line.Split(':');
                var gameId = int.Parse(string.Join("", game[0].Where(c => int.TryParse(c.ToString(), out _))));

                var results = game[1].Split(';');
                var resultsString = string.Join(",", results);
                var resultsCommaSplit = resultsString.Split(',');
                var resultsSanitized = resultsCommaSplit.Select(s => s.Trim()).ToArray();

                var minRed = 0;
                var minGreen = 0;
                var minBlue = 0;

                foreach (var result in resultsSanitized)
                {
                    var count = int.Parse(result.Split(' ')[0].Trim());

                    if (result.Contains("red") && minRed < count)
                        minRed = count;
                    
                    if (result.Contains("green") && minGreen < count)
                        minGreen = count;
                    
                    if (result.Contains("blue") && minBlue < count)
                        minBlue = count;
                }

                sumOfMultiplies += minRed * minGreen * minBlue;
            }
            Console.WriteLine(sumOfMultiplies);
        }
    }
}