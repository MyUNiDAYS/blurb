using Blurb.Core.Parsing;
using Xunit;

namespace Blurb.Core.Test.Parsing
{
	public class ValueParserSpecs
	{
		[Fact]
		public void ShouldParseSimpleStatic()
		{
			var termValue = ValueParser.Parse("I am simple");
			termValue.Value.ShouldEqual("I am simple");
			termValue.Parameters.Count.ShouldEqual(0);
		}

		[Fact]
		public void ShouldParseSimpleDynamic()
		{
			var termValue = ValueParser.Parse("I am {age}");
			termValue.Value.ShouldEqual("I am {0}");
			termValue.Parameters.Count.ShouldEqual(1);
			termValue.Parameters[0].Name.ShouldEqual("age");
			termValue.Parameters[0].Type.ShouldEqual(typeof(object));
			termValue.Parameters[0].Index.ShouldEqual(0);
			termValue.Parameters[0].Format.ShouldBeNull();
		}

		[Fact]
		public void ShouldParseSimpleDynamicFormatted()
		{
			var termValue = ValueParser.Parse("My birthday is {birthday:dd/MM/yyyy}");
			termValue.Value.ShouldEqual("My birthday is {0:dd/MM/yyyy}");
			termValue.Parameters.Count.ShouldEqual(1);
			termValue.Parameters[0].Name.ShouldEqual("birthday");
			termValue.Parameters[0].Type.ShouldEqual(typeof(object));
			termValue.Parameters[0].Index.ShouldEqual(0);
			termValue.Parameters[0].Format.ShouldEqual("dd/MM/yyyy");
		}
	}
}