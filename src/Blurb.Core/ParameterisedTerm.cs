using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Blurb.Core
{
	[DebuggerDisplay("Term: {Key}")]
	sealed class ParameterisedTerm : Term
	{
		readonly Term term;
		readonly object[] parameters;
		public override string Name => this.term.Name;
		public override string Key => this.term.Key;

		public ParameterisedTerm(Term term, params object[] parameters)
		{
			this.term = term;
			this.parameters = parameters.ToArray();
		}
		
		public override string ToString(CultureInfo culture)
		{
			return culture != null
				? this.FormatValueWithCulture(culture)
				: this.FormatValue();
		}

		string FormatValueWithCulture(CultureInfo culture)
		{
			var oldUiCulture = CultureInfo.CurrentUICulture;
			var oldCulture = CultureInfo.CurrentCulture;
			CultureInfo.CurrentUICulture = culture;
			CultureInfo.CurrentCulture = culture;

			var result = this.FormatValue();

			CultureInfo.CurrentUICulture = oldUiCulture;
			CultureInfo.CurrentCulture = oldCulture;
			return result;
		}

		string FormatValue()
		{
			var format = this.term.ToString();
			return string.Format(format, this.parameters);
		}
	}
}
