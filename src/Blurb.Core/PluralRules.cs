// Adapted from https://github.com/axuno/SmartFormat
// The MIT License(MIT)
// Copyright 2011-2019 axuno gGmbH, Scott Rippey, Bernhard Millauer and other contributors.
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 

using System.Collections.Generic;
using System.Globalization;

namespace Blurb.Core
{
	/// <summary>
	/// Assigns the ISO language code to a pluralization rule.
	/// </summary>
	public static class PluralRules
	{
		public static Plurality GetPlurality(CultureInfo culture, decimal value)
		{
			var isoLanguageName = culture.TwoLetterISOLanguageName;

			PluralRuleDelegate @delegate;
			if (!IsoLangToDelegate.ContainsKey(isoLanguageName))
				@delegate = Singular;
			else
				@delegate = IsoLangToDelegate[isoLanguageName];

			return (Plurality) @delegate(value, 2);
		}

		/// <summary>
		/// Holds the ISO langue code as key, and the <see cref="PluralRuleDelegate"/> with the pluralization rule.
		/// </summary>
		public static Dictionary<string, PluralRuleDelegate> IsoLangToDelegate =
			new Dictionary<string, PluralRuleDelegate>
			{
				// Singular
				{"az", Singular}, // Azerbaijani
				{"bm", Singular}, // Bambara
				{"bo", Singular}, // Tibetan
				{"dz", Singular}, // Dzongkha
				{"fa", Singular}, // Persian
				{"hu", Singular}, // Hungarian
				{"id", Singular}, // Indonesian
				{"ig", Singular}, // Igbo
				{"ii", Singular}, // Sichuan Yi
				{"ja", Singular}, // Japanese
				{"jv", Singular}, // Javanese
				{"ka", Singular}, // Georgian
				{"kde", Singular}, // Makonde
				{"kea", Singular}, // Kabuverdianu
				{"km", Singular}, // Khmer
				{"kn", Singular}, // Kannada
				{"ko", Singular}, // Korean
				{"ms", Singular}, // Malay
				{"my", Singular}, // Burmese
				{"root", Singular}, // Root (?)
				{"sah", Singular}, // Sakha
				{"ses", Singular}, // Koyraboro Senni
				{"sg", Singular}, // Sango
				{"th", Singular}, // Thai
				{"to", Singular}, // Tonga
				{"vi", Singular}, // Vietnamese
				{"wo", Singular}, // Wolof
				{"yo", Singular}, // Yoruba
				{"zh", Singular}, // Chinese
				// Dual: one (n == 1), other
				{"af", DualOneOther}, // Afrikaans
				{"bem", DualOneOther}, // Bembda
				{"bg", DualOneOther}, // Bulgarian
				{"bn", DualOneOther}, // Bengali
				{"brx", DualOneOther}, // Bodo
				{"ca", DualOneOther}, // Catalan
				{"cgg", DualOneOther}, // Chiga
				{"chr", DualOneOther}, // Cherokee
				{"da", DualOneOther}, // Danish
				{"de", DualOneOther}, // German
				{"dv", DualOneOther}, // Divehi
				{"ee", DualOneOther}, // Ewe
				{"el", DualOneOther}, // Greek
				{"en", DualOneOther}, // English
				{"eo", DualOneOther}, // Esperanto
				{"es", DualOneOther}, // Spanish
				{"et", DualOneOther}, // Estonian
				{"eu", DualOneOther}, // Basque
				{"fi", DualOneOther}, // Finnish
				{"fo", DualOneOther}, // Faroese
				{"fur", DualOneOther}, // Friulian
				{"fy", DualOneOther}, // Western Frisian
				{"gl", DualOneOther}, // Galician
				{"gsw", DualOneOther}, // Swiss German
				{"gu", DualOneOther}, // Gujarati
				{"ha", DualOneOther}, // Hausa
				{"haw", DualOneOther}, // Hawaiian
				{"he", DualOneOther}, // Hebrew
				{"is", DualOneOther}, // Icelandic
				{"it", DualOneOther}, // Italian
				{"kk", DualOneOther}, // Kazakh
				{"kl", DualOneOther}, // Kalaallisut
				{"ku", DualOneOther}, // Kurdish
				{"lb", DualOneOther}, // Luxembourgish
				{"lg", DualOneOther}, // Ganda
				{"lo", DualOneOther}, // Lao
				{"mas", DualOneOther}, // Masai
				{"ml", DualOneOther}, // Malayalam
				{"mn", DualOneOther}, // Mongolian
				{"mr", DualOneOther}, // Marathi
				{"nah", DualOneOther}, // Nahuatl
				{"nb", DualOneOther}, // Norwegian Bokmål
				{"ne", DualOneOther}, // Nepali
				{"nl", DualOneOther}, // Dutch
				{"nn", DualOneOther}, // Norwegian Nynorsk
				{"no", DualOneOther}, // Norwegian
				{"nyn", DualOneOther}, // Nyankole
				{"om", DualOneOther}, // Oromo
				{"or", DualOneOther}, // Oriya
				{"pa", DualOneOther}, // Punjabi
				{"pap", DualOneOther}, // Papiamento
				{"ps", DualOneOther}, // Pashto
				{"pt", DualOneOther}, // Portuguese
				{"rm", DualOneOther}, // Romansh
				{"saq", DualOneOther}, // Samburu
				{"so", DualOneOther}, // Somali
				{"sq", DualOneOther}, // Albanian
				{"ssy", DualOneOther}, // Saho
				{"sw", DualOneOther}, // Swahili
				{"sv", DualOneOther}, // Swedish
				{"syr", DualOneOther}, // Syriac
				{"ta", DualOneOther}, // Tamil
				{"te", DualOneOther}, // Telugu
				{"tk", DualOneOther}, // Turkmen
				{"tr", DualOneOther}, // Turkish
				{"ur", DualOneOther}, // Urdu
				{"wae", DualOneOther}, // Walser
				{"xog", DualOneOther}, // Soga
				{"zu", DualOneOther}, // Zulu
				// DualWithZero: one (n == 0..1), other
				{"ak", DualWithZero}, // Akan
				{"am", DualWithZero}, // Amharic
				{"bh", DualWithZero}, // Bihari
				{"fil", DualWithZero}, // Filipino
				{"guw", DualWithZero}, // Gun
				{"hi", DualWithZero}, // Hindi
				{"ln", DualWithZero}, // Lingala
				{"mg", DualWithZero}, // Malagasy
				{"nso", DualWithZero}, // Northern Sotho
				{"ti", DualWithZero}, // Tigrinya
				{"tl", DualWithZero}, // Tagalog
				{"wa", DualWithZero}, // Walloon
				// DualFromZeroToTwo: one (n == 0..2 fractionate and n != 2), other
				{"ff", DualFromZeroToTwo}, // Fulah
				{"fr", DualFromZeroToTwo}, // French
				{"kab", DualFromZeroToTwo}, // Kabyle
				// Triple: one (n == 1), two (n == 2), other
				{"ga", TripleOneTwoOther}, // Irish
				{"iu", TripleOneTwoOther}, // Inuktitut
				{"ksh", TripleOneTwoOther}, // Colognian
				{"kw", TripleOneTwoOther}, // Cornish
				{"se", TripleOneTwoOther}, // Northern Sami
				{"sma", TripleOneTwoOther}, // Southern Sami
				{"smi", TripleOneTwoOther}, // Sami language
				{"smj", TripleOneTwoOther}, // Lule Sami
				{"smn", TripleOneTwoOther}, // Inari Sami
				{"sms", TripleOneTwoOther}, // Skolt Sami
				// Russian & Serbo-Croatian
				{"be", RussianSerboCroatian}, // Belarusian
				{"bs", RussianSerboCroatian}, // Bosnian
				{"hr", RussianSerboCroatian}, // Croatian
				{"ru", RussianSerboCroatian}, // Russian
				{"sh", RussianSerboCroatian}, // Serbo-Croatian
				{"sr", RussianSerboCroatian}, // Serbian
				{"uk", RussianSerboCroatian}, // Ukrainian
				// Unique
				// Arabic
				{"ar", Arabic},
				// Breton
				{"br", Breton},
				// Czech
				{"cs", Czech},
				// Welsh
				{"cy", Welsh},
				// Manx
				{"gv", Manx},
				// Langi
				{"lag", Langi},
				// Lithuanian
				{"lt", Lithuanian},
				// Latvian
				{"lv", Latvian},
				// Macedonian
				{"mb", Macedonian},
				// Moldavian
				{"mo", Moldavian},
				// Maltese
				{"mt", Maltese},
				// Polish
				{"pl", Polish},
				// Romanian
				{"ro", Romanian},
				// Tachelhit
				{"shi", Tachelhit},
				// Slovak
				{"sk", Slovak},
				// Slovenian
				{"sl", Slovenian},
				// Central Morocco Tamazight
				{"tzm", CentralMoroccoTamazight}
			};

