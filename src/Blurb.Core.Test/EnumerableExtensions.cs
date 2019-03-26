using System;
using System.Collections.Generic;
using System.Linq;

namespace Blurb.Core.Test
{
	static class EnumerableExtensions
	{

		public static IEnumerable<TIn> Distinct<TIn, TProp>(this IEnumerable<TIn> enumerable, Func<TIn, TProp> property) where TProp : IComparable
		{
			var comparer = new TEqualityComparer<TIn, TProp>(property);
			return enumerable.Distinct(comparer);
		}

		sealed class TEqualityComparer<T, TProp> : IEqualityComparer<T> where TProp : IComparable
		{
			readonly Func<T, TProp> property;

			public TEqualityComparer(Func<T, TProp> property)
			{
				this.property = property;
			}

			public bool Equals(T x, T y)
			{
				return this.property(x).Equals(this.property(y));
			}

			public int GetHashCode(T obj)
			{
				return this.property(obj).GetHashCode();
			}
		}
	}
}