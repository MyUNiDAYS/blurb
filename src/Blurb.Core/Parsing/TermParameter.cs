using System;
using System.Collections.Generic;

namespace Blurb.Core.Parsing
{
	public class TermParameter
	{
		public int Index { get; set; }
		public string Name { get; set; }
		public string Format { get; set; }
		public Type Type { get; set; }

		public static IEqualityComparer<TermParameter> Comparer { get; } = new NameEqualityComparer();

		sealed class NameEqualityComparer : IEqualityComparer<TermParameter>
		{
			public bool Equals(TermParameter x, TermParameter y)
			{
				if (ReferenceEquals(x, y)) return true;
				if (ReferenceEquals(x, null)) return false;
				if (ReferenceEquals(y, null)) return false;
				if (x.GetType() != y.GetType()) return false;
				return string.Equals(x.Name, y.Name);
			}

			public int GetHashCode(TermParameter obj)
			{
				return (obj.Name != null ? obj.Name.GetHashCode() : 0);
			}
		}
	}
}