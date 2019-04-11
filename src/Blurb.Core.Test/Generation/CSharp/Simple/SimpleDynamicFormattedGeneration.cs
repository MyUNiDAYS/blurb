using System.Collections.Generic;
using System.Globalization;
using Blurb.Core.Generation.CSharp;
using Blurb.Core.Parsing;
using Xunit;

namespace Blurb.Core.Test.Generation.CSharp.Simple
{
    public class SimpleDynamicFormattedGeneration
	{
		[Fact]
		public void ShouldGenerateCorrectCSharp()
		{
			var cultureEn = new CultureInfo("en");
			var collection = new TermCollection
			{
				Namspace = this.GetType().Namespace,
				ClassName = this.GetType().Name,
				Terms = new[]
				{
					new SimpleTermDefinition
					{
						Key = "SimpleDynamicFormatted",
						Translations = new Dictionary<CultureInfo, TermValue>
						{
							{
								cultureEn, new TermValue
								{
									OriginalValue = "My birthday is {birthday:dd/MM/yyyy}",
									Value = "My birthday is {0:dd/MM/yyyy}",
									Parameters = new[] {new TermParameter {Name = "birthday", Type = "DateTime"}}
								}
							}
						}
					}
				}
			};

			var supportedCultures = new[] { cultureEn };
			var cultureSettings = new CultureSettings{ SupportedCultures = supportedCultures, DefaultCulture = cultureEn };
			var generator = new CSharpGenerator(new ITermCSharpGenerator[] { new SimpleTermDefinitionCSharpGenerator(cultureSettings) });
			var generated = generator.Generate(collection);

			generated.ShouldEqual(@"		static readonly Term _SimpleDynamicFormatted = new StaticTerm(""SimpleDynamicFormatted"", new Dictionary<string, string> { { ""en"", @""My birthday is {0:dd/MM/yyyy}"" } });
		/// <summary>
		/// en: My birthday is {birthday:dd/MM/yyyy}
		/// </summary>
		public static Term SimpleDynamicFormatted (DateTime birthday)
		{
			return new ParameterisedTerm(this._SimpleDynamicFormatted, birthday);
		}
");
		}
	}
}