using System.Collections.Generic;
using System.Globalization;
using Blurb.Core.Parsing;

namespace Blurb.Core.Test.Generation.CSharp
{
	public class SimpleStaticGeneration : CompiledSpec
	{
		public override TermCollection Collection => new TermCollection
		{
			Namspace = this.GetType().Namespace,
			ClassName = this.GetType().Name,
			Terms = new[]
			{
				new SimpleTermDefinition
				{
					Key = "SimpleStatic",
					Translations = new Dictionary<CultureInfo, TermValue>
					{
						{new CultureInfo("en"), new TermValue {Value = "I am simple"}}
					}
				}
			}
		};

		public override void Assertions()
		{
			var staticTerm = GetStaticTerm("SimpleStatic");

			staticTerm.Key.ShouldEqual("Blurb.Core.Test.Generation.CSharp.SimpleStaticGeneration.SimpleStatic");
			staticTerm.ToString(new CultureInfo("en")).ShouldEqual("I am simple");
		}
	}
}