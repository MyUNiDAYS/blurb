using System;
using System.Globalization;

namespace Blurb.Core.Test.Generation.CSharp.Simple
{
    public class SimpleDynamicFormattedGeneration : CompiledSpec
	{
		public override void Assertions()
		{
			var staticTerm = this.GetParameterisedTerm("SimpleDynamicFormatted", new DateTime(2019, 4, 22, 12, 46, 30, DateTimeKind.Utc));

			staticTerm.Key.ShouldEqual("SimpleDynamicFormatted");
			staticTerm.ToString(new CultureInfo("en")).ShouldEqual("I am formatted 22/04/2019 and {escaped}");
		}
	}
}