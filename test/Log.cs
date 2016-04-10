using System;
using System.Globalization;
using System.Threading;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Test
{
	/// <summary>
	/// Static provider for Microsoft.Extensions.Logging.ILogger.
	/// </summary>
	public static class Log
	{
		public static ILogger Logger => LoggerFactory.CreateLogger("<default>");


		public static ILoggerFactory LoggerFactory => s_loggerFactory.Value;


		private static readonly Lazy<ILoggerFactory> s_loggerFactory = new Lazy<ILoggerFactory>(() =>
		{
			Serilog.ILogger logger = new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.WriteTo.LiterateConsole(
					Serilog.Events.LogEventLevel.Verbose,
					"[{Timestamp:HH:mm:ss} {SourceContext} {Level}] {Message}{NewLine}{Exception}",
					CultureInfo.InvariantCulture)
				.CreateLogger();

			ILoggerFactory factory = new LoggerFactory().AddSerilog(logger);

			factory.MinimumLevel = LogLevel.Verbose;

			return factory;
		}, LazyThreadSafetyMode.ExecutionAndPublication);
	}
}