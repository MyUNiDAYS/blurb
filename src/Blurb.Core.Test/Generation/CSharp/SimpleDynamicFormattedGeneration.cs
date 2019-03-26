using System.Collections.Generic;
using System.Globalization;
using Blurb.Core.Generation.CSharp;
using Blurb.Core.Parsing;
using Xunit;

namespace Blurb.Core.Test.Generation.CSharp
{
	public class SimpleDynamicFormattedGeneration
	{
		[Fact]
		public void ShouldGenerateCorrectCSharp()
		{
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
								new CultureInfo("en"), new TermValue
								{
									Value = "My birthday is {birthday:dd/MM/yyyy}",
									Parameters = new[] {new TermParameter {Name = "age", Type = typeof(int)}}
								}
							}
						}
					}
				}
			};

			var generator = new CSharpGenerator(new ITermCSharpGenerator[] { new SimpleTermDefinitionCSharpGenerator(new[] { new CultureInfo("en") }) });
			var generate = generator.Generate(collection);
		}
	}
}