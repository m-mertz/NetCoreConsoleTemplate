using Test;
using Xunit;
using Assert = Xunit.Assert;

namespace UnitTests.Test
{
	public class SomeServiceUnitTests
	{
		[Fact]
		public void GetResult_ReturnsExpectedResult()
		{
			SomeService service = new SomeService();

			string result = service.GetResult();

			Assert.Equal("hello world!", result);
		}
	}
}