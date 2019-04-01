using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Blurb.Core.Parsing;

namespace Blurb.Core.Generation.CSharp
{
	sealed class PluralTermDefinitionCSharpGenerator : BaseTermCSharpGenerator<PluralTermDefinition>
	{
		readonly CultureSettings settings;

		public PluralTermDefinitionCSharpGenerator(CultureSettings settings)
		{
			this.settings = settings;
		}

		SimpleTermDefinition GetTerm(PluralTermDefinition definition, Plurality plurality)
		{
			if (definition.Pluralities.ContainsKey(plurality))
				return definition.Pluralities[plurality];

			if(definition.Pluralities.ContainsKey(Plurality.One))
				return definition.Pluralities[Plurality.One];
			
			if (definition.Pluralities.ContainsKey(Plurality.Two))
				return definition.Pluralities[Plurality.Two];
			
			if (definition.Pluralities.ContainsKey(Plurality.Few))
				return definition.Pluralities[Plurality.Few];
			
			if (definition.Pluralities.ContainsKey(Plurality.Many))
				return definition.Pluralities[Plurality.Many];
			
			if (definition.Pluralities.ContainsKey(Plurality.Other))
				return definition.Pluralities[Plurality.Other];

			if (definition.Pluralities.ContainsKey(Plurality.Zero))
				return definition.Pluralities[Plurality.Zero];

			// really stuck :/
			return null;
		}

		public override void Generate(StringBuilder builder, string fullClassName, PluralTermDefinition definition)
		{
			var zero = GetTerm(definition, Plurality.Zero);
			CSharpGenerationHelper.GenerateTermDeclaration_Property(builder, this.settings, fullClassName, zero, '_' + definition.Key + "_Zero");

			var one = GetTerm(definition, Plurality.One);
			CSharpGenerationHelper.GenerateTermDeclaration_Property(builder, this.settings, fullClassName, one, '_' + definition.Key + "_One");

			var two = GetTerm(definition, Plurality.Two);
			CSharpGenerationHelper.GenerateTermDeclaration_Property(builder, this.settings, fullClassName, two, '_' + definition.Key + "_Two");

			var few = GetTerm(definition, Plurality.Few);
			CSharpGenerationHelper.GenerateTermDeclaration_Property(builder, this.settings, fullClassName, few, '_' + definition.Key + "_Few");

			var many = GetTerm(definition, Plurality.Many);
			CSharpGenerationHelper.GenerateTermDeclaration_Property(builder, this.settings, fullClassName, many, '_' + definition.Key + "_Many");

			var other = GetTerm(definition, Plurality.Other);
			CSharpGenerationHelper.GenerateTermDeclaration_Property(builder, this.settings, fullClassName, other, '_' + definition.Key + "_Other");
						
			builder.AppendLine($@"
		public static Term {definition.Key}({string.Join(", ", definition.AllParameters.Select(p => p.Type.Namespace + '.' + p.Type.Name + ' ' + p.Name))})
		{{
			return new ParameterisedTerm(new DelegatedTerm(""{definition.Key}"", culture =>
			{{
				var plurality = PluralRules.GetPlurality(culture, {definition.PluralParameterName});
				switch (plurality)
				{{
					case Plurality.Zero:
						return _{definition.Key}_Zero;
					case Plurality.One:
						return _{definition.Key}_One;
					case Plurality.Two:
						return _{definition.Key}_Two;
					case Plurality.Few:
						return _{definition.Key}_Few;
					case Plurality.Many:
						return _{definition.Key}_Many;
					case Plurality.Other:
					default:
						return _{definition.Key}_Other;
				}}
			}}), {string.Join(", ", definition.AllParameters.Select(p => p.Name))});
		}}");

		}
	}
}