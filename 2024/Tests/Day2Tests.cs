using Days;
using Xunit.Abstractions;

namespace Tests;

public class Day2Tests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Day2Part1Test()
    {
        TestUtils.MeasureExecution(Day2.Part1, 10000, testOutputHelper);
    }
}