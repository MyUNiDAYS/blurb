using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Blurb.Core.Parsing;

namespace Blurb.Core.Generation.CSharp
{
	static class CSharpGenerationHelper
	{
		public static void GenerateXmlDocComments(StringBuilder builder, CultureSettings settings, SimpleTermDefinition definition)
		{
			var defaultCopy = definition.Translations[settings.DefaultCulture];
			builder
				.AppendLine("		/// <summary>")
				.Append("		/// ").Append(settings.DefaultCulture.Name).Append(": ").AppendLine(defaultCopy.OriginalValue)
				.AppendLine("		/// </summary>");
		}

		public static void GenerateTermDeclaration_Property(StringBuilder builder, CultureSettings settings, string fullClassName, SimpleTermDefinition definition, string termKey, bool @public = false)
		{
			builder.Append("		");

			if (@public)
			{
				GenerateXmlDocComments(builder, settings, definition);
				builder.Append("public ");
			}

			builder.Append($@"static readonly Term {termKey} = ");
			RenderTermCultureSwitch_CSharp(builder, settings, fullClassName, definition);
			builder.AppendLine(";");
		}

		public static void GenerateTermDeclaration_Method(StringBuilder builder, CultureSettings settings, string fullClassName, SimpleTermDefinition definition)
		{
			GenerateTermDeclaration_Property(builder, settings, fullClassName, definition, "_" + definition.Key);

			GenerateXmlDocComments(builder, settings, definition);

			builder
				.Append($@"		public static Term ")
				.Append(definition.Key)
				.Append(" (");

			for (var i = 0; i < definition.AllParameters.Count; i++)
			{
				var parameter = definition.AllParameters[i];
				builder
					.Append(parameter.Type)
					.Append(' ')
					.Append(parameter.Name);

				if (i < definition.AllParameters.Count - 1)
					builder.Append(", ");
			}

			builder
				.AppendLine(")")
				.AppendLine("		{")
				.Append("			return new ParameterisedTerm(this._")
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
				.AppendLine("		}");
		}

		public static void RenderTermCultureSwitch_CSharp(StringBuilder builder, CultureSettings settings, string fullClassName, SimpleTermDefinition definition)
		{
			builder.Append($@"new StaticTerm(""{definition.Key}"", new Dictionary<string, string> {{ ");

			var cases = new List<string>();

			for (var i = 0; i < settings.SupportedCultures.Count; i++)
			{
				var culture = settings.SupportedCultures[i];

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
					.Append("\" } ");

				if (i < settings.SupportedCultures.Count - 1)
					builder.Append(", ");
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