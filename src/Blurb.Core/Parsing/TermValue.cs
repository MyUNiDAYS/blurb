using System.Collections.Generic;

namespace Blurb.Core.Parsing
{
	public class TermValue
	{
		public string OriginalValue { get; set; }
		public string Value { get; set; }
		public IReadOnlyList<TermParameter> Parameters { get; set; }

		public TermValue()
		{
			this.Parameters = new TermParameter[0];
		}
	}
}