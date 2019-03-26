
using System.Collections.Generic;
using Blurb.Core;

namespace Test
{
	public class TestTerms
	{
		static readonly Term _PluralDays_One = new StaticTerm("PluralGeneration.PluralGeneration", "PluralDays.One", new Dictionary<string, string> { { "en", @"{days} day" }, });
		static readonly Term _PluralDays_Many = new StaticTerm("PluralGeneration.PluralGeneration", "PluralDays.Many", new Dictionary<string, string> { { "en", @"{days} days" }, });

		public static Term PluralDays(long days)
		{
			if (days == 1)
				return new ParameterisedTerm(_PluralDays_One, days);

			return new ParameterisedTerm(_PluralDays_Many, days);
		}
	}
}