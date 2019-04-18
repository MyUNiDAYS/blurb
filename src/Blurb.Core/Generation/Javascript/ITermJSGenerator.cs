using System.Globalization;
using System.Text;

namespace Blurb.Core.Generation.Javascript
{
	interface ITermJsGenerator
	{
		bool CanGenerateFor(object definition);
		void Generate(StringBuilder builder, string fullClassName, object definition, CultureInfo culture);
	}

	interface ITermJsGenerator<in TTermDefintion> : ITermJsGenerator where TTermDefintion : class
	{
		void Generate(StringBuilder builder, string fullClassName, TTermDefintion definition, CultureInfo culture);
	}
}