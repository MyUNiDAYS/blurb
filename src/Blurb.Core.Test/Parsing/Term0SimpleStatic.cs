using System.Globalization;
using System.Linq;
using Blurb.Core.Parsing;
using Xunit;

namespace Blurb.Core.Test.Parsing
{
	public class Term0SimpleStatic
	{
		[Fact]
		public void ShouldParseInputProperly()
		{
			TestTerms.Terms.Namspace.ShouldEqual("Blurb.Core.Test.Parsing");
			TestTerms.Terms.ClassName.ShouldEqual("Input");

			var termsArray = TestTerms.Terms.Terms.ToArray();

			// TERM 0

			var terms0 = termsArray[0] as SimpleTermDefinition;
			terms0.Key.ShouldEqual("SimpleStatic");
			terms0.Translations[new CultureInfo("en")].OriginalValue.ShouldEqual("I am simple and {{escaped}}");
			terms0.Translations[new CultureInfo("en")].CSharpStringFormatValue.ShouldEqual("I am simple and {{escaped}}");
			terms0.Translations[new CultureInfo("en-US")].OriginalValue.ShouldEqual("I'm simple and {{escaped}}");
			terms0.Translations[new CultureInfo("en-US")].CSharpStringFormatValue.ShouldEqual("I'm simple and {{escaped}}");
			terms0.Translations[new CultureInfo("de-DE")].OriginalValue.ShouldEqual("Ich bin einfach und {{escaped}}");
			terms0.Translations[new CultureInfo("de-DE")].CSharpStringFormatValue.ShouldEqual("Ich bin einfach und {{escaped}}");
		}
	}
}