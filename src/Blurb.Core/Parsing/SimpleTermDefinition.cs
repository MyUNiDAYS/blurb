using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Blurb.Core.Parsing
{
	public class SimpleTermDefinition : ITermDefinition
	{
		public string Key { get; set; }
		public IDictionary<CultureInfo, TermValue> Translations { get; set; }

		public IReadOnlyList<TermParameter> AllParameters
		{
			get
			{
				return this.Translations
					.SelectMany(t => t.Value.Parameters)
					// Here we don't support clever parameter diffing, its the authors responsibility to ensure parameters are properly duplicated between translations
					.Distinct(TermParameter.Comparer).ToArray();
			}
		}
	}
}