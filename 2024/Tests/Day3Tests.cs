using Days;
using Xunit.Abstractions;

namespace Tests;

public class Day3Tests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Day3Part1Test()
    {
        var result = TestUtils.MeasureExecution(Day3.Part1, 10_000, testOutputHelper);

        Assert.Equal(157621318, result);
    }

    [Fact]
    public void Day3Part2Test()
    {
        var result = TestUtils.MeasureExecution(Day3.Part2, 10_000, testOutputHelper);

        Assert.Equal(79845780, result);
    }
}