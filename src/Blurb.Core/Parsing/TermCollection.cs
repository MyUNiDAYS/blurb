using System.Collections.Generic;

namespace Blurb.Core.Parsing
{
	public class TermCollection
	{
		public string Namspace { get; set; }
		public string ClassName { get; set; }
		public IEnumerable<ITermDefinition> Terms { get; set; }
	}
}