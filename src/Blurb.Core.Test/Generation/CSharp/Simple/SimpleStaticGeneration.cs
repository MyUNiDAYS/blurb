using System.Globalization;

namespace Blurb.Core.Test.Generation.CSharp.Simple
{
	public class SimpleStaticGeneration : CompiledSpec
	{
		public override void Assertions()
		{
			var staticTerm = this.GetStaticTerm("SimpleStatic");

			staticTerm.Key.ShouldEqual("SimpleStatic");
			staticTerm.ToString(new CultureInfo("en")).ShouldEqual("I am simple and {escaped}");
		}
	}
}