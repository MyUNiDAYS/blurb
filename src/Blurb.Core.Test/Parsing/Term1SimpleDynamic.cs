using System.Globalization;
using System.IO;
using System.Linq;
using Blurb.Core.Parsing;
using Xunit;

namespace Blurb.Core.Test.Parsing
{
	public class Term1SimpleDynamic
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
			
			// TERM 1

			var terms1 = termsArray[1] as SimpleTermDefinition;
			terms1.Key.ShouldEqual("SimpleDynamic");

			var term1En = terms1.Translations[new CultureInfo("en-US")];
			term1En.OriginalValue.ShouldEqual("I'm {age} and {{escaped}}");
			term1En.Value.ShouldEqual("I'm {0} and {{escaped}}");
			term1En.Parameters.Count.ShouldEqual(1);
			term1En.Parameters[0].Name.ShouldEqual("age");
			term1En.Parameters[0].Format.ShouldBeNull();
			term1En.Parameters[0].Type.ShouldEqual("object");

			var term1EnUs = terms1.Translations[new CultureInfo("en-US")];
			term1EnUs.OriginalValue.ShouldEqual("I'm {age} and {{escaped}}");
			term1EnUs.Value.ShouldEqual("I'm {0} and {{escaped}}");
			term1EnUs.Parameters.Count.ShouldEqual(1);
			term1EnUs.Parameters[0].Name.ShouldEqual("age");
			term1EnUs.Parameters[0].Format.ShouldBeNull();
			term1EnUs.Parameters[0].Type.ShouldEqual("object");

			var term1DeDe = terms1.Translations[new CultureInfo("de-DE")];
			term1DeDe.OriginalValue.ShouldEqual("Ich bin {age} und {{escaped}}");
			term1DeDe.Value.ShouldEqual("Ich bin {0} und {{escaped}}");
			term1DeDe.Parameters.Count.ShouldEqual(1);
			term1DeDe.Parameters[0].Name.ShouldEqual("age");
			term1DeDe.Parameters[0].Format.ShouldBeNull();
			term1DeDe.Parameters[0].Type.ShouldEqual("object");
		}
	}
}