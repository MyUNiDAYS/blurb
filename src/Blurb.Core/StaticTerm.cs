using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Blurb.Core
{
	[DebuggerDisplay("Term: {" + nameof(Key) + "}")]
	sealed class StaticTerm : Term
	{
		public override string Key { get; }
		public override string Name { get; }

		readonly IDictionary<string, string> values;
		
		public StaticTerm(string @namespace, string name, IDictionary<string, string> values)
		{
			this.Key = @namespace + '.' + name;
			this.Name = name;
			this.values = values;
		}
		
		public static implicit operator string(StaticTerm term)
		{
			return term.ToString(null);
		}

		public override string ToString(CultureInfo culture)
		{
			culture = culture ?? CultureInfo.CurrentCulture;

			//var key = GetSupportedCultureName(culture);
			string result;
			return this.values.TryGetValue(culture.Name, out result) 
				? result
				: this.values["en"];
		}
		
	}
}
