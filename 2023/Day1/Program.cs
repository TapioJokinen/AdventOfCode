using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\inputs\day1.txt"));
            var lines = File.ReadLines(path);
            
            var numbers = new Dictionary<string, int>
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 }
            };

            var sumOfNumbers = 0;
            foreach (var line in lines)
            {
                var numbersToSum = new List<int>();

                var counter = 0;

                while (true)
                {
                    if (counter > line.Length)
                        break;

                    var word = line.Substring(0, counter);

                    var found = false;

                    foreach (var keyValuePair in numbers.Where(keyValuePair => word.Contains(keyValuePair.Key)))
                    {
                        found = true;
                        numbersToSum.Add(keyValuePair.Value);
                    }
                    
                    if (found)
                        break;

                    if (word.Length > 0 && int.TryParse(word.Last().ToString(), out var n))
                    {
                        numbersToSum.Add(n);
                        break;
                    }

                    counter++;
                }
                
                var idx2 = line.Length - 1;
                var counter2 = idx2;

                while (true)
                {
                    if (counter < 0)
                        break;

                    var word = line.Substring(counter2, idx2 - counter2 + 1);

                    if (word.Length > 0 && int.TryParse(word.First().ToString(), out var n))
                    {
                        numbersToSum.Add(n);
                        break;
                    }

                    var found = false;
                    foreach (var keyValuePair in numbers.Where(keyValuePair => word.Contains(keyValuePair.Key)))
                    {
                        found = true;
                        numbersToSum.Add(keyValuePair.Value);
                    }
                    
                    if (found)
                        break;

                    counter2--;
                }

                sumOfNumbers += int.Parse($"{numbersToSum.First()}{numbersToSum.Last()}");
            }

            Console.WriteLine(sumOfNumbers);
        }
        
    }
}