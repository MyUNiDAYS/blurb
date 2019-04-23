using System.Globalization;

namespace Blurb.Core.Test.Generation.Javascript.Simple
{
	public class SimpleDynamicGeneration : JsExecutionSpec
	{
		public override CultureInfo Culture => new CultureInfo("en");

		public override void Assertions()
		{
			this.GetParameterisedTerm("SimpleDynamic", 21).ShouldEqual("I am 21 and {escaped}");
		}
	}
}