		static PluralRuleDelegate Singular => (n, c) => 0;

		static PluralRuleDelegate DualOneOther => (n, c) =>
		{
			if (c == 2)
				return n == 1 ? Plurality.One : Plurality.Other;

			if (c == 3)
				return n == 0 ? Plurality.Zero : 
					n == 1 ? Plurality.One : 
					Plurality.Other;

			if (c == 4)
				return n < 0 ? 0 :
					n == 0 ? Plurality.Zero : 
					n == 1 ? Plurality.One : 
					Plurality.Other;

			return Plurality.Other;
		}; // Dual: one (n == 1), other

		static PluralRuleDelegate DualWithZero =>
			(n, c) => n == 0 || n == 1 ? Plurality.One : 
				Plurality.Other; // DualWithZero: one (n == 0..1), other

		/// <summary>
		/// TODO: this looks bugged? Doesn't do what it says?
		/// </summary>
		static PluralRuleDelegate DualFromZeroToTwo =>
			(n, c) => n == 0 || n == 1 ? 0 : 
				Plurality.Other; // DualFromZeroToTwo: one (n == 0..2 fractionate and n != 2), other

		static PluralRuleDelegate TripleOneTwoOther =>
			(n, c) => 
				n == 1 ? Plurality.One : 
				n == 2 ? Plurality.Two : 
				Plurality.Other; // Triple: one (n == 1), two (n == 2), other

