using System;
using System.Collections.Generic;
using System.Globalization;
using Blurb.Core.Parsing;

namespace Blurb.Core.Test.Generation.CSharp.Plurals
{
	public class ComplexGeneration : CompiledSpec
	{
		public override TermCollection Collection => new TermCollection
		{
			Namspace = this.GetType().Namespace,
			ClassName = this.GetType().Name,
			Terms = new[]
			{
				new ComplexTermDefinition
				{
					Key = "ABTest",
					ComplexParameter = new TermParameter{Name = "ab", Type = typeof(Char)},
					Complexities = new Dictionary<string, SimpleTermDefinition>
					{
						{
							"Char.One", new SimpleTermDefinition
							{
								Key = "PluralDays.One",
								Translations = new Dictionary<CultureInfo, TermValue>
								{
									{
										new CultureInfo("en"),
										new TermValue
										{
											Value = "{0} day", Parameters = new[] {new TermParameter {Name = "days", Type = typeof(decimal)}}
										}
									}
								}
							}
						},
						{
							"Plurality.Many", new SimpleTermDefinition
							{
								Key = "PluralDays.Many",
								Translations = new Dictionary<CultureInfo, TermValue>
								{
									{
										new CultureInfo("en"),
										new TermValue
										{
											Value = "{0} days", Parameters = new[] {new TermParameter {Name = "days", Type = typeof(decimal) } }
										}
									}
								}
							}
						}
					}
				}
			}
		};

		public override void Assertions()
		{
			var termOne = GetParameterisedTerm("PluralDays", 1m);
			termOne.Key.ShouldEqual("Blurb.Core.Test.Generation.CSharp.Plurals.PluralGeneration");
			termOne.ToString(new CultureInfo("en")).ShouldEqual("1 day");

			var termMany = GetParameterisedTerm("PluralDays", 10m);
			termMany.Key.ShouldEqual("Blurb.Core.Test.Generation.CSharp.Plurals.PluralGeneration");
			termMany.ToString(new CultureInfo("en")).ShouldEqual("10 days");
		}
	}
}