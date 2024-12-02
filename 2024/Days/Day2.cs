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
            var numbers = line.Split();
            var isDescending = false;
            var isSet = false;
            var isSafe = true;
            var counter = 0;
            
            while (counter < numbers.Length - 1)
            {
                var left = int.Parse(numbers[counter]);
                var right = int.Parse(numbers[counter + 1]);
                var diff = left - right;
                var absDiff = diff < 0 ? -diff : diff;  // Slightly faster than Math.Abs for some reason.
            
                var isOneToThree = absDiff is >= 1 and <= 3;
            
                if (!isOneToThree)
                {
                    isSafe = false;
                    break;
                }
            
                if (isSet)
                {
                    if (isDescending && right > left)
                    {
                        isSafe = false;
                        break;
                    }
            
                    if (!isDescending && left > right)
                    {
                        isSafe = false;
                        break;
                    }
            
                    counter++;
                    continue;
                }
            
                isDescending = left > right;
                isSet = true;
                counter++;
            }

            if (isSafe)
                safe++;
        }

        return safe;
    }
}