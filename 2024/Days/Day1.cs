using System.Text;

namespace Days;

public static class Day1
{
    public static int Part1()
    {
        var filePath = Utils.GetInputFilePathByDayAndPart(1, 1);

        const short lineCount = 1000;
        const short bufferSize = 4096;
        var sum = 0;
        var index = 0;

        var left = new int[lineCount];
        var right = new int[lineCount];

        using var fileStream = File.OpenRead(filePath);
        using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize);

        while (streamReader.ReadLine() is { } line)
        {
            var strings = line.Split();
            var leftNum = int.Parse(strings[0]);
            var rightNum = int.Parse(strings[^1]);

            left[index] = leftNum;
            right[index] = rightNum;
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

        var dict = new Dictionary<int, int>();
        var rightNums = new int[lineCount];
        var index = 0;
        var sum = 0;

        using var fileStream = File.OpenRead(filePath);
        using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize);

        while (streamReader.ReadLine() is { } line)
        {
            var strings = line.Split();
            var leftNum = int.Parse(strings[0]);
            var rightNum = int.Parse(strings[^1]);

            dict.TryAdd(leftNum, 0);

            rightNums[index] = rightNum;
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