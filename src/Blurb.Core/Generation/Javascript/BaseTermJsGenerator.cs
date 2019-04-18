using System.Globalization;
using System.Text;

namespace Blurb.Core.Generation.Javascript
{
	abstract class BaseTermJsGenerator<T> : ITermJsGenerator<T> where T : class
	{
		public abstract void Generate(StringBuilder builder, string fullClassName, T definition, CultureInfo culture);

		public bool CanGenerateFor(object definition)
		{
			return definition is T;
		}

		void ITermJsGenerator.Generate(StringBuilder builder, string fullClassName, object definition, CultureInfo culture)
		{
			this.Generate(builder, fullClassName, definition as T, culture);
		}
	}
}