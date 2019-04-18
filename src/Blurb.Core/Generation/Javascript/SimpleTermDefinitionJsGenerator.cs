using System.Globalization;
using System.Linq;
using System.Text;
using Blurb.Core.Parsing;

namespace Blurb.Core.Generation.Javascript
{
	sealed class SimpleTermDefinitionJsGenerator : BaseTermJsGenerator<SimpleTermDefinition>
	{
		readonly CultureSettings settings;

		public SimpleTermDefinitionJsGenerator(CultureSettings settings)
		{
			this.settings = settings;
		}

		public override void Generate(StringBuilder builder, string fullClassName, SimpleTermDefinition definition, CultureInfo culture)
		{
			if (definition.AllParameters.Any())
				JsGenerationHelper.GenerateTermDeclaration_Method(builder, this.settings, fullClassName, definition, culture);
			else
				JsGenerationHelper.GenerateTermDeclaration_Property(builder, this.settings, fullClassName, definition, definition.Key, culture);
			
		}

	}
}