using System;
using System.Globalization;

namespace Blurb.Core
{
	public class DelegatedTerm : Term
	{
		readonly Func<CultureInfo, Term> @delegate;
		public override string Name { get; }
		public override string Key { get; }

		public DelegatedTerm(string name, string key, Func<CultureInfo, Term> @delegate)
		{
			this.Name = name;
			this.Key = this.Key;
			this.@delegate = @delegate;
		}

		public override string ToString(CultureInfo culture)
		{
			return @delegate(culture).ToString();
		}
	}
}