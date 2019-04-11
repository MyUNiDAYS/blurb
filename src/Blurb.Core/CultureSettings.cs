using System.Collections.Generic;
using System.Globalization;

namespace Blurb.Core
{
	public sealed class CultureSettings
	{
		public IReadOnlyList<CultureInfo> SupportedCultures { get; set; }
		public CultureInfo DefaultCulture { get; set; }
	}
}