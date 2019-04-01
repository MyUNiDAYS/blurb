using System.Globalization;

namespace Blurb.Core
{
	public abstract class Term
	{
		public abstract string Key { get; }
		public abstract string ToString(CultureInfo culture);

		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}
		
		public static implicit operator string (Term term)
		{
			return term.ToString(CultureInfo.CurrentCulture);
		}
	}
}
