using System.Globalization;
using Blurb.Core.Generation.Javascript;
using Xunit;
namespace Blurb.Core.Test.Generation.Javascript
{
	public abstract class JsExecutionSpec
	{
		JsExecutor jsExecutor;
		public abstract CultureInfo Culture { get; }

		[Fact]
		public void ShouldCompileProperly()
		{
			var generator = new JsGenerator(new ITermJsGenerator[] { new SimpleTermDefinitionJsGenerator(), new ComplexTermDefinitionJsGenerator() });
			var generated = generator.Generate(TestTerms.Terms, this.Culture);

			this.jsExecutor = new JsExecutor("var testTerms = new class {\n" + generated + "\n}();");

			this.Assertions();
		}

		public abstract void Assertions();

		protected string GetStaticTerm(string name)
		{
			return this.jsExecutor.GetTerm(name);
		}

		protected string GetParameterisedTerm(string name, params object[] args)
		{
			return this.jsExecutor.GetTerm(name, args);
		}
	}
}