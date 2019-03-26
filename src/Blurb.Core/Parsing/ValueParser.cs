using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Blurb.Core.Parsing
{
	public class ValueParser
	{
		static readonly Regex parameterRegex = new Regex(@"(?<!\{)\{([^\{\}:]+)(:[^\}]*)?\}", RegexOptions.Compiled, TimeSpan.FromSeconds(1));

		public static TermValue Parse(string value)
		{
			var builder = new StringBuilder(value);

			var matches = parameterRegex.Matches(value);

			if (matches.Count == 0)
				return new TermValue {Value = value};

			var parameters = new TermParameter[matches.Count];
			for (var i = matches.Count - 1; i >= 0; i--)
			{
				parameters[i] = new TermParameter
				{
					Index = i,
					Name = matches[i].Groups[1].Value,
					Format = matches[i].Groups[2].Success ? matches[i].Groups[2].Value.TrimStart(':') : null,
					Type = typeof(object)
				};

				builder.Remove(matches[i].Groups[1].Index, matches[i].Groups[1].Length);
				builder.Insert(matches[i].Groups[1].Index, i.ToString());
			}

			return new TermValue
			{
				Value = builder.ToString(),
				Parameters = parameters
			};
		}
	}
}