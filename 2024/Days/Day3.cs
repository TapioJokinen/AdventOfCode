using System.Text;

namespace Days;

public static class Day3
{
    public static int Part1()
    {
        var filePath = Utils.GetInputFilePathByDayAndPart(3, 1);

        const short bufferSize = 4096;

        using var fileStream = File.OpenRead(filePath);
        using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize);

        var sum = 0;

        while (streamReader.ReadLine() is { } line)
        {
            var span = line.AsSpan();

            for (var i = 0; i < span.Length; i++)
            {
                if (span[i] != 'm' || span[i + 1] != 'u' || span[i + 2] != 'l' || span[i + 3] != '(')
                    continue;

                var multiplication = CalculateNumbers(ref i, span);
                if (multiplication != -1)
                    sum += multiplication;
            }
        }

        return sum;
    }

    public static int Part2()
    {
        var filePath = Utils.GetInputFilePathByDayAndPart(3, 2);

        const short bufferSize = 4096;

        using var fileStream = File.OpenRead(filePath);
        using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize);

        var sum = 0;
        var canDo = true;

        while (streamReader.ReadLine() is { } line)
        {
            var span = line.AsSpan();

            for (var i = 0; i < span.Length; i++)
            {
                if (span[i] == 'd' && span[i + 1] == 'o' && span[i + 2] == '(' && span[i + 3] == ')')
                {
                    canDo = true;
                    i += 4;
                }

                if (span[i] == 'd' && span[i + 1] == 'o' && span[i + 2] == 'n' && span[i + 3] == '\'' &&
                    span[i + 4] == 't' && span[i + 5] == '(' && span[i + 6] == ')')
                {
                    canDo = false;
                    i += 7;
                }

                if (!canDo)
                    continue;

                if (span[i] != 'm' || span[i + 1] != 'u' || span[i + 2] != 'l' || span[i + 3] != '(')
                    continue;

                var multiplication = CalculateNumbers(ref i, span);
                if (multiplication != -1)
                    sum += multiplication;
            }
        }

        return sum;
    }

    private static int CalculateNumbers(ref int index, ReadOnlySpan<char> span)
    {
        var startIndex = index + 4;
        var firstNumberEndIndex = span[startIndex..].IndexOf(',');
        var firstNumberSpan = span.Slice(startIndex, firstNumberEndIndex);
        if (!int.TryParse(firstNumberSpan, out var firstNumber))
            return -1;

        var secondNumberStartIndex = startIndex + firstNumberEndIndex + 1;
        var secondNumberEndIndex = span[secondNumberStartIndex..].IndexOf(')');
        var secondNumberSpan = span.Slice(secondNumberStartIndex, secondNumberEndIndex);
        if (!int.TryParse(secondNumberSpan, out var secondNumber))
            return -1;

        return firstNumber * secondNumber;
    }
}