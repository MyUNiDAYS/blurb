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
	}
}