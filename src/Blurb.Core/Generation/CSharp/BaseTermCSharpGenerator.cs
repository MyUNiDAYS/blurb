using System.Text;

namespace Blurb.Core.Generation.CSharp
{
	abstract class BaseTermCSharpGenerator<T> : ITermCSharpGenerator<T> where T : class
	{
		public abstract void Generate(StringBuilder builder, string fullClassName, T definition);

		public bool CanGenerateFor(object definition)
		{
			return definition is T;
		}

		void ITermCSharpGenerator.Generate(StringBuilder builder, string fullClassName, object definition)
		{
			this.Generate(builder, fullClassName, definition as T);
		}
	}
}