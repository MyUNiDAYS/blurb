//using System.Collections.Generic;
//using Blurb.Core;
//
//namespace Test
//{
//	public class TestTerms
//	{
//		static readonly Term _PluralDays_Plurality_One = new StaticTerm("ComplexGeneration.ComplexGeneration", "PluralDays.One", new Dictionary<string, string> {{"en", @"{0} day"},});
//		static readonly Term _PluralDays_Plurality_Many = new StaticTerm("ComplexGeneration.ComplexGeneration", "PluralDays.Many", new Dictionary<string, string> {{"en", @"{0} days"},});
//
//		public static Term PluralDays(Blurb.Core.Plurality lol, System.Decimal days)
//		{
//			return new ParameterisedTerm(new DelegatedTerm("ComplexGeneration.ComplexGeneration", "PluralDays", culture =>
//			{
//				switch (lol)
//				{
//					case Plurality.One:
//						return _PluralDays_Plurality_One;
//
//					case Plurality.Many:
//						return _PluralDays_Plurality_Many;
//				}
//			}), lol, days);
//		}
//	}
//}