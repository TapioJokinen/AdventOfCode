using System.Text;

namespace Days;

public static class Day2
{
    public static int Part1()
    {
        var filePath = Utils.GetInputFilePathByDayAndPart(2, 1);

        const short bufferSize = 4096;
        var safe = 0;

        using var fileStream = File.OpenRead(filePath);
        using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize);

        while (streamReader.ReadLine() is { } line)
        {
            var span = line.AsSpan();
            if (ValidateLine(span))
                safe++;
        }

        return safe;
    }

    public static int Part2()
    {
        var filePath = Utils.GetInputFilePathByDayAndPart(2, 2);

        const short bufferSize = 4096;

        using var fileStream = File.OpenRead(filePath);
        using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize);

        var safe = 0;

        while (streamReader.ReadLine() is { } line)
        {
            var isSafe = false;
            var span = line.AsSpan();

            if (ValidateLine(span))
                isSafe = true;

            var lastSpaceIndex = span.LastIndexOf(' ');
            if (lastSpaceIndex != -1)
            {
                var lastOneRemovedSpan = span[..lastSpaceIndex];
                if (ValidateLine(lastOneRemovedSpan))
                    isSafe = true;
            }

            var lastIndex = 0;
            for (var i = 0; i < span.Length; i++)
            {
                if (isSafe)
                    break;

                if (i != span.Length && !char.IsWhiteSpace(span[i]))
                    continue;

                if (lastIndex == 0)
                {
                    if (ValidateLine(span.Slice(i, span.Length - i)))
                    {
                        isSafe = true;
                        break;
                    }
                }
                else
                {
                    var firstPart = span[..lastIndex];
                    var secondPart = span.Slice(i, span.Length - i);
                    Span<char> result = new char[firstPart.Length + secondPart.Length];

                    firstPart.CopyTo(result[..firstPart.Length]);
                    secondPart.CopyTo(result[firstPart.Length..]);

                    if (ValidateLine(result))
                    {
                        isSafe = true;
                        break;
                    }
                }

                lastIndex = i + 1;
            }

            if (isSafe)
                safe++;
        }

        return safe;
    }

    private static bool ValidateLine(ReadOnlySpan<char> span)
    {
        bool? isDescending = null;
        var isSafe = true;

        int? previousNumber = null;

        for (int i = 0, j = 0; j <= span.Length; j++)
        {
            if (j != span.Length && !char.IsWhiteSpace(span[j]))
                continue;

            if (!int.TryParse(span[i..j], out var currentNumber))
                continue;

            if (previousNumber.HasValue)
            {
                var diff = previousNumber.Value - currentNumber;
                var absDiff = diff < 0 ? -diff : diff;

                if (absDiff is < 1 or > 3)
                {
                    isSafe = false;
                    break;
                }

                if (!isDescending.HasValue)
                {
                    isDescending = previousNumber > currentNumber;
                }
                else if (isDescending.Value && currentNumber > previousNumber)
                {
                    isSafe = false;
                    break;
                }
                else if (!isDescending.Value && previousNumber > currentNumber)
                {
                    isSafe = false;
                    break;
                }
            }

            previousNumber = currentNumber;

            i = j + 1;
        }

        return isSafe;
    }
}