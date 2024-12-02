namespace Days;

public static class Utils
{
    public static string GetInputFilePathByDayAndPart(int day, int part)
    {
        var projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(projectDirectory, "Inputs", $"day-{day}_part-{part}.txt");
    }
}