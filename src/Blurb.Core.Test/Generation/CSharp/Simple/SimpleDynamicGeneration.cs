using System.Collections.Generic;
using System.Globalization;
using Blurb.Core.Generation.CSharp;
using Xunit;

namespace Blurb.Core.Test.Generation.CSharp.Simple
{
	public class SimpleDynamicGeneration
	{
		[Fact]
		public void ShouldGenerateCorrectCSharp()
		{
			var cultureEn = new CultureInfo("en");
			var collection = TestTerms.SimpleDynamic;

			var supportedCultures = new[] { cultureEn };
			var cultureSettings = new CultureSettings { SupportedCultures = supportedCultures, DefaultCulture = cultureEn };
			var generator = new CSharpGenerator(new ITermCSharpGenerator[] { new SimpleTermDefinitionCSharpGenerator(cultureSettings) });
			var generate = generator.Generate(collection);
		}
	}
}