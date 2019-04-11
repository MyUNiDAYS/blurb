using System.Globalization;
using System.IO;
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
			var stream = this.GetType().Assembly.GetManifestResourceStream("Blurb.Core.Test.Parsing.input.json");
			TermCollection termCollection;
			using (var reader = new StreamReader(stream))
			{
				var json = reader.ReadToEnd();
				var parser = new Parser();
				termCollection = parser.Parse(json);
			}

			termCollection.Namspace.ShouldEqual("Blurb.Core.Test.Parsing");
			termCollection.ClassName.ShouldEqual("Input");

			var termsArray = termCollection.Terms.ToArray();

			// TERM 0

			var terms0 = termsArray[0] as SimpleTermDefinition;
			terms0.Key.ShouldEqual("SimpleStatic");
			terms0.Translations[new CultureInfo("en")].OriginalValue.ShouldEqual("I am simple and {{escaped}}");
			terms0.Translations[new CultureInfo("en")].Value.ShouldEqual("I am simple and {{escaped}}");
			terms0.Translations[new CultureInfo("en-US")].OriginalValue.ShouldEqual("I'm simple and {{escaped}}");
			terms0.Translations[new CultureInfo("en-US")].Value.ShouldEqual("I'm simple and {{escaped}}");
			terms0.Translations[new CultureInfo("de-DE")].OriginalValue.ShouldEqual("Ich bin einfach und {{escaped}}");
			terms0.Translations[new CultureInfo("de-DE")].Value.ShouldEqual("Ich bin einfach und {{escaped}}");
		}
	}
}