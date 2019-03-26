using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Blurb.Core.Parsing;

namespace Blurb.Core.Generation.CSharp
{
	sealed class CSharpGenerator
	{
		readonly IEnumerable<ITermCSharpGenerator> generators;

		public CSharpGenerator(IEnumerable<ITermCSharpGenerator> generators)
		{
			this.generators = generators;
		}

		public string Generate(TermCollection collection)
		{
			var builder = new StringBuilder();

			foreach (var definition in collection.Terms)
			{
				foreach (var generator in this.generators)
				{
					if (generator.CanGenerateFor(definition))
						generator.Generate(builder, collection.ClassName + '.' + collection.ClassName, definition);
				}
			}

			return builder.ToString();
		}
		
	}
}