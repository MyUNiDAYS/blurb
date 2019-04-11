using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Blurb.Core.Parsing
{
	public class ValueParser
	{
		static readonly Regex parameterRegex = new Regex(@"(?<!\{)\{([^\{\}:#]+)((?>:|#)[^\}:#]+)?((?>:|#)[^\}:#]+)?\}", RegexOptions.Compiled, TimeSpan.FromSeconds(1));

		public static TermValue Parse(string value)
		{
			var builder = new StringBuilder(value);

			var matches = parameterRegex.Matches(value);

			if (matches.Count == 0)
				return new TermValue
				{
					Value = value,
					OriginalValue = value
				};

			var parameters = new TermParameter[matches.Count];
			for (var i = matches.Count - 1; i >= 0; i--)
			{
				var name = matches[i].Groups[1].Value;
				string format = null;
				var type = "object";

				if (matches[i].Groups[3].Success)
				{
					var val = matches[i].Groups[3].Value;
					if (val.StartsWith(":"))
					{
						format = val.TrimStart(':');
					}
					else
					{
						type = val.TrimStart('#');

						// remove Type
						builder.Remove(matches[i].Groups[3].Index, matches[i].Groups[3].Length);
					}
				}

				if (matches[i].Groups[2].Success)
				{
					var val = matches[i].Groups[2].Value;
					if (val.StartsWith(":"))
					{
						format = val.TrimStart(':');
					}
					else
					{
						type = val.TrimStart('#');

						// remove Type
						builder.Remove(matches[i].Groups[2].Index, matches[i].Groups[2].Length);
					}
				}

				parameters[i] = new TermParameter
				{
					Index = i,
					Name = name,
					Format = format,
					Type = type
				};

				// replace arg name with index
				builder.Remove(matches[i].Groups[1].Index, matches[i].Groups[1].Length);
				builder.Insert(matches[i].Groups[1].Index, i.ToString());
			}

			return new TermValue
			{
				OriginalValue = value,
				Value = builder.ToString(),
				Parameters = parameters
			};
		}
	}
}