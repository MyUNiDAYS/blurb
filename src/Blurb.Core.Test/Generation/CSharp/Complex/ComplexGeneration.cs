using System.Collections.Generic;
using System.Globalization;
using Blurb.Core.Parsing;

namespace Blurb.Core.Test.Generation.CSharp.Complex
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
					ComplexParameter = new TermParameter{Name = "ab", Type = typeof(ABMode)},
					Complexities = new Dictionary<string, SimpleTermDefinition>
					{
						{
							"Blurb.Core.Test.Generation.CSharp.Complex.ABMode.A", new SimpleTermDefinition
							{
								Key = "ABTest.A",
								Translations = new Dictionary<CultureInfo, TermValue>
								{
									{
										new CultureInfo("en"),
										new TermValue
										{
											OriginalValue = "Copy A {days}",
											Value = "Copy A {0}",
											Parameters = new[] {new TermParameter {Name = "days", Type = typeof(decimal)}}
										}
									}
								}
							}
						},
						{
							"Blurb.Core.Test.Generation.CSharp.Complex.ABMode.B", new SimpleTermDefinition
							{
								Key = "ABTest.B",
								Translations = new Dictionary<CultureInfo, TermValue>
								{
									{
										new CultureInfo("en"),
										new TermValue
										{
											OriginalValue = "Copy B {days}",
											Value = "Copy B {0}",
											Parameters = new[] {new TermParameter {Name = "days", Type = typeof(decimal) } }
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
			var termOne = GetParameterisedTerm("ABTest", ABMode.A, 5m);
			termOne.Key.ShouldEqual("ABTest");
			termOne.ToString(new CultureInfo("en")).ShouldEqual("Copy A 5");

			var termMany = GetParameterisedTerm("ABTest", ABMode.B, 6m);
			termMany.Key.ShouldEqual("ABTest");
			termMany.ToString(new CultureInfo("en")).ShouldEqual("Copy B 6");
		}
	}
}