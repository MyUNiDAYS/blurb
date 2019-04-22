using System.Globalization;
using System.Linq;
using System.Text;
using Blurb.Core.Parsing;

namespace Blurb.Core.Generation.Javascript
{
	sealed class ComplexTermDefinitionJsGenerator : BaseTermJsGenerator<ComplexTermDefinition>
	{
		readonly CultureSettings settings;

		public ComplexTermDefinitionJsGenerator(CultureSettings settings)
		{
			this.settings = settings;
		}
		
		public override void Generate(StringBuilder builder, string fullClassName, ComplexTermDefinition definition, CultureInfo culture)
		{
			builder.AppendLine($@"{definition.Key} ({string.Join(", ", definition.AllParameters.Select(p => p.Name))}) {{
	switch ({definition.ComplexParameter.Name}) {{");

			foreach (var complexity in definition.Complexities)
			{
				var termValue = JsGenerationHelper.GetTerm(complexity.Value, culture).OriginalValue;
				// TODO: be cleverer, doesnt account for escaping
				termValue = termValue.Replace("{", "${");

				var key = complexity.Key.Substring(complexity.Key.LastIndexOf('.') + 1);
				builder.AppendLine($@"		case '{key.Replace("'", "\\'")}': return `{termValue.Replace("`", "\\`")}`;");
			}

			builder.AppendLine($@"
	}}

}}");
		}
	}
}