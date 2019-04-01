using System.Collections.Generic;
using Blurb.Core.Test.Generation.CSharp.Complex;

namespace Blurb.Core.Test
{
	public class TestTerms
	{
		static readonly Term _ABTest_Blurb_Core_Test_Generation_CSharp_Plurals_ABMode_A = new StaticTerm("ABTest.A", new Dictionary<string, string> { { "en", @"Copy A {0}" }, });
		static readonly Term _ABTest_Blurb_Core_Test_Generation_CSharp_Plurals_ABMode_B = new StaticTerm("ABTest.B", new Dictionary<string, string> { { "en", @"Copy B {0}" }, });

		public static Term ABTest(ABMode ab, System.Decimal days)
		{
			return new ParameterisedTerm(new DelegatedTerm("ABTest", culture =>
			{
				switch (ab)
				{

					case ABMode.A:
						return _ABTest_Blurb_Core_Test_Generation_CSharp_Plurals_ABMode_A;

					case ABMode.B:
						return _ABTest_Blurb_Core_Test_Generation_CSharp_Plurals_ABMode_B;

				}

				return new EmptyTerm();
			}), ab, days);
		}
	}
}