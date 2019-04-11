using System.Globalization;
using System.IO;
using System.Linq;
using Blurb.Core.Parsing;
using Xunit;

namespace Blurb.Core.Test.Parsing
{
	public class Term2Complex
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

			var term2 = termsArray[2] as ComplexTermDefinition;
			term2.Key.ShouldEqual("ContentDependent");
			
			term2.ComplexParameter.Name.ShouldEqual("anEnum");
			term2.ComplexParameter.Type.ShouldEqual("AnEnum");
			term2.ComplexParameter.Format.ShouldBeNull();
			term2.ComplexParameter.Index.ShouldEqual(0);

			term2.Complexities["AnEnum.Foo"].Translations[new CultureInfo("en")].OriginalValue.ShouldEqual("This is Foo content");
			term2.Complexities["AnEnum.Bar"].Translations[new CultureInfo("en")].OriginalValue.ShouldEqual("This is Bar content");
		}
	}
}