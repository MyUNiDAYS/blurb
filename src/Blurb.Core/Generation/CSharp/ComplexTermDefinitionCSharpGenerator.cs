using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Blurb.Core.Parsing;

namespace Blurb.Core.Generation.CSharp
{
	sealed class ComplexTermDefinitionCSharpGenerator : BaseTermCSharpGenerator<ComplexTermDefinition>
	{
		readonly IEnumerable<CultureInfo> supportedCultures;

		public ComplexTermDefinitionCSharpGenerator(IEnumerable<CultureInfo> supportedCultures)
		{
			this.supportedCultures = supportedCultures;
		}
		
		public override void Generate(StringBuilder builder, string fullClassName, ComplexTermDefinition definition)
		{
			foreach (var complexity in definition.Complexities)
				CSharpGenerationHelper.GenerateTermDeclaration_Property(builder, this.supportedCultures, fullClassName, complexity.Value, '_' + definition.Key + '_' + complexity.Key.Replace('.', '_'));

			builder.AppendLine($@"
		public static Term {definition.Key}({string.Join(", ", definition.AllParameters.Select(p => p.Type.Namespace + '.' + p.Type.Name + ' ' + p.Name))})
		{{
			return new ParameterisedTerm(new DelegatedTerm(""{fullClassName}"", ""{definition.Key}"", culture =>
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
			}}), {string.Join(", ", definition.AllParameters.Select(p => p.Name))});
		}}");
		}
	}
}