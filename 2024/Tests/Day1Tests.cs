using Days;
using Xunit.Abstractions;

namespace Tests;

public class Day1Tests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Day1Part1Test()
    {
        var result = TestUtils.MeasureExecution(Day1.Part1, 10_000, testOutputHelper);

        Assert.Equal(1889772, result);
    }

    [Fact]
    public void Day1Part2Test()
    {
        var result = TestUtils.MeasureExecution(Day1.Part2, 10_000, testOutputHelper);

        Assert.Equal(23228917, result);
    }
}