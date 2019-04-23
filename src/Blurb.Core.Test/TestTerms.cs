using System.IO;
using Blurb.Core.Parsing;

namespace Blurb.Core.Test
{
	public static class TestTerms
	{
		static TermCollection terms;

		public static TermCollection Terms
		{
			get
			{
				if (terms == null)
				{
					var stream = typeof(TestTerms).Assembly.GetManifestResourceStream("Blurb.Core.Test.input.json");
					using (var reader = new StreamReader(stream))
					{
						var json = reader.ReadToEnd();
						var parser = new Parser();
						terms = parser.Parse(json);
					}
				}

				return terms;
			}
		}
	}
}