		static PluralRuleDelegate RussianSerboCroatian => (n, c) =>
			n % 10 == 1 && n % 100 != 11 ? Plurality.One : // one
			(n % 10).Between(2, 4) && !(n % 100).Between(12, 14) ? Plurality.Few : // few
			Plurality.Other; // Russian & Serbo-Croatian

		static PluralRuleDelegate Arabic => (n, c) =>
			n == 0 ? Plurality.Zero : // zero
			n == 1 ? Plurality.One : // one
			n == 2 ? Plurality.Two : // two
			(n % 100).Between(3, 10) ? Plurality.Few : // few
			(n % 100).Between(11, 99) ? Plurality.Many : // many
			Plurality.Other; // other

		static PluralRuleDelegate Breton => (n, c) =>
			n == 0 ? Plurality.Zero : // zero
			n == 1 ? Plurality.One : // one
			n == 2 ? Plurality.Two : // two
			n == 3 ? Plurality.Few : // few
			n == 6 ? Plurality.Many : // many
			Plurality.Other; // other

		static PluralRuleDelegate Czech => (n, c) =>
			n == 1 ? Plurality.One : // one
			n.Between(2, 4) ? Plurality.Few : // few
			Plurality.Other;

		static PluralRuleDelegate Welsh => (n, c) =>
			n == 0 ? Plurality.Zero : // zero
			n == 1 ? Plurality.One : // one
			n == 2 ? Plurality.Two : // two
			n == 3 ? Plurality.Few : // few
			n == 6 ? Plurality.Many : // many
			Plurality.Other;

