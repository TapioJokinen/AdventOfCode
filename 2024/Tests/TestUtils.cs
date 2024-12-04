using System.Diagnostics;
using Xunit.Abstractions;

namespace Tests;

public static class TestUtils
{
    public static int MeasureExecution(Func<int> functionToTest, int iterations, ITestOutputHelper testOutputHelper)
    {
        var elapsedList = new List<long>();
        var result = 0;
        for (var i = 0; i < iterations; i++)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            result = functionToTest();
            stopwatch.Stop();
            var elapsed = stopwatch.Elapsed;
            elapsedList.Add(elapsed.Microseconds);
        }

        testOutputHelper.WriteLine("Result: " + result);
        testOutputHelper.WriteLine("Average elapsed: " + elapsedList.Average() + "µs");

        return result;
    }
}