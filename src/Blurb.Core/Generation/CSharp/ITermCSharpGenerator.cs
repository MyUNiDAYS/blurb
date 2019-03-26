using System.Text;

namespace Blurb.Core.Generation.CSharp
{
	interface ITermCSharpGenerator
	{
		bool CanGenerateFor(object definition);
		void Generate(StringBuilder builder, string fullClassName, object definition);
	}

	interface ITermCSharpGenerator<in TTermDefintion> : ITermCSharpGenerator where TTermDefintion : class
	{
		void Generate(StringBuilder builder, string fullClassName, TTermDefintion definition);
	}
}