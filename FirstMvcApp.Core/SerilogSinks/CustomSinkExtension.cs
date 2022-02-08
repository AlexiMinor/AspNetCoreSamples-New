using Serilog;
using Serilog.Configuration;

namespace FirstMvcApp.Core.SerilogSinks;

public static class CustomSinkExtension
{
    public static LoggerConfiguration CustomSink(
        this LoggerSinkConfiguration loggerConfiguration,
        IFormatProvider formatProvider = null)
    {
        return loggerConfiguration.Sink(new CustomSink(formatProvider));
    }
}