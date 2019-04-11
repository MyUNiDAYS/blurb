using System.Globalization;
using System.IO;
using System.Linq;
using Blurb.Core.Parsing;
using Xunit;

namespace Blurb.Core.Test.Parsing
{
	public class InputParsingSpec
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

			// TERM 2

			var terms2 = termsArray[2] as SimpleTermDefinition;
			terms2.Key.ShouldEqual("SimpleDynamicFormatted");
			var term2En = terms2.Translations[new CultureInfo("en")];
			term2En.OriginalValue.ShouldEqual("My birthday is {birthday:dd/MM/yyyy}");
			term2En.Value.ShouldEqual("My birthday is {0:dd/MM/yyyy}");
			term2En.Parameters.Count.ShouldEqual(1);
			term2En.Parameters[0].Name.ShouldEqual("birthday");
			term2En.Parameters[0].Format.ShouldEqual("dd/MM/yyyy");
			term2En.Parameters[0].Type.ShouldEqual("object");
			terms2.Translations[new CultureInfo("en-US")].OriginalValue.ShouldEqual("My birthday is {birthday:MM/dd/yyyy}");
			terms2.Translations[new CultureInfo("en-US")].Value.ShouldEqual("My birthday is {0:MM/dd/yyyy}");
			terms2.Translations[new CultureInfo("de-DE")].OriginalValue.ShouldEqual("Mein Geburtstag ist {birthday:dd/MM/yyyy}");
			terms2.Translations[new CultureInfo("de-DE")].Value.ShouldEqual("Mein Geburtstag ist {0:dd/MM/yyyy}");
			
			// TERM 3

			var terms3 = termsArray[3] as SimpleTermDefinition;
			terms3.Key.ShouldEqual("SimpleDynamicFormattedWithType");
			var term3En = terms3.Translations[new CultureInfo("en")];
			term3En.OriginalValue.ShouldEqual("My birthday is {birthday#DateTime:dd/MM/yyyy}");
			term3En.Value.ShouldEqual("My birthday is {0:dd/MM/yyyy}");
			term3En.Parameters.Count.ShouldEqual(1);
			term3En.Parameters[0].Name.ShouldEqual("birthday");
			term3En.Parameters[0].Format.ShouldEqual("dd/MM/yyyy");
			term3En.Parameters[0].Type.ShouldEqual("DateTime");
			terms3.Translations[new CultureInfo("en-US")].OriginalValue.ShouldEqual("My birthday is {birthday#DateTime:MM/dd/yyyy}");
			terms3.Translations[new CultureInfo("en-US")].Value.ShouldEqual("My birthday is {0:MM/dd/yyyy}");
			terms3.Translations[new CultureInfo("de-DE")].OriginalValue.ShouldEqual("Mein Geburtstag ist {birthday#DateTime:dd/MM/yyyy}");
			terms3.Translations[new CultureInfo("de-DE")].Value.ShouldEqual("Mein Geburtstag ist {0:dd/MM/yyyy}");
		}
	}
}