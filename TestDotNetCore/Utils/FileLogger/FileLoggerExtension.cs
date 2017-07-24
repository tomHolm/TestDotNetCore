using Microsoft.Extensions.Logging;

namespace TestDotNetCore.Utils.FileLogger
{
    public static class FileLoggerExtension
    {
		public static ILoggerFactory AddFile(this ILoggerFactory factory, string path)
		{
			factory.AddProvider(new FileLoggerProvider(path));
			return factory;
		}
    }
}
