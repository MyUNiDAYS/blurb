using System.Globalization;
using Blurb.Core.Parsing;

namespace Blurb.Core.Test.Generation.CSharp.Complex
{
	public class ComplexGeneration : CompiledSpec
	{
		public override TermCollection Collection => TestTerms.ComplexAB;

		public override void Assertions()
		{
			var termOne = GetParameterisedTerm("ABTest", ABMode.A, 5m);
			termOne.Key.ShouldEqual("ABTest");
			termOne.ToString(new CultureInfo("en")).ShouldEqual("Copy A 5");

			var termMany = GetParameterisedTerm("ABTest", ABMode.B, 6m);
			termMany.Key.ShouldEqual("ABTest");
			termMany.ToString(new CultureInfo("en")).ShouldEqual("Copy B 6");
		}
	}
}