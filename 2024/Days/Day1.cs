using System.Text;

namespace Days;

public static class Day1
{
    public static int Part1()
    {
        var filePath = Utils.GetInputFilePathByDayAndPart(1, 1);

        const short lineCount = 1000;
        const short bufferSize = 4096;

        using var fileStream = File.OpenRead(filePath);
        using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize);

        var left = new int[lineCount];
        var right = new int[lineCount];

        var sum = 0;
        var index = 0;

        while (streamReader.ReadLine() is { } line)
        {
            var span = line.AsSpan();
            var leftNumEnd = span.IndexOf(' ');
            var rightNumStart = span.LastIndexOf(' ') + 1;

            left[index] = int.Parse(span[..leftNumEnd]);
            right[index] = int.Parse(span[rightNumStart..]);
            index++;
        }

        Array.Sort(left, 0, index);
        Array.Sort(right, 0, index);

        for (var i = 0; i < lineCount; i++) sum += Math.Abs(left[i] - right[i]);

        return sum;
    }

    public static int Part2()
    {
        var filePath = Utils.GetInputFilePathByDayAndPart(1, 2);

        const short lineCount = 1000;
        const short bufferSize = 4096;

        using var fileStream = File.OpenRead(filePath);
        using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize);

        var dict = new Dictionary<int, int>();

        var rightNums = new int[lineCount];
        var index = 0;
        var sum = 0;

        while (streamReader.ReadLine() is { } line)
        {
            var span = line.AsSpan();
            var leftNumEnd = span.IndexOf(' ');
            var rightNumStart = span.LastIndexOf(' ') + 1;

            dict.TryAdd(int.Parse(span[..leftNumEnd]), 0);
            rightNums[index] = int.Parse(span[rightNumStart..]);

            index++;
        }

        for (var i = 0; i < lineCount; i++)
            if (dict.ContainsKey(rightNums[i]))
                dict[rightNums[i]] += 1;

        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (var keyValue in dict)
            sum += keyValue.Key * keyValue.Value;

        return sum;
    }
}