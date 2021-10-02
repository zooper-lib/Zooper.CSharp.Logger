using Zooper.CSharp.Logging.Loggers;

namespace Zooper.CSharp.Logging.Formatters
{
    /// <summary>
    /// Base class for all formatters
    /// </summary>
    public abstract class LogFormatter
    {
        public abstract string Format(string message, string? stackTrace, LogLevel level);
    }
}
