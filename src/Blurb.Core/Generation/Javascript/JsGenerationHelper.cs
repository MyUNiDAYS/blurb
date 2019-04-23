using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Blurb.Core.Parsing;

namespace Blurb.Core.Generation.Javascript
{
	static class JsGenerationHelper
	{

		static readonly Regex parameterRegex = new Regex(@"(?<!{){([^{}]+)}", RegexOptions.Compiled, TimeSpan.FromSeconds(1));
		static readonly Regex escapedRegex = new Regex(@"{{([^}]+)}}", RegexOptions.Compiled, TimeSpan.FromSeconds(1));


		public static void GenerateTermDeclaration_Property(StringBuilder builder, SimpleTermDefinition definition, string termKey, CultureInfo culture)
		{
			var termValue = GetTerm(definition, culture).JsTemplateValue;

			termValue = escapedRegex.Replace(termValue, "{$1}");

			builder.AppendLine($@"
get {termKey}() {{
	return `{termValue.Replace("`", "\\`")}`;
}}");
		}

		public static void GenerateTermDeclaration_Method(StringBuilder builder, SimpleTermDefinition definition, CultureInfo culture)
		{
			var termValue = GetTerm(definition, culture).JsTemplateValue;

			termValue = parameterRegex.Replace(termValue, "${$1}");
			termValue = escapedRegex.Replace(termValue, "{$1}");

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