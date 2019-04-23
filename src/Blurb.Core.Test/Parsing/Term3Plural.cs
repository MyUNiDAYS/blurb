using System.Globalization;
using System.Linq;
using Blurb.Core.Parsing;
using Xunit;

namespace Blurb.Core.Test.Parsing
{
	public class Term3Plural
	{
		[Fact]
		public void ShouldParseInputProperly()
		{
			TestTerms.Terms.Namspace.ShouldEqual("Blurb.Core.Test.Parsing");
			TestTerms.Terms.ClassName.ShouldEqual("Input");

			var term = TestTerms.Terms.Terms.Single(t => t.Key == "Plural") as PluralTermDefinition;
			
			term.PluralParameterName.ShouldEqual("years");
			term.Pluralities[Plurality.One].Translations[new CultureInfo("en")].OriginalValue.ShouldEqual("I will be {years} year old on {birthday:dd/MM/yyyy}");
			term.Pluralities[Plurality.Other].Translations[new CultureInfo("en")].OriginalValue.ShouldEqual("I will be {years} years old on {birthday:dd/MM/yyyy}");
		}
	}
}