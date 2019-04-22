using System.Globalization;
using Blurb.Core.Generation.Javascript;
using Blurb.Core.Parsing;
using Xunit;
namespace Blurb.Core.Test.Generation.Javascript
{
	public abstract class JsExecutionSpec
	{
		JsExecutor jsExecutor;
		public abstract TermCollection Collection { get; }
		public abstract CultureInfo Culture { get; }

		[Fact]
		public void ShouldCompileProperly()
		{
			var cultureEn = new CultureInfo("en");
			var supportedCultures = new[] { cultureEn };
			var cultureSettings = new CultureSettings { SupportedCultures = supportedCultures, DefaultCulture = cultureEn };
			
			var generator = new JsGenerator(new ITermJsGenerator[] { new SimpleTermDefinitionJsGenerator(cultureSettings), new ComplexTermDefinitionJsGenerator(cultureSettings),  });
			var generated = generator.Generate(this.Collection, this.Culture);

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