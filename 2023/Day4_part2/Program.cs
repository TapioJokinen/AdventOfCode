using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4_part2
{
    internal static class Program
    {
        private static (int gameId, string[], string[]) GetResultsFromLine(string line)
        {
            var game = line.Split(':');
            var gameId = int.Parse(string.Join("", game[0].Where(c => int.TryParse(c.ToString(), out _))));
            var leftAndRight = game[1].Split('|');
            var left = leftAndRight[0].Split(' ');
            var right = leftAndRight[1].Split(' ');

            return (gameId, left.Where(l => l != "" && l != " " ).ToArray(), right.Where(r => r != "" && r != " " ).ToArray());
        }
        
        public static void Main(string[] args)
        {
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\inputs\day4.txt"));
            var lines = File.ReadLines(path).ToArray();

            var scratchcards = new Dictionary<int, int>();
            
            foreach (var line in lines)
            {
                var (gameId, left,  right) = GetResultsFromLine(line);

                if (scratchcards.ContainsKey(gameId))
                {
                    scratchcards[gameId] += 1;
                }
                else
                {
                    scratchcards.Add(gameId, 1);
                }

                var points = (from l in left from r in right where l == r select l).Count();

                for (var i = 0; i < scratchcards[gameId]; i++)
                {
                    for (var j = 1; j <= points; j++)
                    {
                        if (scratchcards.ContainsKey(gameId + j))
                        {
                            scratchcards[gameId + j] += 1;
                        }
                        else
                        {
                            scratchcards.Add(gameId + j, 1);
                        }
                    }
                }
            }

            Console.WriteLine(scratchcards.Values.Sum());
        }
    }
}