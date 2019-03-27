using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Blurb.Core.Parsing
{
	public class Parser
	{
		public TermCollection Parse(string json)
		{
			var root = (JObject)JsonConvert.DeserializeObject(json);

			var @namespace = (string) root["namespace"];
			var @class = (string) root["class"];

			var jsonTerms = root["terms"] as JArray;

			foreach (JObject obj in jsonTerms)
			{
				ParseTermDefinition(obj);
			}

			return null;
		}

		ITermDefinition ParseTermDefinition(JObject obj)
		{
			var key = (string) obj["key"];
			var jsonTranslations = obj["translations"] as JObject;
			var jsonLangs = jsonTranslations.Properties();

			// simple

			if (jsonLangs.All(p => p.Value is JValue))
			{
				return new SimpleTermDefinition
				{
					Key = key,
					Translations = jsonLangs.Select(p => new { key = new CultureInfo(p.Name), value = ValueParser.Parse((string) p.Value)}).ToDictionary(x => x.key, x => x.value)
				};
			}

			// complex
			
			var variations = jsonLangs.SelectMany(p => (p.Value as JObject).Properties()).Select(p => p.Name).Distinct().ToArray();

			variations.Select(v => new SimpleTermDefinition
			{
				Key = key,
				Translations = jsonLangs.Select(lang => new { key = new CultureInfo(lang.Name), value = ValueParser.Parse((string)(lang.Value as JObject).Property(v).Value) }).ToDictionary(x => x.key, x => x.value)

			return null;
		}
	}
}
