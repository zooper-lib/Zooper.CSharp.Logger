using System.Diagnostics;
using Zooper.CSharp.Logging.Logger;

namespace Zooper.CSharp.Logging.Formatter
{
    /// <summary>
    /// Base class for all formatters
    /// </summary>
    public abstract class LogFormatter
    {
        public abstract string Format(string message, string? stackTrace, LogLevel level);
    }
}
