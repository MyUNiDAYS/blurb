using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Blurb.Core.Parsing;

namespace Blurb.Core.Generation.CSharp
{
	static class CSharpGenerationHelper
	{
		public static void GenerateTermDeclaration_Property(StringBuilder builder, IEnumerable<CultureInfo> supportedCultures, string fullClassName, SimpleTermDefinition definition, string termKey)
		{
			builder.Append($@"static readonly Term {termKey} = ");
			RenderTermCultureSwitch_CSharp(builder, supportedCultures, fullClassName, definition);
			builder.AppendLine(";");
		}

		public static void GenerateTermDeclaration_Method(StringBuilder builder, IEnumerable<CultureInfo> supportedCultures, string fullClassName, SimpleTermDefinition definition)
		{
			GenerateTermDeclaration_Property(builder, supportedCultures, fullClassName, definition, "_" + definition.Key);

			builder
				.AppendLine()
				.Append($@"public static Term ")
				.Append(definition.Key)
				.Append(" (");

			for (var i = 0; i < definition.AllParameters.Count; i++)
			{
				var parameter = definition.AllParameters[i];
				builder
					.Append(parameter.Type.Namespace)
					.Append('.')
					.Append(parameter.Type.Name)
					.Append(' ')
					.Append(parameter.Name);

				if (i < definition.AllParameters.Count - 1)
					builder.Append(", ");
			}

			builder
				.Append(")")
				.AppendLine("{")
				.Append("	return new ParameterisedTerm(this._")
				.Append(definition.Key)
				.Append(", ");

			for (var i = 0; i < definition.AllParameters.Count; i++)
			{
				builder.Append(definition.AllParameters[i].Name);

				if (i < definition.AllParameters.Count - 1)
					builder.Append(", ");
			}

			builder
				.AppendLine(");")
				.AppendLine("}");
		}

		public static void RenderTermCultureSwitch_CSharp(StringBuilder builder, IEnumerable<CultureInfo> supportedCultures, string fullClassName, SimpleTermDefinition definition)
		{
			builder.Append($@"new StaticTerm(""{fullClassName}"", ""{definition.Key}"", new Dictionary<string, string> {{ ");

			var cases = new List<string>();

			foreach (var culture in supportedCultures)
			{
				var termValue = GetTerm(definition, culture);

				var termText = termValue.Value;
				//
				//				// TODO: should this live in GetTerm()?
				//				if (!hasParameters)
				//					termValue = termValue.Replace("{{", "{").Replace("}}", "}");

				builder
					.Append("{ \"")
					.Append(culture.Name.ToLowerInvariant())
					.Append("\", @\"")
					.Append(termText.Replace("\"", "\"\""))
					.Append("\" }, ");
			}

			builder.Append("})");
		}

		public static TermValue GetTerm(SimpleTermDefinition definition, CultureInfo CultureInfo)
		{
			if (definition.Translations.TryGetValue(CultureInfo, out var value) && value != null)
				return value;

			if (CultureInfo.CultureTypes.HasFlag(CultureTypes.UserCustomCulture))
			{
				CultureInfo = CultureInfo.Parent;
				if (definition.Translations.TryGetValue(CultureInfo, out value) && value != null)
					return value;
			}

			if (CultureInfo.CultureTypes.HasFlag(CultureTypes.SpecificCultures))
			{
				if (definition.Translations.TryGetValue(CultureInfo.Parent, out value) && value != null)
					return value;
			}

			return definition.Translations.TryGetValue(new CultureInfo("en"), out value) && value != null
				? value
				: null;
		}
	}
}