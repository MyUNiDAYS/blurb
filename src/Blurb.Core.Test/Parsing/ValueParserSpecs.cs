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
			termValue.CSharpStringFormatValue.ShouldEqual("I am simple");
			termValue.Parameters.Count.ShouldEqual(0);
		}

		[Fact]
		public void ShouldParseSimpleDynamic()
		{
			var termValue = ValueParser.Parse("I am {age}");
			termValue.CSharpStringFormatValue.ShouldEqual("I am {0}");
			termValue.Parameters.Count.ShouldEqual(1);
			termValue.Parameters[0].Name.ShouldEqual("age");
			termValue.Parameters[0].Type.ShouldEqual("object");
			termValue.Parameters[0].Index.ShouldEqual(0);
			termValue.Parameters[0].Format.ShouldBeNull();
		}
		[Fact]
		public void ShouldParseSimpleDynamicWithType()
		{
			var termValue = ValueParser.Parse("I am {age#int}");
			termValue.CSharpStringFormatValue.ShouldEqual("I am {0}");
			termValue.Parameters.Count.ShouldEqual(1);
			termValue.Parameters[0].Name.ShouldEqual("age");
			termValue.Parameters[0].Type.ShouldEqual("int");
			termValue.Parameters[0].Index.ShouldEqual(0);
			termValue.Parameters[0].Format.ShouldBeNull();
		}

		[Fact]
		public void ShouldParseSimpleDynamicFormatted()
		{
			var termValue = ValueParser.Parse("My birthday is {birthday:dd/MM/yyyy}");
			termValue.CSharpStringFormatValue.ShouldEqual("My birthday is {0:dd/MM/yyyy}");
			termValue.Parameters.Count.ShouldEqual(1);
			termValue.Parameters[0].Name.ShouldEqual("birthday");
			termValue.Parameters[0].Type.ShouldEqual("object");
			termValue.Parameters[0].Index.ShouldEqual(0);
			termValue.Parameters[0].Format.ShouldEqual("dd/MM/yyyy");
		}

		[Fact]
		public void ShouldParseSimpleDynamicFormattedWithType()
		{
			var termValue = ValueParser.Parse("My birthday is {birthday#DateTime:dd/MM/yyyy}");
			termValue.CSharpStringFormatValue.ShouldEqual("My birthday is {0:dd/MM/yyyy}");
			termValue.Parameters.Count.ShouldEqual(1);
			termValue.Parameters[0].Name.ShouldEqual("birthday");
			termValue.Parameters[0].Type.ShouldEqual("DateTime");
			termValue.Parameters[0].Index.ShouldEqual(0);
			termValue.Parameters[0].Format.ShouldEqual("dd/MM/yyyy");
		}
		[Fact]
		public void ShouldParseSimpleDynamicTypeWithFormatting()
		{
			var termValue = ValueParser.Parse("My birthday is {birthday:dd/MM/yyyy#DateTime}");
			termValue.CSharpStringFormatValue.ShouldEqual("My birthday is {0:dd/MM/yyyy}");
			termValue.Parameters.Count.ShouldEqual(1);
			termValue.Parameters[0].Name.ShouldEqual("birthday");
			termValue.Parameters[0].Type.ShouldEqual("DateTime");
			termValue.Parameters[0].Index.ShouldEqual(0);
			termValue.Parameters[0].Format.ShouldEqual("dd/MM/yyyy");
		}
	}
}