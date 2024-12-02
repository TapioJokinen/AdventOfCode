using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day5_part1
{
    internal static class Program
    {
        private static IEnumerable<string> GetSeeds(string line)
        {
            return line.Split(':')[1].Trim().Split(' ');
        }
 
        private static string FindDestination(string line, string seed)
        {
            var numbers = line.Split(' ');
 
            var seedToLong = long.Parse(seed);
            var destination = long.Parse(numbers[0]);
            var source = long.Parse(numbers[1]);
            var range = long.Parse(numbers[2]);
 
            if(source > seedToLong || !(source <= seedToLong && seedToLong <= source + range))
                return seedToLong.ToString();

            var result = seedToLong - source + destination;
            return result.ToString();
        }

        private static string GetLocation(IEnumerable<string> routes, string previousRoute)
        {
            var location = routes.Where(r => r != previousRoute).ToList();
            return location.Count > 0 ? location.First() : previousRoute;
        }
 
        public static void Main(string[] args)
        {
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\inputs\day5.txt"));
            var lines = File.ReadLines(path).ToArray();
            
            var sw = Stopwatch.StartNew();
            // All seeds.
            var seeds = GetSeeds(lines[0]);
            
            // Here we store all locations and in the end choose the smallest one.
            var locations = new List<string>();
            
            foreach (var seed in seeds)
            {
                var routes = new Dictionary<string, List<string>>()
                {
                    {"seed-to-soil map", new List<string>()},
                    {"soil-to-fertilizer map", new List<string>()},
                    {"fertilizer-to-water map", new List<string>()},
                    {"water-to-light map", new List<string>()},
                    {"light-to-temperature map", new List<string>()},
                    {"temperature-to-humidity map", new List<string>()},
                    {"humidity-to-location map", new List<string>()}
                };
                var location = seed;
                var map = "";
                
                foreach (var line in lines)
                {
                    if(line == lines[0] || string.IsNullOrEmpty(line))
                        continue;
 
                    if (!int.TryParse(line[0].ToString(), out _))
                    {
                        if (!string.IsNullOrEmpty(map))
                        {
                            location = routes[map].Any(s => s != location) 
                                ? routes[map].First(s => s != location) 
                                : routes[map].First();
                        }
                        map = line.Split(':')[0];
                        continue;
                    }
                    routes[map].Add(FindDestination(line, location));
                }
                // Traverse location
                var traverse1 = GetLocation(routes["seed-to-soil map"], seed);
                var traverse2 = GetLocation(routes["soil-to-fertilizer map"], traverse1);
                var traverse3 = GetLocation(routes["fertilizer-to-water map"], traverse2);
                var traverse4 = GetLocation(routes["water-to-light map"], traverse3);
                var traverse5 = GetLocation(routes["light-to-temperature map"], traverse4);
                var traverse6 = GetLocation(routes["temperature-to-humidity map"], traverse5);
                var finalLocation = GetLocation(routes["humidity-to-location map"], traverse6);
                locations.Add(finalLocation);
            }
            Console.WriteLine(locations.Select(long.Parse).Min());
            sw.Stop();
            Console.WriteLine("Time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }
    }
}