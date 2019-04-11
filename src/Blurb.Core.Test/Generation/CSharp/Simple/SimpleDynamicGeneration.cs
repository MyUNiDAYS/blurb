using System.Collections.Generic;
using System.Globalization;
using Blurb.Core.Generation.CSharp;
using Blurb.Core.Parsing;
using Xunit;

namespace Blurb.Core.Test.Generation.CSharp.Simple
{
	public class SimpleDynamicGeneration
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
						Key = "SimpleDynamic",
						Translations = new Dictionary<CultureInfo, TermValue>
						{
							{
								cultureEn, new TermValue
								{
									Value = "I am {age}",
									Parameters = new[] {new TermParameter {Name = "age", Type = "int"}}
								}
							}
						}
					}
				}
			};

			var supportedCultures = new[] { cultureEn };
			var cultureSettings = new CultureSettings { SupportedCultures = supportedCultures, DefaultCulture = cultureEn };
			var generator = new CSharpGenerator(new ITermCSharpGenerator[] { new SimpleTermDefinitionCSharpGenerator(cultureSettings) });
			var generate = generator.Generate(collection);
		}
	}
}