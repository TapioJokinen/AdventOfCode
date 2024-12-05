using Days;
using Xunit.Abstractions;

namespace Tests;

public class Day4Tests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Day4Part1Test()
    {
        var result = TestUtils.MeasureExecution(Day4.Part1, 10_000, testOutputHelper);

        Assert.Equal(2378, result);
    }

    [Fact]
    public void Day4Part2Test()
    {
        var result = TestUtils.MeasureExecution(Day4.Part2, 10_000, testOutputHelper);

        Assert.Equal(1796, result);
    }
}