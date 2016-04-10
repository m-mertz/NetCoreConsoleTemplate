using System;
using Microsoft.Extensions.Logging;

namespace Test
{
	/// <summary>
	/// Convenience methods for asserting including logging.
	/// </summary>
	public static class Assert
	{
		public static void NotNull(object obj, string objName)
		{
			if (obj == null)
			{
				Logger.LogCritical("Assert failed! Object {0} is null!", objName);
				throw new ArgumentNullException(objName);
			}
		}


		private static ILogger Logger => Log.LoggerFactory.CreateLogger("Assert");
	}
}