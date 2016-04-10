using Xunit;

namespace Test.UnitTests
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