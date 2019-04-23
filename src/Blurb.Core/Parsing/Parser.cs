using System;
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
			var root = (JObject) JsonConvert.DeserializeObject(json);

			var @namespace = (string) root["namespace"];
			var @class = (string) root["class"];

			var jsonTerms = root["terms"] as JArray;

			var termDefinitions = jsonTerms.Select(obj => ParseTermDefinition(obj as JObject)).ToArray();

			return new TermCollection
			{
				Namspace = @namespace,
				ClassName = @class,
				Terms = termDefinitions
			};
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
					Translations = jsonLangs.Select(p => new {key = new CultureInfo(p.Name), value = ValueParser.Parse((string) p.Value)}).ToDictionary(x => x.key, x => x.value)
				};
			}

			// complex

			var variations = jsonLangs.SelectMany(p => (p.Value as JObject).Properties()).Select(p => p.Name).Distinct().ToArray();

			var pluralityNames = Enum.GetNames(typeof(Plurality));
			var plural = variations
				.Select(v => v.Substring(v.LastIndexOf('.') + 1))
				.All(v => pluralityNames.Contains(v, StringComparer.InvariantCultureIgnoreCase));

			var pluralParameterName = variations.First().Substring(0, variations.First().LastIndexOf('.'));

			if (plural)
			{
				var termDefs = variations.Select(v => new
				{
					plurality = (Plurality)Enum.Parse(typeof(Plurality), v.Substring(v.LastIndexOf('.') + 1), true),
					term = new SimpleTermDefinition
					{
						Key = key,
						Translations = jsonLangs
							.Select(lang => new
							{
								key = new CultureInfo(lang.Name),
								value = ValueParser.Parse((string) (lang.Value as JObject).Property(v).Value)
							}).ToDictionary(x => x.key, x => x.value)
					}
				}).ToDictionary(x => x.plurality, x => x.term);

				foreach (var simpleTermDefinition in termDefs)
				{
					foreach (var termParameter in simpleTermDefinition.Value.AllParameters.Where(p => p.Name == pluralParameterName))
					{
						termParameter.Type = "decimal";
					}
				}

				return new PluralTermDefinition
				{
					Key = key,
					Pluralities = termDefs,
					PluralParameterName = pluralParameterName
				};
			}
			else
			{
				var termDefs = variations.Select(v => new
				{
					enumName = v,
					term = new SimpleTermDefinition
					{
						Key = key,
						Translations = jsonLangs
							.Select(lang => new
							{
								key = new CultureInfo(lang.Name),
								value = ValueParser.Parse((string)(lang.Value as JObject).Property(v).Value)
							}).ToDictionary(x => x.key, x => x.value)
					}
				}).ToDictionary(x => x.enumName, x => x.term);

				var enumType = pluralParameterName;

				var lastIndexOf = enumType.LastIndexOf('.');
				string name;
				if (lastIndexOf > -1)
					name = enumType.Substring(lastIndexOf + 1);
				else
					name = enumType;

				name = char.ToLower(name[0]) + name.Substring(1);

				return new ComplexTermDefinition
				{
					Key = key,
					Complexities = termDefs,
					ComplexParameter = new TermParameter
					{
						Name = name,
						Type = enumType
					} 
				};
			}


			return null;
		}
	}
}