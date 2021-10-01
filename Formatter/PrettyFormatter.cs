using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zooper.CSharp.Core.Extensions;
using Zooper.CSharp.Logging.Logger;

namespace Zooper.CSharp.Logging.Formatter
{
	public class PrettyFormatter : LogFormatter
	{
		private const char TopLeftCorner = '┌';
		private const char BottomLeftCorner = '└';
		private const char MiddleCorner = '├';
		private const char VerticalLine = '│';
		private const char DoubleDivider = '─';
		private const char NewLine = '\n';

		private readonly string _topBorder;
		private readonly string _middleBorder;
		private readonly string _bottomBorder;

		private static readonly Dictionary<LogLevel, string> LevelEmojis = new()
		{
			{ LogLevel.Verbose, "" },
			{ LogLevel.Debug, "🐛" },
			{ LogLevel.Info, "💡" },
			{ LogLevel.Warning, "⚠️" },
			{ LogLevel.Error, "⛔" },
			{ LogLevel.Wtf, "👾" },
		};

		private readonly bool _printEmojis;

		public PrettyFormatter(int lineLength = 120, bool printEmojis = true)
		{
			_printEmojis = printEmojis;

			var doubleDividerLine = new StringBuilder();
			for (var i = 0; i < lineLength - 1; i++)
			{
				doubleDividerLine.Append(DoubleDivider);
			}

			_topBorder = $"{TopLeftCorner}{doubleDividerLine}";
			_middleBorder = $"{MiddleCorner}{doubleDividerLine}";
			_bottomBorder = $"{BottomLeftCorner}{doubleDividerLine}";
		}


		public override string Format(string message, string? stackTrace, LogLevel level)
		{
			return _format(
				level,
				message,
				DateTime.Now.ToIso8601(),
				stackTrace
			);
		}

		private string _format(
			LogLevel level,
			string message,
			string? time,
			string? stacktrace
		)
		{
			List<string> lineBuffer = new() { _topBorder };

			// The time
			if (time != null)
			{
				lineBuffer.Add($"{VerticalLine} {time}");
				lineBuffer.Add(_middleBorder);
			}

			// The message
			string? emoji = _getEmoji(level);
			lineBuffer.AddRange(message.Split(NewLine).Select(line => $"{VerticalLine} {emoji} {line}"));

			lineBuffer.Add(_middleBorder);

			// The StackTrace
			if (stacktrace != null)
			{
				lineBuffer.AddRange(stacktrace.Split(NewLine).Select(line => $"{VerticalLine} {line}"));
			}

			lineBuffer.Add(_bottomBorder);

			return string.Join(NewLine, lineBuffer);
		}

		private string _getEmoji(LogLevel level)
		{
			return _printEmojis ? LevelEmojis[level]! : String.Empty;
		}
	}
}