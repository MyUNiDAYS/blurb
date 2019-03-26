using System.Collections.Generic;
using System.Linq;

namespace Blurb.Core.Parsing
{
	public class PluralTermDefinition : ITermDefinition
	{
		public string Key { get; set; }
		public string PluralParameterName { get; set; }

		public IDictionary<Plurality, SimpleTermDefinition> Pluralities { get; set; }

		public IReadOnlyList<TermParameter> AllParameters
		{
			get
			{
				return this.Pluralities
					.Values
					.SelectMany(p => p.Translations.Values)
					.SelectMany(t => t.Parameters)
					// Here we don't support clever parameter diffing, its the authors responsibility to ensure parameters are properly duplicated between translations
					.Distinct(TermParameter.Comparer).ToArray();
			}
		}

	}
}