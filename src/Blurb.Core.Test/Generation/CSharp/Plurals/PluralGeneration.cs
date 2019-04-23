using System;
using System.Globalization;

namespace Blurb.Core.Test.Generation.CSharp.Plurals
{
	public class PluralGeneration : CompiledSpec
	{
		public override void Assertions()
		{
			var termOne = GetParameterisedTerm("Plural", 1m, new DateTime(2019, 4, 22, 13, 33, 30, DateTimeKind.Utc));
			termOne.Key.ShouldEqual("Plural");
			termOne.ToString(new CultureInfo("en")).ShouldEqual("I will be 1 year old on 22/04/2019");

			var termMany = GetParameterisedTerm("Plural", 10m, new DateTime(2019, 4, 22, 13, 33, 30, DateTimeKind.Utc));
			termMany.Key.ShouldEqual("Plural");
			termMany.ToString(new CultureInfo("en")).ShouldEqual("I will be 10 years old on 22/04/2019");
		}
	}
}