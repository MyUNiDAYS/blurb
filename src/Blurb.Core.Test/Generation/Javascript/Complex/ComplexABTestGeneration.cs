using System.Globalization;
using Blurb.Core.Parsing;

namespace Blurb.Core.Test.Generation.Javascript.Complex
{
	public class ComplexABTestGeneration : JsExecutionSpec
	{
		public override TermCollection Collection => TestTerms.ComplexAB;
		public override CultureInfo Culture => new CultureInfo("en");

		public override void Assertions()
		{
			var staticTerm = this.GetParameterisedTerm("ABTest", "A", 5);

			staticTerm.ShouldEqual("Copy A 5");
		}
	}
}