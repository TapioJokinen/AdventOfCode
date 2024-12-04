using Days;
using Xunit.Abstractions;

namespace Tests;

public class Day2Tests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Day2Part1Test()
    {
        var result = TestUtils.MeasureExecution(Day2.Part1, 10_000, testOutputHelper);

        Assert.Equal(332, result);
    }

    [Fact]
    public void Day2Part2Test()
    {
        var result = TestUtils.MeasureExecution(Day2.Part2, 10_000, testOutputHelper);

        Assert.Equal(398, result);
    }
}