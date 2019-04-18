using System.Globalization;
using System.IO;
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
			
			// TERM 3

			var term3 = termsArray[3] as PluralTermDefinition;
			term3.Key.ShouldEqual("Plural");
			
			term3.PluralParameterName.ShouldEqual("years");
			term3.Pluralities[Plurality.One].Translations[new CultureInfo("en")].OriginalValue.ShouldEqual("I will be {years} year old on {birthday:dd/MM/yyyy}");
			term3.Pluralities[Plurality.Other].Translations[new CultureInfo("en")].OriginalValue.ShouldEqual("I will be {years} years old on {birthday:dd/MM/yyyy}");
		}
	}
}