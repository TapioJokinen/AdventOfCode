using Days;
using Xunit.Abstractions;

namespace Tests;

public class Day1Tests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Day1Part1Test()
    {
        TestUtils.MeasureExecution(Day1.Part1, 1000000, testOutputHelper);
    }

    [Fact]
    public void Day1Part2Test()
    {
        TestUtils.MeasureExecution(Day1.Part2, 1000000, testOutputHelper);
    }
}