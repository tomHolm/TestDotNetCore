using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace TestDotNetCore.Utils.FileLogger
{
	public class FileLogger : ILogger
	{
		private string filePath;
		private object _lock = new object();

		public FileLogger(string path)
		{
			this.filePath = path;
		}

		public IDisposable BeginScope<TState>(TState state)
		{
			return null;
		}

		public bool IsEnabled(LogLevel logLevel)
		{
			return true; // logLevel < LogLevel.Information;
		}

		public void Log<TState>(LogLevel level, EventId eventId, TState state, Exception e, Func<TState, Exception, string> formatter)
		{
			if (formatter != null)
			{
				lock (this._lock)
				{
					File.AppendAllText(this.filePath, formatter(state, e) + Environment.NewLine);
				}
			}
		}
	}
}
