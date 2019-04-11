
using System.Collections.Generic;
using Blurb.Core;

namespace Test { 
	public class TestTerms { static readonly Term _PluralDays_Zero = new StaticTerm("PluralDays.One", new Dictionary<string, string> { { "en", @"{0} day" }, });
static readonly Term _PluralDays_One = new StaticTerm("PluralDays.One", new Dictionary<string, string> { { "en", @"{0} day" }, });
static readonly Term _PluralDays_Two = new StaticTerm("PluralDays.One", new Dictionary<string, string> { { "en", @"{0} day" }, });
static readonly Term _PluralDays_Few = new StaticTerm("PluralDays.One", new Dictionary<string, string> { { "en", @"{0} day" }, });
static readonly Term _PluralDays_Many = new StaticTerm("PluralDays.One", new Dictionary<string, string> { { "en", @"{0} day" }, });
static readonly Term _PluralDays_Other = new StaticTerm("PluralDays.Other", new Dictionary<string, string> { { "en", @"{0} days" }, });
		///  <summary>
		/// en: {days} days
		/// </summary>
		public static Term PluralDays(decimal days)
		{
			return new ParameterisedTerm(new DelegatedTerm("PluralDays", culture =>
			{
				var plurality = PluralRules.GetPlurality(culture, days);
				switch (plurality)
				{
					case Plurality.Zero:
						return _PluralDays_Zero;
					case Plurality.One:
						return _PluralDays_One;
					case Plurality.Two:
						return _PluralDays_Two;
					case Plurality.Few:
						return _PluralDays_Few;
					case Plurality.Many:
						return _PluralDays_Many;
					case Plurality.Other:
					default:
						return _PluralDays_Other;
				}
			}), days);
		}
} }