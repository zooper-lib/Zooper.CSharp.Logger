using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Zooper.CSharp.Logging.Formatters;
using Zooper.CSharp.Logging.Loggers;
using Zooper.CSharp.Logging.Writers;

namespace Zooper.CSharp.Logging.Tests
{
	public class LoggerTest
	{
		private readonly ITestOutputHelper _output;

		public LoggerTest(ITestOutputHelper output)
		{
			_output = output;
		}

		/// <summary>
		/// THIS IS NO REAL TEST!
		/// </summary>
		[Fact]
		public void Test1()
		{
			var formatter = new PrettyFormatter();
			var writer = new ConsoleLogWriter();

			string formatted = formatter.Format("Test", Environment.StackTrace, LogLevel.Debug);
			_output.WriteLine(formatted);

			var logger = new Logger(LogLevel.Debug, new List<LogWriter>() { writer }, formatter);

			logger.Log("Test", LogLevel.Error);
		}
	}
}