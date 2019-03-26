using System;
using System.Globalization;
using System.Reflection;
using Blurb.Core.Generation.CSharp;
using Blurb.Core.Parsing;
using Xunit;

namespace Blurb.Core.Test.Generation.CSharp
{
	public abstract class CompiledSpec
	{
		Assembly assembly;
		Type testTerm;
		public abstract TermCollection Collection { get; }

		[Fact]
		public void ShouldCompileProperly()
		{
			var supportedCultures = new[] {new CultureInfo("en")};

			var simpleTermDefinitionCSharpGenerator = new SimpleTermDefinitionCSharpGenerator(supportedCultures);
			var generator = new CSharpGenerator(new ITermCSharpGenerator[]{ simpleTermDefinitionCSharpGenerator, new PluralTermDefinitionCSharpGenerator(supportedCultures) });
			var generated = generator.Generate(this.Collection);

			assembly = new Compiler().CompileCSharp(@"
using System.Collections.Generic;
using Blurb.Core;

namespace Test { 
	public class TestTerms { " + generated + "} }");

			testTerm = assembly.GetType("Test.TestTerms");

			Assertions();
		}

		public abstract void Assertions();

		protected Term GetStaticTerm(string name)
		{
			return this.testTerm.GetField(name).GetValue(null) as Term;
		}

		protected Term GetParameterisedTerm(string name, params object[] args)
		{
			return this.testTerm.GetMethod(name).Invoke(null, args) as Term;
		}

	}
}