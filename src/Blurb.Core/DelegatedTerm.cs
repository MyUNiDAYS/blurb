using System;
using System.Globalization;

namespace Blurb.Core
{
	public sealed class DelegatedTerm : Term
	{
		readonly Func<CultureInfo, Term> @delegate;
		public override string Key { get; }

		public DelegatedTerm(string key, Func<CultureInfo, Term> @delegate)
		{
			this.Key = key;
			this.@delegate = @delegate;
		}

		public override string ToString(CultureInfo culture)
		{
			return @delegate(culture).ToString();
		}
	}
}