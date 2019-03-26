using System.Globalization;

namespace Blurb.Core
{
	public abstract class Term
	{
		public abstract string Name { get; }
		public abstract string Key { get; }
		public abstract string ToString(CultureInfo culture);

		public override string ToString()
		{
			return this.ToString(null);
		}
		
		public static implicit operator string (Term term)
		{
			return term.ToString(null);
		}
	}
}
