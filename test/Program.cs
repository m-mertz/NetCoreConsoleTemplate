using System;
using System.Threading;
using Autofac;
using Microsoft.Extensions.Logging;

namespace Test
{
	public class Program
	{
		public static void Main(string[] args)
		{
			using (ILifetimeScope scope = Container.BeginLifetimeScope())
			{
				Main main = scope.Resolve<Main>();
				Assert.NotNull(main, nameof(main));
				main.Run();
			}

			Console.ReadLine();
		}


		private static IContainer Container => s_container.Value;


		private static readonly Lazy<IContainer> s_container = new Lazy<IContainer>(() =>
		{
			ContainerBuilder builder = new ContainerBuilder();

			builder.RegisterType<SomeService>().As<ISomeService>();
			builder.RegisterType<ConsoleOutput>().As<IOutput>();
			builder.RegisterType<Main>();

			return builder.Build();
		}, LazyThreadSafetyMode.ExecutionAndPublication);
	}


	/// <summary>
	/// This class represents the entry point into the actual functionality of the console
	/// application. It will be instantiated with DI to enable automatic injection into this and all
	/// referenced classes.
	/// </summary>
	public class Main
	{
		public Main(ISomeService someService, IOutput output)
		{
			Assert.NotNull(someService, nameof(someService));
			Assert.NotNull(output, nameof(output));
			m_someService = someService;
			m_output = output;
		}


		public void Run()
		{
			Log.LoggerFactory.CreateLogger<Main>().LogVerbose("Starting {0}", nameof(Run));
			m_output.WriteLine(m_someService.GetResult());
		}


		private readonly ISomeService m_someService;


		private readonly IOutput m_output;
	}


	// The following are all dummy interfaces and implementations to demonstrate DI functionality,
	// can be removed when populating this template with actual functionality.

	public interface IOutput
	{
		void WriteLine(string text);
	}


	public class ConsoleOutput : IOutput
	{
		public void WriteLine(string text)
		{
			Console.WriteLine(text);
		}
	}


	public interface ISomeService
	{
		string GetResult();
	}


	public class SomeService : ISomeService
	{
		public string GetResult()
		{
			return "hello world!";
		}
	}
}