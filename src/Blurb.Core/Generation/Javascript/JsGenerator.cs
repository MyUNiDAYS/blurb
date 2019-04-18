using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Blurb.Core.Parsing;

namespace Blurb.Core.Generation.Javascript
{
	sealed class JsGenerator
	{
		readonly IEnumerable<ITermJsGenerator> generators;

		public JsGenerator(IEnumerable<ITermJsGenerator> generators)
		{
			this.generators = generators;
		}

		public string Generate(TermCollection collection, CultureInfo culture)
		{
			var builder = new StringBuilder();

			foreach (var definition in collection.Terms)
			{
				foreach (var generator in this.generators)
				{
					if (generator.CanGenerateFor(definition))
						generator.Generate(builder, collection.ClassName + '.' + collection.ClassName, definition, culture);
				}
			}

			return builder.ToString();
		}
		
	}
}