using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Blurb.Core.Parsing;

namespace Blurb.Core.Generation.CSharp
{
	sealed class PluralTermDefinitionCSharpGenerator : BaseTermCSharpGenerator<PluralTermDefinition>
	{
		readonly IEnumerable<CultureInfo> supportedCultures;

		public PluralTermDefinitionCSharpGenerator(IEnumerable<CultureInfo> supportedCultures)
		{
			this.supportedCultures = supportedCultures;
		}

		public override void Generate(StringBuilder builder, string fullClassName, PluralTermDefinition definition)
		{
			foreach (var plurality in definition.Pluralities.Keys)
				CSharpGenerationHelper.GenerateTermDeclaration_Property(builder, this.supportedCultures, fullClassName,
					definition.Pluralities[plurality], '_' + definition.Pluralities[plurality].Key.Replace('.', '_'));

			builder.Append($@"
		public static Term {definition.Key}(decimal {definition.PluralParameterName})
		{{
			if({definition.PluralParameterName} == 1)
				return new ParameterisedTerm(_{definition.Key}_One, {definition.PluralParameterName});

			return new ParameterisedTerm(_{definition.Key}_Many, {definition.PluralParameterName});
		}}");

		}
	}
}