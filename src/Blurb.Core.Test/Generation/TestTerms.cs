using System.Collections.Generic;
using System.Globalization;
using Blurb.Core.Parsing;

namespace Blurb.Core.Test.Generation
{
	public static class TestTerms
	{
		public static readonly TermCollection SimpleStatic = new TermCollection
		{
			Namspace = "Blurb.Core.Test.Generation",
			ClassName = "TestTerms",
			Terms = new[]
			{
				new SimpleTermDefinition
				{
					Key = "SimpleStatic",
					Translations = new Dictionary<CultureInfo, TermValue>
					{
						{
							new CultureInfo("en"), new TermValue
							{
								OriginalValue = "I am simple",
								Value = "I am simple"
							}
						},
						{
							new CultureInfo("de"), new TermValue
							{
								OriginalValue = "Ich bin einfach",
								Value = "Ich bin einfach"
							}
						}
					}
				}
			}
		};

		public static readonly TermCollection SimpleDynamic = new TermCollection
		{
			Namspace = "Blurb.Core.Test.Generation",
			ClassName = "TestTerms",
			Terms = new[]
			{
				new SimpleTermDefinition
				{
					Key = "SimpleDynamic",
					Translations = new Dictionary<CultureInfo, TermValue>
					{
						{
							new CultureInfo("en"), new TermValue
							{
								OriginalValue = "I am {age}",
								Value = "I am {0}",
								Parameters = new[]
								{
									new TermParameter {Name = "age", Type = "int"}
								}
							}
						}
					}
				}
			}
		};

		public static readonly TermCollection ComplexAB = new TermCollection
		{
			Namspace = "Blurb.Core.Test.Generation",
			ClassName = "TestTerms",
			Terms = new[]
			{
				new ComplexTermDefinition
				{
					Key = "ABTest",
					ComplexParameter = new TermParameter{Name = "ab", Type = "Blurb.Core.Test.Generation.CSharp.Complex.ABMode"},
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
											Parameters = new[] {new TermParameter {Name = "days", Type = "System.Decimal"}}
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
											Parameters = new[] {new TermParameter {Name = "days", Type = "System.Decimal" } }
										}
									}
								}
							}
						}
					}
				}
			}
		};
	}
}