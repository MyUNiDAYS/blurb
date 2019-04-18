using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Blurb.Core.Parsing;

namespace Blurb.Core.Generation.Javascript
{
	static class JsGenerationHelper
	{
		public static void GenerateXmlDocComments(StringBuilder builder, CultureSettings settings, SimpleTermDefinition definition)
		{
//			var defaultCopy = definition.Translations[settings.DefaultCulture];
//			builder
//				.AppendLine("		/// <summary>")
//				.Append("		/// ").Append(settings.DefaultCulture.Name).Append(": \"").Append(defaultCopy.OriginalValue).AppendLine("\"")
//				.AppendLine("		/// </summary>");
		}

		public static void GenerateTermDeclaration_Property(StringBuilder builder, CultureSettings settings, string fullClassName, SimpleTermDefinition definition, string termKey, CultureInfo culture)
		{
			builder.AppendLine($@"
get {termKey}() {{
	return '{GetTerm(definition, culture).Value.Replace("'", "\\'")}';
}}");
			
		}

		public static void GenerateTermDeclaration_Method(StringBuilder builder, CultureSettings settings, string fullClassName, SimpleTermDefinition definition, CultureInfo culture)
		{
			//GenerateTermDeclaration_Property(builder, settings, fullClassName, definition, "_" + definition.Key, culture);

			//GenerateXmlDocComments(builder, settings, definition);

			var termValue = GetTerm(definition, culture).OriginalValue;

			// TODO: be cleverer, doesnt account for escaping
			termValue = termValue.Replace("{", "${");

			builder.AppendLine($@"{definition.Key} ({string.Join(", ", definition.AllParameters.Select(p => p.Name))}) {{
return `{termValue.Replace("`", "\\`")}`;
}}");
			
		}
		
		public static TermValue GetTerm(SimpleTermDefinition definition, CultureInfo culture)
		{
			if (definition.Translations.TryGetValue(culture, out var value) && value != null)
				return value;

			if (culture.CultureTypes.HasFlag(CultureTypes.UserCustomCulture))
			{
				culture = culture.Parent;
				if (definition.Translations.TryGetValue(culture, out value) && value != null)
					return value;
			}

			if (culture.CultureTypes.HasFlag(CultureTypes.SpecificCultures))
			{
				if (definition.Translations.TryGetValue(culture.Parent, out value) && value != null)
					return value;
			}

			return definition.Translations.TryGetValue(new CultureInfo("en"), out value) && value != null
				? value
				: null;
		}
	}
}