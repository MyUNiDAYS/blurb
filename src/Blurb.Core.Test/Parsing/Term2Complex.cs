using System.Globalization;
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
			TestTerms.Terms.Namspace.ShouldEqual("Blurb.Core.Test.Parsing");
			TestTerms.Terms.ClassName.ShouldEqual("Input");
			
			var term = TestTerms.Terms.Terms.Single(t => t.Key == "ContentDependent") as ComplexTermDefinition;
			
			term.ComplexParameter.Name.ShouldEqual("aBMode");
			term.ComplexParameter.Type.ShouldEqual("Blurb.Core.Test.ABMode");
			term.ComplexParameter.Format.ShouldBeNull();
			term.ComplexParameter.Index.ShouldEqual(0);

			term.Complexities["Blurb.Core.Test.ABMode.A"].Translations[new CultureInfo("en")].OriginalValue.ShouldEqual("This is A content {arg}");
			term.Complexities["Blurb.Core.Test.ABMode.B"].Translations[new CultureInfo("en")].OriginalValue.ShouldEqual("This is B content {arg}");
		}
	}
}