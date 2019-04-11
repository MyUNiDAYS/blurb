using System.Linq;
using System.Text;
using Blurb.Core.Parsing;

namespace Blurb.Core.Generation.CSharp
{
	sealed class ComplexTermDefinitionCSharpGenerator : BaseTermCSharpGenerator<ComplexTermDefinition>
	{
		readonly CultureSettings settings;

		public ComplexTermDefinitionCSharpGenerator(CultureSettings settings)
		{
			this.settings = settings;
		}
		
		public override void Generate(StringBuilder builder, string fullClassName, ComplexTermDefinition definition)
		{
			foreach (var complexity in definition.Complexities)
				CSharpGenerationHelper.GenerateTermDeclaration_Property(builder, this.settings, fullClassName, complexity.Value, '_' + definition.Key + '_' + complexity.Key.Replace('.', '_'));

			builder.AppendLine().AppendLine("		/// <summary>");
			foreach (var complexity in definition.Complexities)
			{
				var defaultCopy = complexity.Value.Translations[this.settings.DefaultCulture];
				builder.Append("		/// ").Append(this.settings.DefaultCulture.Name).Append(", ").Append(complexity.Key).Append(": \"").Append(defaultCopy.OriginalValue).AppendLine("\"");
			}
			builder.AppendLine("		/// </summary>");

			builder.AppendLine($@"		public static Term {definition.Key}({string.Join(", ", definition.AllParameters.Select(p => p.Type + ' ' + p.Name))})
		{{
			return new ParameterisedTerm(new DelegatedTerm(""{definition.Key}"", culture =>
			{{
				switch ({definition.ComplexParameter.Name})
				{{");


			foreach (var complexity in definition.Complexities)
			{
				builder.AppendLine($@"
					case {complexity.Key}:
						return _{definition.Key}_{complexity.Key.Replace('.', '_')};");
			}
			builder.AppendLine($@"
				}}

				return new EmptyTerm();
			}}), {string.Join(", ", definition.AllParameters.Skip(1).Select(p => p.Name))});
		}}");
		}
	}
}