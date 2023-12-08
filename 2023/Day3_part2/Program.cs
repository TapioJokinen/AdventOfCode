using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
// ReSharper disable InvertIf

namespace Day3_part2
{
    internal static class Program
    {
        private static bool IsNumber(char c)
        {
            return int.TryParse(c.ToString(), out _);
        }

        private static string CreateNumberFromLine(string line, int index, bool asc)
        {
            var numberBuilder = new StringBuilder();
            var counter = asc ? index + 1 : index - 1;
            while (true)
            {
                if (counter >= line.Length || !IsNumber(line[counter]))
                    break;


                switch (asc)
                {
                    case true:
                        numberBuilder.Append(line[counter]);
                        counter++;
                        break;
                    default:
                        numberBuilder.Insert(0, line[counter]);
                        counter--;
                        break;
                }
            }
            return numberBuilder.ToString();
        }
        
        private static IEnumerable<int> FindLineNumbers(string line, int index)
        {
            var numbers = new List<int>();

            var middle = line[index];
            var prefix = CreateNumberFromLine(line, index, false);
            var suffix = CreateNumberFromLine(line, index, true);

            if (!IsNumber(middle))
            {
                if(prefix.Length > 0)
                    numbers.Add(int.Parse(prefix));
                if(suffix.Length > 0)
                    numbers.Add(int.Parse(suffix));
            }
            else
            {
                if (prefix.Length > 0 || suffix.Length > 0 || IsNumber(middle))
                {
                    numbers.Add(int.Parse($"{prefix}{middle}{suffix}".Trim()));
                }
            }

            return numbers;
        }
        private static IEnumerable<int> FindAdjacentNumbers(string line, string previousLine, string nextLine, int index)
        {
            var sameLineNumbers = FindLineNumbers(line, index);
            var previousLineNumbers = FindLineNumbers(previousLine, index);
            var nextLineNumbers = FindLineNumbers(nextLine, index);

            return sameLineNumbers.Concat(previousLineNumbers).Concat(nextLineNumbers);
        }
        
        public static void Main(string[] args)
        {
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\inputs\day3.txt"));
            var lines = File.ReadLines(path).ToArray();

            var sumOfGears = 0;
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var previousLine = i > 0 ? lines[i - 1] : "";
                var nextLine = i < lines.Length - 1 ? lines[i + 1] : "";

                for (var j = 0; j < line.Length; j++)
                {
                    var character = line[j];

                    if (character != '*')
                        continue;

                    var adjacentNumbers = FindAdjacentNumbers(line, previousLine, nextLine, j).ToList();
                    
                    if (adjacentNumbers.Count == 2)
                        sumOfGears += adjacentNumbers.First() * adjacentNumbers.Last();
                }
            }
            Console.WriteLine(sumOfGears);
        }
    }
}