using System.Globalization;

namespace Blurb.Core
{
	public sealed class EscapedTerm : Term
	{
		readonly Term term;
		public override string Key => this.term.Key;

		public EscapedTerm(Term term)
		{
			this.term = term;
		}

		public override string ToString(CultureInfo culture)
		{
			return string.Format(this.term.ToString(culture));
		}
	}
}