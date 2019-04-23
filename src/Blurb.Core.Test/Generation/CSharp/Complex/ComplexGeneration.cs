using System.Globalization;

namespace Blurb.Core.Test.Generation.CSharp.Complex
{
	public class ComplexGeneration : CompiledSpec
	{
		public override void Assertions()
		{
			var termOne = GetParameterisedTerm("ContentDependent", ABMode.A, 5m);
			termOne.Key.ShouldEqual("ContentDependent");
			termOne.ToString(new CultureInfo("en")).ShouldEqual("This is A content 5");

			var termMany = GetParameterisedTerm("ContentDependent", ABMode.B, 6m);
			termMany.Key.ShouldEqual("ContentDependent");
			termMany.ToString(new CultureInfo("en")).ShouldEqual("This is B content 6");
		}
	}
}