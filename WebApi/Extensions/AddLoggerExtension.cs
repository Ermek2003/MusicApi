using Serilog;

namespace WebApi.Extensions;

public static class AddLoggerExtension
{
    public static void AddLogger(this IHostBuilder hostBuilder)
    {
        //hostBuilder.UseSerilog((context, configuration) =>
        //    configuration.ReadFrom.Configuration(context.Configuration));
    }
}
