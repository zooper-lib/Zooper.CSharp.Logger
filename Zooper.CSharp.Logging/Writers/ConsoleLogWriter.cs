using System;
using System.Threading.Tasks;

namespace Zooper.CSharp.Logging.Writers
{
	public class ConsoleLogWriter : LogWriter
	{
		public override Task WriteAsync(string message)
		{
			Console.WriteLine(message);

			return Task.CompletedTask;
		}
	}
}
