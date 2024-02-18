static class Logger
{
    public static void ConfigureLogging()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Hour)
            .MinimumLevel.Debug()
            .CreateLogger();
    }
}
