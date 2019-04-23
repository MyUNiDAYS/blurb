using System;
using System.Globalization;

namespace Blurb.Core.Test.Generation.CSharp.Simple
{
	public class SimpleDynamicGeneration : CompiledSpec
	{

		public override void Assertions()
		{
			var staticTerm = this.GetParameterisedTerm("SimpleDynamic", 21);

			staticTerm.Key.ShouldEqual("SimpleDynamic");
			staticTerm.ToString(new CultureInfo("en")).ShouldEqual("I am 21 and {escaped}");
		}
	}
}