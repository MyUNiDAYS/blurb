using System.Globalization;

namespace Blurb.Core.Test.Generation.Javascript.Simple
{
	public class SimpleStaticGeneration : JsExecutionSpec
	{
		public override CultureInfo Culture => new CultureInfo("de-DE");

		public override void Assertions()
		{
			var staticTerm = this.GetStaticTerm("SimpleStatic");

			staticTerm.ShouldEqual("Ich bin einfach und {escaped}");
		}
	}
}