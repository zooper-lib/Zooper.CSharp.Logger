using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zooper.CSharp.Logging.Formatters;
using Zooper.CSharp.Logging.Writers;

namespace Zooper.CSharp.Logging.Loggers
{
	public class Logger
	{
		private readonly List<LogWriter> _logWriters;
		private readonly LogFormatter? _logFormatter;

		public Logger(LogLevel minimumLoggingLevel, List<LogWriter> logWriters, LogFormatter? logFormatter = null)
		{
			MinimumLoggingLevel = minimumLoggingLevel;
			_logWriters = logWriters;
			_logFormatter = logFormatter;
		}

		public LogLevel MinimumLoggingLevel { get; set; }

		/// <summary>
		/// Log any message
		/// </summary>
		/// <param name="message"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>
		public Task LogVerboseAsync(string message, string? stackTrace) =>
			Log(message, LogLevel.Verbose, stackTrace);

		/// <summary>
		/// Log a message only for debugging
		/// </summary>
		/// <param name="message"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>
		public Task LogDebugAsync(string message, string? stackTrace) =>
			Log(message, LogLevel.Debug, stackTrace);

		/// <summary>
		/// Log a info message
		/// </summary>
		/// <param name="message"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>
		public Task LogInfoAsync(string message, string? stackTrace) =>
			Log(message, LogLevel.Info, stackTrace);

		/// <summary>
		/// Log a warning
		/// </summary>
		/// <param name="message"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>
		public Task LogWarningAsync(string message, string? stackTrace) =>
			Log(message, LogLevel.Warning, stackTrace);

		/// <summary>
		/// Log an error
		/// </summary>
		/// <param name="message"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>
		public Task LogErrorAsync(string message, string? stackTrace) =>
			Log(message, LogLevel.Error, stackTrace);

		/// <summary>
		/// <para>Logs a message when you don't know what is happening</para>
		/// <para>WTFs are logged independently of the logging level</para>
		/// </summary>
		/// <param name="message"></param>
		/// <param name="stackTrace"></param>
		/// <returns></returns>
		public Task LogWtfAsync(string message, string? stackTrace) =>
			Log(message, LogLevel.Wtf, stackTrace);

		/// <summary>
		/// Log a message with it's StackTrace and level
		/// </summary>
		/// <param name="message">The message to log</param>
		/// <param name="level">The LogLevel of this log</param>
		/// <param name="stackTrace">The StackTrace</param>
		public Task Log(string message, LogLevel level, string? stackTrace = null)
		{
			stackTrace ??= Environment.StackTrace;

			// If the level is less than the minimum level
			if (level < MinimumLoggingLevel && level != LogLevel.Wtf)
			{
				return Task.CompletedTask;
			}

			if (_logFormatter != null)
			{
				message = _logFormatter!.Format(message, stackTrace, level);
			}

			Task.WaitAll(_logWriters.Select(logWriter => logWriter.WriteAsync(message)).ToArray());

			return Task.CompletedTask;
		}
	}
}