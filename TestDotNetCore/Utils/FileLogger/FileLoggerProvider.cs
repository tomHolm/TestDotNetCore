using Microsoft.Extensions.Logging;

namespace TestDotNetCore.Utils.FileLogger
{
    public class FileLoggerProvider : ILoggerProvider
    {
		private string _path;

		public FileLoggerProvider(string path)
		{
			this._path = path;
		}

		public ILogger CreateLogger(string name)
		{
			return new FileLogger(this._path);
		}

		public void Dispose()
		{
		}
    }
}
