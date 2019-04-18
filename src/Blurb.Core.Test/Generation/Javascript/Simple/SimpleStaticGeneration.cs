using System.Globalization;
using Blurb.Core.Parsing;

namespace Blurb.Core.Test.Generation.Javascript.Simple
{
	public class SimpleStaticGeneration : JsExecutionSpec
	{
		public override TermCollection Collection => TestTerms.SimpleStatic;
		public override CultureInfo Culture => new CultureInfo("de");

		public override void Assertions()
		{
			var staticTerm = this.GetStaticTerm("SimpleStatic");

			staticTerm.ShouldEqual("Ich bin einfach");
		}
	}
}