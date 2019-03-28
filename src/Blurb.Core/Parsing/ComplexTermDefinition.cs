using System.Collections.Generic;
using System.Linq;

namespace Blurb.Core.Parsing
{
	public class ComplexTermDefinition : ITermDefinition
	{
		public string Key { get; set; }
		public TermParameter ComplexParameter { get; set; }

		public IDictionary<string, SimpleTermDefinition> Complexities { get; set; }

		public IReadOnlyList<TermParameter> AllParameters
		{
			get
			{
				return
					new[] {this.ComplexParameter}.Union(
							this.Complexities
								.Values
								.SelectMany(p => p.Translations.Values)
								.SelectMany(t => t.Parameters)
						)
						// Here we don't support clever parameter diffing, its the authors responsibility to ensure parameters are properly duplicated between translations
						.Distinct(TermParameter.Comparer)
						.ToArray();
			}
		}
	}
}