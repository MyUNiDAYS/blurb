using System.Globalization;

namespace Blurb.Core
{
	public sealed class EmptyTerm : Term
	{
		public override string Key => "Empty";
		public override string ToString(CultureInfo culture)
		{
			return null;
		}
	}
}