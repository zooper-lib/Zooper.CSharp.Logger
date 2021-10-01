using System.Threading.Tasks;

namespace Zooper.CSharp.Logging.Writers
{
	public abstract class LogWriter
	{
		public abstract Task WriteAsync(string message);
	}
}
