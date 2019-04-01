using System.Linq;
using System.Text;
using Blurb.Core.Parsing;

namespace Blurb.Core.Generation.CSharp
{
	sealed class SimpleTermDefinitionCSharpGenerator : BaseTermCSharpGenerator<SimpleTermDefinition>
	{
		readonly CultureSettings settings;

		public SimpleTermDefinitionCSharpGenerator(CultureSettings settings)
		{
			this.settings = settings;
		}

		public override void Generate(StringBuilder builder, string fullClassName, SimpleTermDefinition definition)
		{
			if (definition.AllParameters.Any())
				CSharpGenerationHelper.GenerateTermDeclaration_Method(builder, this.settings, fullClassName, definition);
			else
			{
				CSharpGenerationHelper.GenerateTermDeclaration_Property(builder, this.settings, fullClassName, definition, definition.Key, @public: true);
			}
		}

	}
}