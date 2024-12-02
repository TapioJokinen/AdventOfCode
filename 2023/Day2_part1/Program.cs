using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable LoopCanBeConvertedToQuery

namespace Day2_part1
{
    internal static class Program
    {
        public static bool IsPossible(string[] results)
        {
            var colors = new Dictionary<string, int>
            {
                { "red", 12 },
                { "green", 13 },
                { "blue", 14 }
            };

            foreach (var keyValuePair in colors)
            {
                foreach (var result in results)
                {
                    var count = int.Parse(result.Split(' ')[0].Trim());
                    if (result.Contains(keyValuePair.Key) && count > keyValuePair.Value)
                        return false;
                }
            }
            return true;
        }
        
        public static void Main(string[] args)
        {
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\inputs\day2.txt"));
            var lines = File.ReadLines(path);


            var sumOfIds = 0;

            foreach (var line in lines)
            {
                var game = line.Split(':');
                var gameId = int.Parse(string.Join("", game[0].Where(c => int.TryParse(c.ToString(), out _))));

                var results = game[1].Split(';');
                var resultsString = string.Join(",", results);
                var resultsCommaSplit = resultsString.Split(',');
                var resultsSanitized = resultsCommaSplit.Select(s => s.Trim()).ToArray();

                if (IsPossible(resultsSanitized))
                    sumOfIds += gameId;
            }
            
            Console.WriteLine(sumOfIds);
        }
    }
}