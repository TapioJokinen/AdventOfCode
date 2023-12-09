using System;
using System.IO;
using System.Linq;

namespace Day4_part1
{
    internal static class Program
    {
        private static (string[], string[]) GetResultsFromLine(string line)
        {
            var game = line.Split(':');
            var leftAndRight = game[1].Split('|');
            var left = leftAndRight[0].Split(' ');
            var right = leftAndRight[1].Split(' ');

            return (left.Where(l => l != "" && l != " " ).ToArray(), right.Where(r => r != "" && r != " " ).ToArray());
        }
        
        public static void Main(string[] args)
        {
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\inputs\day4.txt"));
            var lines = File.ReadLines(path).ToArray();

            var sumOfPoints = 0;
            foreach (var line in lines)
            {
                var points = 0;
                var (left,  right) = GetResultsFromLine(line);

                foreach (var l in left)
                {
                    foreach (var r in right)
                    {
                        if (l != r) 
                            continue;

                        if (points > 0)
                        {
                            points *= 2;
                        }
                        else
                        {
                            points += 1;
                        }
                    }
                }

                sumOfPoints += points;
            }
            Console.WriteLine(sumOfPoints);
        }
    }
}