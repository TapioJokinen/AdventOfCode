using System.Text;

namespace Days;

public static class Day4
{
    private const int GridSize = 140;

    private static readonly List<ValueTuple<char, char, char, char>> Combos =
    [
        ('M', 'M', 'S', 'S'),
        ('S', 'S', 'M', 'M'),
        ('S', 'M', 'S', 'M'),
        ('M', 'S', 'M', 'S')
    ];

    public static int Part1()
    {
        var filePath = Utils.GetInputFilePathByDayAndPart(4, 1);

        const short bufferSize = 4096;
        var inputArray = new char[GridSize, GridSize];

        using var fileStream = File.OpenRead(filePath);
        using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize);

        var row = 0;

        while (streamReader.ReadLine() is { } line)
        {
            var span = line.AsSpan();
            for (var col = 0; col < span.Length; col++)
                inputArray[row, col] = span[col];

            row++;
        }

        return FindXmas(inputArray);
    }

    public static int Part2()
    {
        var filePath = Utils.GetInputFilePathByDayAndPart(4, 2);

        const short bufferSize = 4096;
        var grid = new char[GridSize, GridSize];

        using var fileStream = File.OpenRead(filePath);
        using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, bufferSize);

        var row = 0;

        while (streamReader.ReadLine() is { } line)
        {
            var span = line.AsSpan();
            for (var col = 0; col < span.Length; col++)
                grid[row, col] = span[col];

            row++;
        }

        return FindXmasX(grid);
    }

    private static int FindXmasX(char[,] grid)
    {
        var count = 0;

        for (var row = 0; row < GridSize; row++)
        for (var col = 0; col < GridSize; col++)
        {
            if (col + 2 >= GridSize)
                continue;

            if (row + 2 >= GridSize)
                continue;

            if (grid[row + 1, col + 1] != 'A')
                continue;

            count += FindMas(grid, row, col);
        }

        return count;
    }

    private static int FindMas(char[,] grid, int row, int col)
    {
        var count = 0;

        var first = grid[row, col];
        var second = grid[row, col + 2];
        var third = grid[row + 2, col];
        var fourth = grid[row + 2, col + 2];

        for (var i = 0; i < 4; i++)
        {
            var (a, b, c, d) = Combos[i];

            if (first == a && second == b && third == c && fourth == d)
                count++;
        }

        return count;
    }

    private static int FindXmas(char[,] grid)
    {
        var count = 0;

        var directions = new[]
        {
            (0, 1),
            (0, -1),
            (1, 0),
            (-1, 0),
            (1, 1),
            (-1, -1),
            (1, -1),
            (-1, 1)
        };

        for (var row = 0; row < GridSize; row++)
        for (var col = 0; col < GridSize; col++)
        {
            if (grid[row, col] != 'X') continue;

            foreach (var (rowDelta, colDelta) in directions)
                if (IsXmasInDirection(grid, row, col, rowDelta, colDelta))
                    count++;
        }

        return count;
    }

    private static bool IsXmasInDirection(char[,] grid, int row, int col, int rowDelta, int colDelta)
    {
        for (var step = 1; step <= 3; step++)
        {
            var newRow = row + step * rowDelta;
            var newCol = col + step * colDelta;

            if (newRow < 0 || newRow >= GridSize || newCol < 0 || newCol >= GridSize)
                return false;

            var expectedChar = step switch
            {
                1 => 'M',
                2 => 'A',
                3 => 'S',
                _ => throw new InvalidOperationException()
            };

            if (grid[newRow, newCol] != expectedChar)
                return false;
        }

        return true;
    }
}