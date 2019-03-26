using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Blurb.Core.Parsing;

namespace Blurb.Core.Generation.CSharp
{
	sealed class SimpleTermDefinitionCSharpGenerator : BaseTermCSharpGenerator<SimpleTermDefinition>
	{
		readonly IEnumerable<CultureInfo> supportedCultures;

		public SimpleTermDefinitionCSharpGenerator(IEnumerable<CultureInfo> supportedCultures)
		{
			this.supportedCultures = supportedCultures;
		}

		public override void Generate(StringBuilder builder, string fullClassName, SimpleTermDefinition definition)
		{
			if (definition.AllParameters.Any())
				CSharpGenerationHelper.GenerateTermDeclaration_Method(builder, this.supportedCultures, fullClassName, definition);
			else
			{
				builder.Append("public ");
				CSharpGenerationHelper.GenerateTermDeclaration_Property(builder, this.supportedCultures, fullClassName, definition, definition.Key);
			}
		}

	}
}