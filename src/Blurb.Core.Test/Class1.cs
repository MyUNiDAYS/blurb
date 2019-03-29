
using System.Collections.Generic;
using Blurb.Core;

namespace Test
{
	public class TestTerms
	{
		static readonly Term _ABTest_Blurb_Core_Test_Generation_CSharp_Plurals_ABMode_A = new StaticTerm("ComplexGeneration.ComplexGeneration", "ABTest.A", new Dictionary<string, string> { { "en", @"Copy A {0}" }, });
		static readonly Term _ABTest_Blurb_Core_Test_Generation_CSharp_Plurals_ABMode_B = new StaticTerm("ComplexGeneration.ComplexGeneration", "ABTest.B", new Dictionary<string, string> { { "en", @"Copy B {0}" }, });

		public static Term ABTest(Blurb.Core.Test.Generation.CSharp.Plurals.ABMode ab, System.Decimal days)
		{
			return new ParameterisedTerm(new DelegatedTerm("ComplexGeneration.ComplexGeneration", "ABTest", culture =>
			{
				switch (ab)
				{

					case Blurb.Core.Test.Generation.CSharp.Plurals.ABMode.A:
						return _ABTest_Blurb_Core_Test_Generation_CSharp_Plurals_ABMode_A;

					case Blurb.Core.Test.Generation.CSharp.Plurals.ABMode.B:
						return _ABTest_Blurb_Core_Test_Generation_CSharp_Plurals_ABMode_B;

				}

				return new EmptyTerm();
			}), ab, days);
		}
	}
}