		static PluralRuleDelegate Manx => (n, c) =>
			(n % 10).Between(1, 2) || n % 20 == 0
				? Plurality.One : // one
				Plurality.Other;

		static PluralRuleDelegate Langi => (n, c) =>
			n == 0 ? Plurality.Zero : // zero
			n > 0 && n < 2 ? Plurality.One : // one
			Plurality.Other;

		static PluralRuleDelegate Lithuanian => (n, c) =>
			n % 10 == 1 && !(n % 100).Between(11, 19) ? Plurality.One : // one
			(n % 10).Between(2, 9) && !(n % 100).Between(11, 19) ? Plurality.Few : // few
			Plurality.Other;

		static PluralRuleDelegate Latvian => (n, c) =>
			n == 0 ? Plurality.Zero : // zero
			n % 10 == 1 && n % 100 != 11 ? Plurality.One :
			Plurality.Other;

		static PluralRuleDelegate Macedonian => (n, c) =>
			n % 10 == 1 && n != 11
				? Plurality.One : // one
				Plurality.Other;

		static PluralRuleDelegate Moldavian => (n, c) =>
			n == 1 ? Plurality.One : // one
			n == 0 || n != 1 && (n % 100).Between(1, 19) ? Plurality.Few : // few
			Plurality.Other;

		static PluralRuleDelegate Maltese => (n, c) =>
			n == 1 ? Plurality.One : // one
			n == 0 || (n % 100).Between(2, 10) ? Plurality.Few : // few
			(n % 100).Between(11, 19) ? Plurality.Many : // many
			Plurality.Other;

		static PluralRuleDelegate Polish => (n, c) =>
			n == 1 ? Plurality.One : // one
			(n % 10).Between(2, 4) && !(n % 100).Between(12, 14) ? Plurality.Few : // few
			(n % 10).Between(0, 1) || (n % 10).Between(5, 9) || (n % 100).Between(12, 14) ? Plurality.Many : // many
			Plurality.Other;

		static PluralRuleDelegate Romanian => (n, c) =>
			n == 1 ? Plurality.One : // one
			n == 0 || (n % 100).Between(1, 19) ? Plurality.Few : // few
			Plurality.Other;

		static PluralRuleDelegate Tachelhit => (n, c) =>
			n >= 0 && n <= 1 ? Plurality.One : // one
			n.Between(2, 10) ? Plurality.Few : // few
			Plurality.Other;

		static PluralRuleDelegate Slovak => (n, c) =>
			n == 1 ? Plurality.One : // one
			n.Between(2, 4) ? Plurality.Few : // few
			Plurality.Other;

		static PluralRuleDelegate Slovenian => (n, c) =>
			n % 100 == 1 ? Plurality.One : // one
			n % 100 == 2 ? Plurality.Two : // two
			(n % 100).Between(3, 4) ? Plurality.Few : // few
			Plurality.Other;

		static PluralRuleDelegate CentralMoroccoTamazight => (n, c) =>
			n.Between(0, 1) || n.Between(11, 99)
				? Plurality.One : // one
				Plurality.Other;

		/// <summary>
		/// This delegate determines which singular or plural word should be chosen for the given quantity.
		/// This allows each language to define its own behavior for singular or plural words.
		/// </summary>
		/// <param name="value">The value that is being referenced by the singular or plural words</param>
		/// <param name="pluralCount"></param>
		/// <returns>Returns the index of the parameter to be used for pluralization.</returns>
		public delegate Plurality PluralRuleDelegate(decimal value, int pluralCount);
		
		/// <summary>
		/// Returns True if the value is inclusively between the min and max and has no fraction.
		/// </summary>
		static bool Between(this decimal value, decimal min, decimal max)
		{
			return value % 1 == 0 && value >= min && value <= max;
		}
	}
}