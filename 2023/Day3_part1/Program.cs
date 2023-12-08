using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
// For readability
// ReSharper disable InvertIf

namespace Day3
{
    internal static class Program
    {
        private static readonly IList<string> Punctuations = new List<string>
        {
            ",", ";", ":", "?", "!", "\"", "'", "(", ")", "[", "]", "{", "}", "–", "-", "/", "\\",
            "&", "@", "#", "$", "%", "^", "*", "_", "+", "-", "=", "<", ">", "|", "~"
        };
        private static bool IsNumber(char c)
        {
            return int.TryParse(c.ToString(), out _);
        }

        private static int GetNumberEndIndex(string line, int startIndex)
        {
            while (true)
            {
                if (startIndex == line.Length || !IsNumber(line[startIndex])) 
                    return startIndex;
                
                startIndex++;
            }
        }

        private static bool IsNextSymbol(string line, int endIndex)
        {
            if (line.Length - 1 < endIndex + 1) 
                return false;
            
            var nextChar = line[endIndex + 1].ToString();
            return Punctuations.Contains(nextChar);
        }
        
        private static bool IsPreviousSymbol(string line, int startIndex)
        {
            if (startIndex - 1 < 0) 
                return false;
            
            var previousChar = line[startIndex - 1].ToString();
            return Punctuations.Contains(previousChar);
        }

        private static void AddNumberToList(ICollection<int> numbersList, string line, int startIndex, int endIndex)
        {
            var number = int.Parse(line.Substring(startIndex, endIndex - startIndex));
            numbersList.Add(number);
        }

        private static string GetLineSubstring(string line, int startIndex, int endIndex)
        {
            var start = startIndex > 0 ? startIndex - 1 : 0;
            var end = endIndex < line.Length ? endIndex + 1 : endIndex;
            return line.Substring(start, end - start);
        }
        
        public static void Main(string[] args)
        {
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\inputs\day3.txt"));
            var lines = File.ReadLines(path).ToArray();
            
            // Removed period from the list.
            var punctuationList = new List<string>
            {
                ",", ";", ":", "?", "!", "\"", "'", "(", ")", "[", "]", "{", "}", "–", "-", "/", "\\",
                "&", "@", "#", "$", "%", "^", "*", "_", "+", "-", "=", "<", ">", "|", "~"
            };

            var numbersToAdd = new List<int>();
            
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var previousLine = i > 0 ? lines[i - 1] : "";
                var nextLine = i < lines.Length - 1 ? lines[i + 1] : "";

                for (var j = 0; j < line.Length; j++)
                {
                    var curr = line[j];

                    if (!IsNumber(curr)) 
                        continue;
                    
                    var endIndex = GetNumberEndIndex(line, j);

                    if (IsNextSymbol(line, endIndex - 1))
                    {
                        AddNumberToList(numbersToAdd, line, j, endIndex);
                        j = endIndex;
                        continue;
                    }
                        
                    if (IsPreviousSymbol(line, j))
                    {
                        AddNumberToList(numbersToAdd, line, j, endIndex);
                        j = endIndex;
                        continue;
                    }

                    if (previousLine.Length > 0)
                    {
                        var substr = GetLineSubstring(previousLine, j, endIndex);
                        if (punctuationList.Any(p => substr.Contains(p)))
                        {
                            AddNumberToList(numbersToAdd, line, j, endIndex);
                            j = endIndex;
                            continue;
                        }
                    }
                        
                    if (nextLine.Length > 0)
                    {
                        var substr = GetLineSubstring(nextLine, j, endIndex);
                        if (punctuationList.Any(p => substr.Contains(p)))
                        {
                            AddNumberToList(numbersToAdd, line, j, endIndex);
                            j = endIndex;
                        }
                    }
                }
            }
            Console.WriteLine(numbersToAdd.Sum());
        }
    }
}