using System.Globalization;
using Blurb.Core.Parsing;

namespace Blurb.Core.Test.Generation.Javascript.Complex
{
	public class ComplexABTestGeneration : JsExecutionSpec
	{
		public override CultureInfo Culture => new CultureInfo("en");

		public override void Assertions()
		{
			var staticTerm = this.GetParameterisedTerm("ContentDependent", "A", 5);

			staticTerm.ShouldEqual("This is A content 5");
		}
	}
}