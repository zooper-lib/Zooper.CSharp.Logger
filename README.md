This library provides a wrapper for loggers. Some examples are included.
Note: This library is part of the whole zooper lib family.

## Getting started

Import the Nuget package:

`nuget install Zooper.CSharp.Logging`

## Usage

To use the logger you have to instantiate at least 2 classes:

### The log writer

The logger needs a writer it can write the log to. This can be the console,
an api or a file. You can also define multiple writer which the logger writes to simultanously.

``` csharp
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
```

This is an example of a console writer which also can be found inside this package.

``` csharp
var writer = new ConsoleLogWriter();
```

### The logger

After that, you need to pass the writer to the logger:

``` csharp
var logger = new Logger(minimumLoggingLevel, logWriters);
```

The `minimumLoggingLevel` defines what type of log should be logged. These options are available (starting from the lowest to the highest):

``` csharp
public enum LogLevel {
  verbose,
  debug,
  info,
  warning,
  error,
  wtf,
}
```

The `logWriters` expects a list of writers like our `ConsoleLogWriter`.

After all is set up, you can log messages like so:

``` csharp 
logger.LogInfoAsync('Your message');
```

or with `StackTrace`

``` csharp 
logger.LogInfoAsync('Your message', Environment.StackTrace);
```

or

``` csharp
logger.Log('Your message', LogLevel.Info, Environment.StackTrace);
```

### Formatting the output

Use the `LogFormatter` base class to dress up the output. An example class is also included named `PrettyFormatter`.

``` csharp
var formatter = new PrettyFormatter();
```

Then you can pass it to our logger:

``` csharp
var logger = new Logger(<minimumLoggingLevel>, <logWriters>, formatter);
```