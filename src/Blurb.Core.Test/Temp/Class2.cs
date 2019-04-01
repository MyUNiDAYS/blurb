using Xunit;

namespace Test
{
	public class a
	{
		[Fact]
		public void Lol()
		{
			var pluralDays = TestTerms.PluralDays(5);
			var s = pluralDays.ToString();
		}
	}
}